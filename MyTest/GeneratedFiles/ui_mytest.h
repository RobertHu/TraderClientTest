/********************************************************************************
** Form generated from reading UI file 'mytest.ui'
**
** Created by: Qt User Interface Compiler version 5.1.0
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_MYTEST_H
#define UI_MYTEST_H

#include <QtCore/QVariant>
#include <QtWidgets/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QButtonGroup>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QToolBar>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_MyTestClass
{
public:
    QMenuBar *menuBar;
    QToolBar *mainToolBar;
    QWidget *centralWidget;
    QStatusBar *statusBar;

    void setupUi(QMainWindow *MyTestClass)
    {
        if (MyTestClass->objectName().isEmpty())
            MyTestClass->setObjectName(QStringLiteral("MyTestClass"));
        MyTestClass->resize(600, 400);
        menuBar = new QMenuBar(MyTestClass);
        menuBar->setObjectName(QStringLiteral("menuBar"));
        MyTestClass->setMenuBar(menuBar);
        mainToolBar = new QToolBar(MyTestClass);
        mainToolBar->setObjectName(QStringLiteral("mainToolBar"));
        MyTestClass->addToolBar(mainToolBar);
        centralWidget = new QWidget(MyTestClass);
        centralWidget->setObjectName(QStringLiteral("centralWidget"));
        MyTestClass->setCentralWidget(centralWidget);
        statusBar = new QStatusBar(MyTestClass);
        statusBar->setObjectName(QStringLiteral("statusBar"));
        MyTestClass->setStatusBar(statusBar);

        retranslateUi(MyTestClass);

        QMetaObject::connectSlotsByName(MyTestClass);
    } // setupUi

    void retranslateUi(QMainWindow *MyTestClass)
    {
        MyTestClass->setWindowTitle(QApplication::translate("MyTestClass", "MyTest", 0));
    } // retranslateUi

};

namespace Ui {
    class MyTestClass: public Ui_MyTestClass {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_MYTEST_H
