#include "qtssldemo.h"
#include <QtNetwork/QSslSocket>
#include <QtNetwork/QSslCipher>
#include <QtNetwork/QAbstractSocket>

QtSslDemo::QtSslDemo(QWidget *parent)
	: QMainWindow(parent)
{
	ui.setupUi(this);
	QSslSocket* sslSocket=new QSslSocket(this);
	QString hostName ="";
	quint16 port = 8888;
	sslSocket->connectToHostEncrypted(hostName,port);

}

QtSslDemo::~QtSslDemo()
{

}
