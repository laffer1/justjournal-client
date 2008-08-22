/*
Copyright (C) 2008 Lucas Holt. All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions
are met:
1. Redistributions of source code must retain the above copyright
   notice, this list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright
   notice, this list of conditions and the following disclaimer in the
   documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY AUTHOR AND CONTRIBUTORS ``AS IS'' AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
ARE DISCLAIMED.  IN NO EVENT SHALL AUTHOR OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS
OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY
OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF
SUCH DAMAGE.
*/

#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>
#include <stdbool.h>
#include <unistd.h>
#include <errno.h>
#include <limits.h>

#include <xmlrpc-c/base.h>
#include <xmlrpc-c/client.h>

#define NAME "JustJournal/UNIX"
#define VERSION "1.0.2"
#define ENTRY_MAX 32000
#define USERLEN 16
#define PASSLEN 19

static void usage( const char *name );
static void die_if_fault_occurred( xmlrpc_env *env );

int main( int argc, char *argv[] )
{
    xmlrpc_env env;
    xmlrpc_value * resultP;
    const char * postResult;
    char username[USERLEN];
    char password[PASSLEN];
    char entry[ENTRY_MAX];
    char c;
    int i;
    bool debug = false;

    if ( argc < 3 )
        usage( argv[0] );
    
    while ((c = getopt( argc, argv, "u:p:d" )) != -1) {
        switch( c )
        {
            case 'u':
                strncpy( username, optarg, USERLEN - 1 );
		username[USERLEN -1] = '\0';
                break;
            case 'p':
                strncpy( password, optarg, PASSLEN - 1 );
                password[PASSLEN -1] = '\0';
                break;
            case 'd':
                debug = true;
                fprintf( stderr, "Debug enabled.\n");
                break;
            case '?': /* fall through */
            default:
                usage( argv[0] );
        }
    }
    argc -= optind;

    for ( i = 0; i < ENTRY_MAX -1; i++ )
    {
        c = getchar();
        if ( c == EOF )
            break;
        entry[i] = c;
    }
    entry[i] = '\0';

    if ( i == 0 )
    {
        fprintf( stderr, "No entry specified." );
        exit(1);
    }
        
    xmlrpc_client_init( XMLRPC_CLIENT_NO_FLAGS, NAME, VERSION );
    xmlrpc_env_init( &env );

    resultP = xmlrpc_client_call( &env, "http://www.justjournal.com/xml-rpc",
                                "blogger.newPost",
                                "(sssssb)", 
				"", /* key, not used */
                                username, /* journal unique name */
				username, /* journal username */
				password, /* journal password */
				entry, /* blog content */
				true ); /* post now */
    die_if_fault_occurred( &env );

    xmlrpc_read_string( &env, resultP, &postResult );
    if (debug)
        fprintf( stderr, "Debug: post result is: %s\n", postResult );
    die_if_fault_occurred( &env );
    free((char *)postResult);

    xmlrpc_DECREF( resultP );
    xmlrpc_env_clean( &env );
    xmlrpc_client_cleanup();

    return 0;
}

static void usage( const char *name )
{
    fprintf( stderr, "usage: %s -u USERNAME -p PASSWORD\n", name );
    exit(0);
}

static void die_if_fault_occurred( xmlrpc_env *env )
{
    if ( env == NULL )
	exit(1);

    if ( env->fault_occurred )
    {
        if (env->fault_code == -501)
            fprintf( stderr, "ERROR:  Your account info %s",
                             "might be incorrect.\n" );
        else
            fprintf( stderr, "XML-RPC Fault: %s (%d)\n",
                env->fault_string, env->fault_code );
        exit(1);
    }
}

