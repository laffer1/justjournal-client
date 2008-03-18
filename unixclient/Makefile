CC=gcc
CFLAGS= -I/usr/local/include -Wall -std=c99
LDFLAGS= -L/usr/local/lib -lxmlrpc_client -lxmlrpc -lxmlrpc_xmlparse -lxmlrpc_xmltok -lxmlrpc_util
PREFIX=	/usr/local

all: clean jjclient

jjclient: jj.c
	${CC} ${CFLAGS} ${LDFLAGS} -o jjclient jj.c

install:
	install jjclient ${DESTDIR}${PREFIX}/bin/jjclient

clean:
	rm -f *.o jjclient

