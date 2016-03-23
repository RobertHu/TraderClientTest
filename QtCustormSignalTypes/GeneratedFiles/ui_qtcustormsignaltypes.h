/********************************************************************************
** Form generated from reading UI file 'qtcustormsignaltypes.ui'
**
** Created by: Qt User Interface Compiler version 5.1.0
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_QTCUSTORMSIGNALTYPES_H
#define UI_QTCUSTORMSIGNALTYPES_H

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

class Ui_QtCustormSignalTypesClass
{
public:
    QMenuBar *menuBar;
    QToolBar *mainToolBar;
    QWidget *centralWidget;
    QStatusBar *statusBar;

    void setupUi(QMainWindow *QtCustormSignalTypesClass)
    {
        if (QtCustormSignalTypesClass->objectName().isEmpty())
            QtCustormSignalTypesClass->setObjectName(QStringLiteral("QtCustormSignalTypesClass"));
        QtCustormSignalTypesClass->resize(600, 400);
        menuBar = new QMenuBar(QtCustormSignalTypesClass);
        menuBar->setObjectName(QStringLiteral("menuBar"));
        QtCustormSignalTypesClass->setMenuBar(menuBar);
        mainToolBar = new QToolBar(QtCustormSignalTypesClass);
        mainToolBar->setObjectName(QStringLiteral("mainToolBar"));
        QtCustormSignalTypesClass->addToolBar(mainToolBar);
        centralWidget = new QWidget(QtCustormSignalTypesClass);
        centralWidget->setObjectName(QStringLiteral("centralWidget"));
        QtCustormSignalTypesClass->setCentralWidget(centralWidget);
        statusBar = new QStatusBar(QtCustormSignalTypesClass);
        statusBar->setObjectName(QStringLiteral("statusBar"));
        QtCustormSignalTypesClass->setStatusBar(statusBar);

        retranslateUi(QtCustormSignalTypesClass);

        QMetaObject::connectSlotsByName(QtCustormSignalTypesClass);
    } // setupUi

    void retranslateUi(QMainWindow *QtCustormSignalTypesClass)
    {
        QtCustormSignalTypesClass->setWindowTitle(QApplication::translate("QtCustormSignalTypesClass", "QtCustormSignalTypes", 0));
    } // retranslateUi

};

namespace Ui {
    class QtCustormSignalTypesClass: public Ui_QtCustormSignalTypesClass {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_QTCUSTORMSIGNALTYPES_H
