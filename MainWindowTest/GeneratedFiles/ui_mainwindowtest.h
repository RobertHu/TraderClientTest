/********************************************************************************
** Form generated from reading UI file 'mainwindowtest.ui'
**
** Created by: Qt User Interface Compiler version 5.1.0
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_MAINWINDOWTEST_H
#define UI_MAINWINDOWTEST_H

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

class Ui_MainWindowTestClass
{
public:
    QMenuBar *menuBar;
    QToolBar *mainToolBar;
    QWidget *centralWidget;
    QStatusBar *statusBar;

    void setupUi(QMainWindow *MainWindowTestClass)
    {
        if (MainWindowTestClass->objectName().isEmpty())
            MainWindowTestClass->setObjectName(QStringLiteral("MainWindowTestClass"));
        MainWindowTestClass->resize(600, 400);
        menuBar = new QMenuBar(MainWindowTestClass);
        menuBar->setObjectName(QStringLiteral("menuBar"));
        MainWindowTestClass->setMenuBar(menuBar);
        mainToolBar = new QToolBar(MainWindowTestClass);
        mainToolBar->setObjectName(QStringLiteral("mainToolBar"));
        MainWindowTestClass->addToolBar(mainToolBar);
        centralWidget = new QWidget(MainWindowTestClass);
        centralWidget->setObjectName(QStringLiteral("centralWidget"));
        MainWindowTestClass->setCentralWidget(centralWidget);
        statusBar = new QStatusBar(MainWindowTestClass);
        statusBar->setObjectName(QStringLiteral("statusBar"));
        MainWindowTestClass->setStatusBar(statusBar);

        retranslateUi(MainWindowTestClass);

        QMetaObject::connectSlotsByName(MainWindowTestClass);
    } // setupUi

    void retranslateUi(QMainWindow *MainWindowTestClass)
    {
        MainWindowTestClass->setWindowTitle(QApplication::translate("MainWindowTestClass", "MainWindowTest", 0));
    } // retranslateUi

};

namespace Ui {
    class MainWindowTestClass: public Ui_MainWindowTestClass {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_MAINWINDOWTEST_H
