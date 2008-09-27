/* $Id: gtkjj.c,v 1.4 2008/09/27 02:06:02 laffer1 Exp $ */
/*-
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

#include <gtk/gtk.h>

#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>

#include <xmlrpc-c/base.h>
#include <xmlrpc-c/client.h>

#define NAME "JustJournal/GTK"
#define VERSION "1.0"

GtkWidget *user, *pass; /* textboxes */
GtkTextBuffer *buffer;

static void button_clicked (GtkButton *button, GtkWindow *parent);
static void msgbox( GtkWindow *parent, char * msg );
static void cut_clicked (GtkButton*, GtkTextView*);
static void copy_clicked (GtkButton*, GtkTextView*);
static void paste_clicked (GtkButton*, GtkTextView*);

int main( int argc, char *argv[] )
{
    GtkWidget *window, *vbox, *authbox, *authbox2, *vauthbox, *hboxccp;
    GtkWidget *lbluser, *lblpass; /* labels */
    GtkWidget *submit, *cut, *copy, *paste; /* buttons */
    GtkWidget *scrolled_win, *textview = NULL;

    gtk_init( &argc, &argv );

    window = gtk_window_new( GTK_WINDOW_TOPLEVEL );
    gtk_window_set_title( GTK_WINDOW (window), NAME );
    gtk_container_set_border_width( GTK_CONTAINER (window), 10 );
    //gtk_widget_set_size_request( window, 200, -1 );

    g_signal_connect (G_OBJECT (window), "destroy",
                  G_CALLBACK (gtk_main_quit), NULL);

    cut = gtk_button_new_from_stock (GTK_STOCK_CUT);
    copy = gtk_button_new_from_stock (GTK_STOCK_COPY);
    paste = gtk_button_new_from_stock (GTK_STOCK_PASTE);

    hboxccp = gtk_hbox_new (TRUE, 5);
    gtk_box_pack_start (GTK_BOX (hboxccp), cut, TRUE, TRUE, 0);
    gtk_box_pack_start (GTK_BOX (hboxccp), copy, TRUE, TRUE, 0);
    gtk_box_pack_start (GTK_BOX (hboxccp), paste, TRUE, TRUE, 0);


    g_signal_connect (G_OBJECT (cut), "clicked",
                    G_CALLBACK (cut_clicked),
                    (gpointer) textview);
    g_signal_connect (G_OBJECT (copy), "clicked",
                    G_CALLBACK (copy_clicked),
                    (gpointer) textview);
    g_signal_connect (G_OBJECT (paste), "clicked",
                    G_CALLBACK (paste_clicked),
                    (gpointer) textview);

    submit = gtk_button_new_with_mnemonic("_Post");
    g_signal_connect (G_OBJECT (submit), "clicked",
                    G_CALLBACK (button_clicked),
                    (gpointer) window);

    lbluser = gtk_label_new( "Username" );
    lblpass = gtk_label_new( "Password" );
    user = gtk_entry_new();
    pass = gtk_entry_new();
    gtk_entry_set_visibility( GTK_ENTRY (pass), FALSE );
    gtk_entry_set_invisible_char( GTK_ENTRY (pass), '*' );

    /* setup textview */
    textview = gtk_text_view_new();
    buffer = gtk_text_view_get_buffer (GTK_TEXT_VIEW (textview));
    scrolled_win = gtk_scrolled_window_new (NULL, NULL);
    gtk_container_add (GTK_CONTAINER (scrolled_win), textview);
    gtk_scrolled_window_set_policy (GTK_SCROLLED_WINDOW (scrolled_win),
                                  GTK_POLICY_AUTOMATIC, GTK_POLICY_ALWAYS);

    /* create username hbox */
    authbox = gtk_hbox_new( FALSE, 5 );
    gtk_box_pack_start_defaults( GTK_BOX (authbox), lbluser );
    gtk_box_pack_start_defaults( GTK_BOX (authbox), user );

    /* create password hbox */
    authbox2 = gtk_hbox_new( FALSE, 5 );
    gtk_box_pack_start_defaults( GTK_BOX (authbox2), lblpass );
    gtk_box_pack_start_defaults( GTK_BOX (authbox2), pass );

    vauthbox = gtk_vbox_new( FALSE, 5 );
    gtk_box_pack_start_defaults( GTK_BOX (vauthbox), authbox );
    gtk_box_pack_start_defaults( GTK_BOX (vauthbox), authbox2 );

    /* Setup the final box for layout in the window */
    vbox = gtk_vbox_new( FALSE, 5 );
    gtk_box_pack_start( GTK_BOX (vbox), hboxccp, FALSE, TRUE, 5 );
    gtk_box_pack_start( GTK_BOX (vbox), vauthbox, FALSE, TRUE, 5 );
    gtk_box_pack_start( GTK_BOX (vbox), scrolled_win, TRUE, TRUE, 5 );
    gtk_box_pack_start( GTK_BOX (vbox), submit, FALSE, TRUE, 5 );

    gtk_container_add( GTK_CONTAINER (window), vbox );
    gtk_widget_show_all( window );

    gtk_main();
    return 0;
}

static void button_clicked( GtkButton *button, GtkWindow *parent )
{
    xmlrpc_env env;
    xmlrpc_value * resultP;
    const char * postResult;
    const gchar *u, *p; /* username, password */
    char *c;
    GtkTextIter start, end;
    char *errmsg = NULL;

    u = gtk_entry_get_text( GTK_ENTRY (user) );
    p = gtk_entry_get_text( GTK_ENTRY (pass) );
    gtk_text_buffer_get_bounds (buffer, &start, &end);
    c = gtk_text_iter_get_text (&start, &end);

    xmlrpc_client_init( XMLRPC_CLIENT_NO_FLAGS, NAME, VERSION );
    xmlrpc_env_init( &env );

    resultP = xmlrpc_client_call( &env, "http://www.justjournal.com/xml-rpc",
                                "blogger.newPost",
                                "(sssssb)", 
				"", /* key, not used */
                                u, /* journal unique name */
				u, /* journal username */
				p, /* journal password */
				c, /* blog content */
				true ); /* post now */
    if ( env.fault_occurred )
    {
        if (env.fault_code == -501)
            asprintf( &errmsg, "ERROR:  Your account info %s",
                             "might be incorrect.\n" );
        else
            asprintf( &errmsg, "XML-RPC Fault: %s (%d)\n",
                env.fault_string, env.fault_code );
        msgbox( parent, errmsg );
        free(errmsg);
        goto cleanup;
    }
    xmlrpc_read_string( &env, resultP, &postResult ); // do we need this?
    if ( env.fault_occurred )
    {
        if (env.fault_code == -501)
            asprintf( &errmsg, "ERROR:  Your account info %s",
                             "might be incorrect.\n" );
        else
            asprintf( &errmsg, "XML-RPC Fault: %s (%d)\n",
                env.fault_string, env.fault_code );
        msgbox( parent, errmsg);
        free(errmsg);
    }
    /* if we get here it worked.  Clear the entry data */
    gtk_entry_set_text( GTK_ENTRY (user), "" );
    gtk_entry_set_text( GTK_ENTRY (pass), "" );
    gtk_text_buffer_set_text (buffer, "", -1);

cleanup:
    g_free(c);
    xmlrpc_DECREF( resultP );
    xmlrpc_env_clean( &env );
    xmlrpc_client_cleanup();
}

static void msgbox( GtkWindow * parent, char * msg )
{
    GtkWidget *dialog, *label, *image, *hbox;

    /* Create a non-modal dialog with one OK button. */
    dialog = gtk_dialog_new_with_buttons ("Information", parent,
                                        GTK_DIALOG_DESTROY_WITH_PARENT,
                                        GTK_STOCK_OK, GTK_RESPONSE_OK,
                                        NULL);

    gtk_dialog_set_has_separator (GTK_DIALOG (dialog), FALSE);

    label = gtk_label_new (msg);
    image = gtk_image_new_from_stock (GTK_STOCK_DIALOG_INFO,
                                    GTK_ICON_SIZE_DIALOG);

    hbox = gtk_hbox_new (FALSE, 5);
    gtk_container_set_border_width (GTK_CONTAINER (hbox), 10);
    gtk_box_pack_start_defaults (GTK_BOX (hbox), image);
    gtk_box_pack_start_defaults (GTK_BOX (hbox), label);

    gtk_box_pack_start_defaults (GTK_BOX (GTK_DIALOG (dialog)->vbox), hbox);
    gtk_widget_show_all (dialog);

    /* Call gtk_widget_destroy() when the dialog emits the response signal. */
    g_signal_connect (G_OBJECT (dialog), "response",
                    G_CALLBACK (gtk_widget_destroy), NULL);
}

/* Copy the selected text to the clipboard and remove it from the buffer. */
static void
cut_clicked (GtkButton *cut,
             GtkTextView *textview)
{
    GtkClipboard *clipboard = gtk_clipboard_get (GDK_SELECTION_CLIPBOARD);
    //GtkTextBuffer *buffer2 = gtk_text_view_get_buffer (textview);

    gtk_text_buffer_cut_clipboard (buffer, clipboard, TRUE);
}

/* Copy the selected text to the clipboard. */
static void
copy_clicked (GtkButton *copy,
              GtkTextView *textview)
{
    GtkClipboard *clipboard = gtk_clipboard_get (GDK_SELECTION_CLIPBOARD);

    gtk_text_buffer_copy_clipboard (buffer, clipboard);
}

/* Insert the text from the clipboard into the text buffer. */
static void
paste_clicked (GtkButton *paste,
               GtkTextView *textview)
{
    GtkClipboard *clipboard = gtk_clipboard_get (GDK_SELECTION_CLIPBOARD);
    //GtkTextBuffer *buffer2 = gtk_text_view_get_buffer (textview);

    gtk_text_buffer_paste_clipboard (buffer, clipboard, NULL, TRUE);
}
