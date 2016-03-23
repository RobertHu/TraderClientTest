#ifndef QTTHREADDEMO_H
#define QTTHREADDEMO_H

#include <QtWidgets/QMainWindow>
#include "ui_qtthreaddemo.h"

class QtThreadDemo : public QMainWindow
{
	Q_OBJECT

public:
	QtThreadDemo(QWidget *parent = 0);
	~QtThreadDemo();

private:
	Ui::QtThreadDemoClass ui;
};

#endif // QTTHREADDEMO_H
