#ifndef QTSSLDEMO_H
#define QTSSLDEMO_H

#include <QtWidgets/QMainWindow>
#include "ui_qtssldemo.h"

class QtSslDemo : public QMainWindow
{
	Q_OBJECT

public:
	QtSslDemo(QWidget *parent = 0);
	~QtSslDemo();

private:
	Ui::QtSslDemoClass ui;
};

#endif // QTSSLDEMO_H
