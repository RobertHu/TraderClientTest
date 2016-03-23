#include "qtssldemo.h"
#include <QtWidgets/QApplication>

int main(int argc, char *argv[])
{
	QApplication a(argc, argv);
	QtSslDemo w;
	w.show();
	return a.exec();
}
