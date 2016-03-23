/********************************************************************************
** Form generated from reading UI file 'dddddddd.ui'
**
** Created by: Qt User Interface Compiler version 5.1.0
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_DDDDDDDD_H
#define UI_DDDDDDDD_H

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

class Ui_ddddddddClass
{
public:
    QMenuBar *menuBar;
    QToolBar *mainToolBar;
    QWidget *centralWidget;
    QStatusBar *statusBar;

    void setupUi(QMainWindow *ddddddddClass)
    {
        if (ddddddddClass->objectName().isEmpty())
            ddddddddClass->setObjectName(QStringLiteral("ddddddddClass"));
        ddddddddClass->resize(600, 400);
        menuBar = new QMenuBar(ddddddddClass);
        menuBar->setObjectName(QStringLiteral("menuBar"));
        ddddddddClass->setMenuBar(menuBar);
        mainToolBar = new QToolBar(ddddddddClass);
        mainToolBar->setObjectName(QStringLiteral("mainToolBar"));
        ddddddddClass->addToolBar(mainToolBar);
        centralWidget = new QWidget(ddddddddClass);
        centralWidget->setObjectName(QStringLiteral("centralWidget"));
        ddddddddClass->setCentralWidget(centralWidget);
        statusBar = new QStatusBar(ddddddddClass);
        statusBar->setObjectName(QStringLiteral("statusBar"));
        ddddddddClass->setStatusBar(statusBar);

        retranslateUi(ddddddddClass);

        QMetaObject::connectSlotsByName(ddddddddClass);
    } // setupUi

    void retranslateUi(QMainWindow *ddddddddClass)
    {
        ddddddddClass->setWindowTitle(QApplication::translate("ddddddddClass", "dddddddd", 0));
    } // retranslateUi

};

namespace Ui {
    class ddddddddClass: public Ui_ddddddddClass {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_DDDDDDDD_H
