#include "qtssldemo2.h"
#include <QtWidgets/QApplication>
#include "Communication.h"
#include "PacketParser.h"
#include "SendCenter.h"
int main(int argc, char *argv[])
{
	QApplication a(argc, argv);
	PacketParser::Default.start();
	SendCenter::Default.start();
	QtSslDemo2 demo;
	demo.show();
	return a.exec();
}
