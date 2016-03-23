/********************************************************************************
** Form generated from reading UI file 'qtssldemo.ui'
**
** Created by: Qt User Interface Compiler version 5.1.0
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_QTSSLDEMO_H
#define UI_QTSSLDEMO_H

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

class Ui_QtSslDemoClass
{
public:
    QMenuBar *menuBar;
    QToolBar *mainToolBar;
    QWidget *centralWidget;
    QStatusBar *statusBar;

    void setupUi(QMainWindow *QtSslDemoClass)
    {
        if (QtSslDemoClass->objectName().isEmpty())
            QtSslDemoClass->setObjectName(QStringLiteral("QtSslDemoClass"));
        QtSslDemoClass->resize(600, 400);
        menuBar = new QMenuBar(QtSslDemoClass);
        menuBar->setObjectName(QStringLiteral("menuBar"));
        QtSslDemoClass->setMenuBar(menuBar);
        mainToolBar = new QToolBar(QtSslDemoClass);
        mainToolBar->setObjectName(QStringLiteral("mainToolBar"));
        QtSslDemoClass->addToolBar(mainToolBar);
        centralWidget = new QWidget(QtSslDemoClass);
        centralWidget->setObjectName(QStringLiteral("centralWidget"));
        QtSslDemoClass->setCentralWidget(centralWidget);
        statusBar = new QStatusBar(QtSslDemoClass);
        statusBar->setObjectName(QStringLiteral("statusBar"));
        QtSslDemoClass->setStatusBar(statusBar);

        retranslateUi(QtSslDemoClass);

        QMetaObject::connectSlotsByName(QtSslDemoClass);
    } // setupUi

    void retranslateUi(QMainWindow *QtSslDemoClass)
    {
        QtSslDemoClass->setWindowTitle(QApplication::translate("QtSslDemoClass", "QtSslDemo", 0));
    } // retranslateUi

};

namespace Ui {
    class QtSslDemoClass: public Ui_QtSslDemoClass {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_QTSSLDEMO_H
