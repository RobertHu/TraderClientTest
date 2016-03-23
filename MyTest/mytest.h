#ifndef MYTEST_H
#define MYTEST_H

#include <QtWidgets/QMainWindow>
#include "ui_mytest.h"

class MyTest : public QMainWindow
{
	Q_OBJECT

public:
	MyTest(QWidget *parent = 0);
	~MyTest();

private:
	Ui::MyTestClass ui;
};

#endif // MYTEST_H
