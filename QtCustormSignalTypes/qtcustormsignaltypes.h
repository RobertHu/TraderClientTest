#ifndef QTCUSTORMSIGNALTYPES_H
#define QTCUSTORMSIGNALTYPES_H

#include <QtWidgets/QMainWindow>
#include "ui_qtcustormsignaltypes.h"

#include <MyClass.h>

class QtCustormSignalTypes : public QMainWindow
{
	Q_OBJECT

public:
	QtCustormSignalTypes(QWidget *parent = 0);
	~QtCustormSignalTypes();

private:
	Ui::QtCustormSignalTypesClass ui;
signals:
	void dataArrived(const MyClass& data);
	private slots:
		void btnClicked(bool checked);
		void handleDataArrived(const MyClass& data);

};

#endif // QTCUSTORMSIGNALTYPES_H
