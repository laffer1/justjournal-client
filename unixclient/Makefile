# Makefile for jjclient
# $Id: Makefile,v 1.8 2009/06/04 05:16:43 laffer1 Exp $
CC?=gcc
CFLAGS?= -I/usr/local/include -Wall -pedantic -std=c99 -O2 -fstack-protector
LDFLAGS?=-L/usr/local/lib -lxmlrpc_client -lxmlrpc -lxmlrpc_xmlparse -lxmlrpc_xmltok -lxmlrpc_util -lcurl
PREFIX?= /usr/local

all: clean jjclient

jjclient: jj.o
	${CC} ${CFLAGS} ${LDFLAGS} -o jjclient jj.o

jj.o: jj.c
	${CC} ${CFLAGS} -c jj.c

install: install-man
	install -o root -g wheel -m 555 jjclient ${DESTDIR}${PREFIX}/bin/jjclient

install-man:
	install -o root -g wheel -m 444 jjclient.1 ${DESTDIR}${PREFIX}/man/man1/

clean:
	rm -f *.o jjclient

