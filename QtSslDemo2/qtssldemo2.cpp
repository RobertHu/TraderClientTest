#include "qtssldemo2.h"
#include "Communication.h"
#include "CommunicationObject.h"
#include <quuid.h>
#include <qjsonobject.h>
#include <qjsonarray.h>
#include <qjsonvalue.h>
#include "PacketBulder.h"
#include "PacketParser.h"
#include "SendCenter.h"
#include "InvokeManager.h"
#include "RequestCondition.h"
#include <qjsondocument.h>
#include <qpushbutton.h>

QtSslDemo2::QtSslDemo2(QWidget *parent)
	: QMainWindow(parent)
{
	ui.setupUi(this);
	connect(this,&QtSslDemo2::startConnect,&Communication::Default,&Communication::startConnect,Qt::BlockingQueuedConnection);
	QPushButton* btn = new QPushButton(this);
	connect(btn,&QAbstractButton::clicked,this,&QtSslDemo2::btnClicked);
	this->setCentralWidget(btn);
	_thread = new QThread();
	Communication::Default.moveToThread(_thread);
	_thread->start();
}

QtSslDemo2::~QtSslDemo2()
{
	_thread->quit();
	delete _thread;
}

void QtSslDemo2::btnClicked()
{
	start();
}

void QtSslDemo2::start()
{
	emit startConnect("127.0.0.1",8888);

	qDebug() << "ui currentThreadid: "<<QThread::currentThreadId();
	QString invokeId =InvokeManager::Default.generateKey();
	QString session;
	QJsonObject content;
	QJsonArray arguments;
	arguments.append(tr("MN01"));
	arguments.append(tr("12345678"));
	arguments.append(tr("CHS"));
	arguments.append(tr("13"));
	content.insert("Arguments",arguments);
	content.insert("InvokeId",invokeId);
	content.insert("Method",tr("Login"));
	CommunicationObject* request = new CommunicationObject(session,invokeId,content);
	QByteArray* packet = PacketBulder::build(request);
	SendCenter::Default.add(packet);
	RequestCondition* requestCondition = InvokeManager::Default.get(invokeId);
	requestCondition->waitForCondition();
	InvokeManager::Default.remove(invokeId);
	if(requestCondition->getIsError())
	{
		qDebug() << "login error";
	}
	else
	{
		QJsonObject loginResult  = requestCondition->getContent();
		QJsonDocument document;
		document.setObject(loginResult);
		qDebug() << "get login result: "<< QString::fromUtf8(document.toJson());
	}
}
