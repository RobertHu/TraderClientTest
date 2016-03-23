#ifndef MAINWINDOWTEST_H
#define MAINWINDOWTEST_H

#include <QtWidgets/QMainWindow>
#include "ui_mainwindowtest.h"

class MainWindowTest : public QMainWindow
{
	Q_OBJECT

public:
	MainWindowTest(QWidget *parent = 0);
	~MainWindowTest();

private:
	Ui::MainWindowTestClass ui;
};

#endif // MAINWINDOWTEST_H
