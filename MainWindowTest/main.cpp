#include "mainwindowtest.h"
#include <QtWidgets/QApplication>

int main(int argc, char *argv[])
{
	QApplication a(argc, argv);
	MainWindowTest w;
	w.show();
	return a.exec();
}
