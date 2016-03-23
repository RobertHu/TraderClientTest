#ifndef LUADEMO1_H
#define LUADEMO1_H

#include <QtWidgets/QMainWindow>
#include "ui_luademo1.h"

class LuaDemo1 : public QMainWindow
{
	Q_OBJECT

public:
	LuaDemo1(QWidget *parent = 0);
	~LuaDemo1();

private:
	Ui::LuaDemo1Class ui;
};

#endif // LUADEMO1_H
