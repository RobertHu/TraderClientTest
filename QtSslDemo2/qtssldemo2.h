#ifndef QTSSLDEMO2_H
#define QTSSLDEMO2_H

#include <QtWidgets/QMainWindow>
#include "ui_qtssldemo2.h"
#include <qbytearray.h>
#include <qwaitcondition.h>
#include <qmutex.h>

class QtSslDemo2 : public QMainWindow
{
	Q_OBJECT

public:
	QtSslDemo2(QWidget *parent = 0);
	~QtSslDemo2();
	void start();

signals:
	void startConnect(const QString& host ,int port);

private slots:
	void btnClicked();

private:
	Ui::QtSslDemo2Class ui;
	QThread* _thread;
};

#endif // QTSSLDEMO2_H
