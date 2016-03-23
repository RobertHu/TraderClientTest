/********************************************************************************
** Form generated from reading UI file 'qtthreaddemo.ui'
**
** Created by: Qt User Interface Compiler version 5.1.0
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_QTTHREADDEMO_H
#define UI_QTTHREADDEMO_H

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

class Ui_QtThreadDemoClass
{
public:
    QMenuBar *menuBar;
    QToolBar *mainToolBar;
    QWidget *centralWidget;
    QStatusBar *statusBar;

    void setupUi(QMainWindow *QtThreadDemoClass)
    {
        if (QtThreadDemoClass->objectName().isEmpty())
            QtThreadDemoClass->setObjectName(QStringLiteral("QtThreadDemoClass"));
        QtThreadDemoClass->resize(600, 400);
        menuBar = new QMenuBar(QtThreadDemoClass);
        menuBar->setObjectName(QStringLiteral("menuBar"));
        QtThreadDemoClass->setMenuBar(menuBar);
        mainToolBar = new QToolBar(QtThreadDemoClass);
        mainToolBar->setObjectName(QStringLiteral("mainToolBar"));
        QtThreadDemoClass->addToolBar(mainToolBar);
        centralWidget = new QWidget(QtThreadDemoClass);
        centralWidget->setObjectName(QStringLiteral("centralWidget"));
        QtThreadDemoClass->setCentralWidget(centralWidget);
        statusBar = new QStatusBar(QtThreadDemoClass);
        statusBar->setObjectName(QStringLiteral("statusBar"));
        QtThreadDemoClass->setStatusBar(statusBar);

        retranslateUi(QtThreadDemoClass);

        QMetaObject::connectSlotsByName(QtThreadDemoClass);
    } // setupUi

    void retranslateUi(QMainWindow *QtThreadDemoClass)
    {
        QtThreadDemoClass->setWindowTitle(QApplication::translate("QtThreadDemoClass", "QtThreadDemo", 0));
    } // retranslateUi

};

namespace Ui {
    class QtThreadDemoClass: public Ui_QtThreadDemoClass {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_QTTHREADDEMO_H
