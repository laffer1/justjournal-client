# $Id: Makefile,v 1.4 2008/09/27 02:06:02 laffer1 Exp $
# gtkjj: Just Journal for GTK
CC=gcc
CFLAGS= -I/usr/local/include -Wall -pedantic -std=c99 -O2 `pkg-config --cflags gtk+-2.0`
LDFLAGS= -L/usr/local/lib -lxmlrpc_client -lxmlrpc -lxmlrpc_xmlparse -lxmlrpc_xmltok -lxmlrpc_util -lcurl `pkg-config --libs gtk+-2.0`
PREFIX=	/usr/local

all: clean gtkjj

gtkjj: gtkjj.c
	${CC} ${CFLAGS} ${LDFLAGS} -o gtkjj gtkjj.c

install:
	install gtkjj ${DESTDIR}${PREFIX}/bin/gtkjj

clean:
	rm -f *.o gtkjj
