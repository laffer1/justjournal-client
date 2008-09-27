# Makefile for jjclient
# $Id: Makefile,v 1.4 2008/09/27 00:30:40 laffer1 Exp $
CC=gcc
CFLAGS= -I/usr/local/include -Wall -pedantic -std=c99
LDFLAGS= -L/usr/local/lib -lxmlrpc_client -lxmlrpc -lxmlrpc_xmlparse -lxmlrpc_xmltok -lxmlrpc_util -lcurl
PREFIX=	/usr/local

all: clean jjclient

jjclient: jj.c
	${CC} ${CFLAGS} ${LDFLAGS} -o jjclient jj.c

install: install-man
	install jjclient ${DESTDIR}${PREFIX}/bin/jjclient

install-man:
	cp jjclient.1 ${DESTDIR}${PREFIX}/man/man1/

clean:
	rm -f *.o jjclient

