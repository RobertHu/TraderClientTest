#include "qtthreaddemo.h"
#include <QtWidgets/QApplication>
#include "TextThread.h"
#include <qmessagebox.h>
extern bool stopThreads;

int main(int argc, char *argv[])
{
	QApplication a(argc, argv);
	TextThread foo("Foo"), bar("Bar");
	foo.start();
	bar.start();
	QMessageBox::information(nullptr,"Threading", "Close me to stop");
	stopThreads = true;
	foo.wait();
	bar.wait();
	return 0;
}
