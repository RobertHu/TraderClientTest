#include "qtcustormsignaltypes.h"
#include <qpushbutton.h>
#include <qmessagebox.h>

QtCustormSignalTypes::QtCustormSignalTypes(QWidget *parent)
	: QMainWindow(parent)
{
	ui.setupUi(this);
	QPushButton * btn = new QPushButton;
	btn->setText("Click");
	setCentralWidget(btn);
	connect(btn,SIGNAL(clicked(bool)),this,SLOT(btnClicked(bool)));
	connect(this,SIGNAL(dataArrived(const MyClass&)),this,SLOT(handleDataArrived(const MyClass&)));
}

QtCustormSignalTypes::~QtCustormSignalTypes()
{

}

void QtCustormSignalTypes::btnClicked(bool checked)
{
	MyClass my("Hello world");
	emit dataArrived(my);
}

void QtCustormSignalTypes::handleDataArrived(const MyClass& data)
{
	QMessageBox box;
	box.setText(data.getDesc());
	box.exec();

}