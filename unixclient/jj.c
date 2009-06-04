/*-
Copyright (C) 2008, 2009 Lucas Holt. All rights reserved.

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

$Id: jj.c,v 1.11 2009/06/04 05:16:43 laffer1 Exp $
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
#define VERSION "1.0.3"
#define ENTRY_MAX 32000
#define USERLEN 16
#define PASSLEN 19
#define HOSTLEN 512
#define HOST "www.justjournal.com"
#define RECENT_POST_COUNT 15

static void usage( const char *name );
static void die_if_fault_occurred( xmlrpc_env *env );
static void getRecentPosts( const char *host, const char *username, const char *password );

int main( int argc, char *argv[] )
{
    xmlrpc_env env;
    xmlrpc_value * resultP = NULL;
    const char * postResult = NULL;
    char username[USERLEN];
    char password[PASSLEN];
    char entry[ENTRY_MAX];
    char host[HOSTLEN];
    char * title = NULL;
    size_t titlelen;
    int c;
    size_t i;
    bool debug = false;
    bool hflag = false;
    bool uflag = false;
    bool pflag = false;
    bool rflag = false;

    if ( argc < 3 )
        usage( argv[0] );
    
    while ((c = getopt( argc, argv, "h:u:p:s:dr" )) != -1) {
        switch( c )
        {
            case 'h': /* host */	
            	if ( strlen(optarg) > HOSTLEN - 16)
            	{
            	    fprintf( stderr, "host name is too long" );
            	    exit(EXIT_FAILURE);   
            	}
            	(void) snprintf(host, HOSTLEN, "http://%s/xml-rpc", optarg);
            	hflag = true;
            	break;
         
            case 'u': /* username */
                strncpy( username, optarg, USERLEN - 1 );
                username[USERLEN -1] = '\0';
                uflag = true;
                break;
         
            case 'p': /* password */
                strncpy( password, optarg, PASSLEN - 1 );
                password[PASSLEN -1] = '\0';
                pflag = true;
                break;
                
            case 's': /* subject */
            	titlelen = strlen(optarg);
            	if ( (title = malloc((titlelen * sizeof(char)) + 1)) == NULL )
            	{
                    fprintf( stderr, "Unable to allocate memory." );
                    exit(EXIT_FAILURE);
                }    
                strncpy( title, optarg, titlelen );
                title[titlelen] = '\0';
                break;
                
            case 'r': /* recent entries */
                rflag = true;
                break;
                
            case 'd': /* debug */
                debug = true;
                fprintf( stderr, "Debug enabled.\n");
                break;
                
            case '?': /* fall through */
            default:
                usage( argv[0] );
        }
    }
    argc -= optind;
    
    if ( !uflag || !pflag )
    {
        fprintf( stderr, "Username and password are required.\n" );
        usage( argv[0] );
    }
    
    /* set host if it's not defined */
    if ( !hflag )
        (void) snprintf(host, HOSTLEN, "http://%s/xml-rpc", HOST);
        
    if ( debug && hflag )
        fprintf( stderr, "host is set to: %s\n", host );

    if ( rflag )
    {
        getRecentPosts( host, username, password );
        exit(0);
    }

    /* Copy the title into the top of entry if it's requested */
    entry[0] = '\0';
    if ( title != NULL ) 
        (void) snprintf( entry, ENTRY_MAX, "<title>%s</title>", title );
        
    /* Start from title or the beginning and copy the entry from stdin */        
    for ( i = strlen(entry); i < ENTRY_MAX -1; i++ )
    {
        c = getchar();
        if ( c == EOF )
            break;
        entry[i] = (char) c;
    }
    entry[i] = '\0';

    if ( i == 0 )
    {
        fprintf( stderr, "No entry specified." );
        exit(EXIT_FAILURE);
    }
        
    xmlrpc_client_init( XMLRPC_CLIENT_NO_FLAGS, NAME, VERSION );
    xmlrpc_env_init( &env );

    resultP = xmlrpc_client_call( &env, host,
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
    if ( debug && postResult != NULL )
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
    fprintf( stderr, "usage: %s -u USERNAME -p PASSWORD [-h host] [-s subject] [-d]\n", name );
    exit(EXIT_FAILURE);
}

static void die_if_fault_occurred( xmlrpc_env *env )
{
    if ( env == NULL )
	    exit(EXIT_FAILURE);

    if ( env->fault_occurred )
    {
        if ( env->fault_code == -501 )
            fprintf( stderr, "ERROR:  Your account info %s",
                             "might be incorrect.\n" );
        else
            fprintf( stderr, "XML-RPC Fault: %s (%d)\n",
                env->fault_string, env->fault_code );
        exit(EXIT_FAILURE);
    }
}

static void getRecentPosts( const char *host, const char *username, const char *password )
{
    xmlrpc_env env;
    xmlrpc_value * resultP = NULL; // array item 
    xmlrpc_value * firstElementP = NULL;  // first element in array
    xmlrpc_value * varP = NULL;
    const char * postResult = NULL;
    int arrsize;
    
    xmlrpc_client_init( XMLRPC_CLIENT_NO_FLAGS, NAME, VERSION );
    xmlrpc_env_init( &env );

    resultP = xmlrpc_client_call( &env, host,
                                "blogger.getRecentPosts",
                                "(ssssi)", 
                                "", /* key, not used */
                                username, /* journal unique name */
                                username, /* journal username */
                                password, /* journal password */
                                RECENT_POST_COUNT ); /* post count */
    die_if_fault_occurred( &env );
 
    arrsize = xmlrpc_array_size( &env, resultP ); 
    die_if_fault_occurred( &env );
    //fprintf( stderr, "Array size %d\n", arrsize );
    
    for (int i = 0; i < arrsize; i++ )
    {
        xmlrpc_array_read_item( &env, resultP, i, &firstElementP);
        xmlrpc_struct_find_value( &env, firstElementP, "title", &varP);
        if (varP)
        {
            xmlrpc_read_string( &env, varP, &postResult);
            printf( "%d %s\n\n", i, postResult );
            free((char *)postResult);
            xmlrpc_DECREF( varP );
        }
        die_if_fault_occurred( &env );
        
        xmlrpc_struct_find_value( &env, firstElementP, "content", &varP);
        if (varP)
        {
            xmlrpc_read_string( &env, varP, &postResult);
            printf( "%s\n\n", postResult );
            free((char *)postResult);
            xmlrpc_DECREF( varP );
        }
        die_if_fault_occurred( &env );

        xmlrpc_DECREF( firstElementP );
    }
    xmlrpc_DECREF( resultP );
    xmlrpc_env_clean( &env );
    xmlrpc_client_cleanup();
}
