/********************************************************************************
** Form generated from reading UI file 'qtssldemo2.ui'
**
** Created by: Qt User Interface Compiler version 5.1.0
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_QTSSLDEMO2_H
#define UI_QTSSLDEMO2_H

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

class Ui_QtSslDemo2Class
{
public:
    QMenuBar *menuBar;
    QToolBar *mainToolBar;
    QWidget *centralWidget;
    QStatusBar *statusBar;

    void setupUi(QMainWindow *QtSslDemo2Class)
    {
        if (QtSslDemo2Class->objectName().isEmpty())
            QtSslDemo2Class->setObjectName(QStringLiteral("QtSslDemo2Class"));
        QtSslDemo2Class->resize(600, 400);
        menuBar = new QMenuBar(QtSslDemo2Class);
        menuBar->setObjectName(QStringLiteral("menuBar"));
        QtSslDemo2Class->setMenuBar(menuBar);
        mainToolBar = new QToolBar(QtSslDemo2Class);
        mainToolBar->setObjectName(QStringLiteral("mainToolBar"));
        QtSslDemo2Class->addToolBar(mainToolBar);
        centralWidget = new QWidget(QtSslDemo2Class);
        centralWidget->setObjectName(QStringLiteral("centralWidget"));
        QtSslDemo2Class->setCentralWidget(centralWidget);
        statusBar = new QStatusBar(QtSslDemo2Class);
        statusBar->setObjectName(QStringLiteral("statusBar"));
        QtSslDemo2Class->setStatusBar(statusBar);

        retranslateUi(QtSslDemo2Class);

        QMetaObject::connectSlotsByName(QtSslDemo2Class);
    } // setupUi

    void retranslateUi(QMainWindow *QtSslDemo2Class)
    {
        QtSslDemo2Class->setWindowTitle(QApplication::translate("QtSslDemo2Class", "QtSslDemo2", 0));
    } // retranslateUi

};

namespace Ui {
    class QtSslDemo2Class: public Ui_QtSslDemo2Class {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_QTSSLDEMO2_H
