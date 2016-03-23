/********************************************************************************
** Form generated from reading UI file 'luademo1.ui'
**
** Created by: Qt User Interface Compiler version 5.1.0
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_LUADEMO1_H
#define UI_LUADEMO1_H

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

class Ui_LuaDemo1Class
{
public:
    QMenuBar *menuBar;
    QToolBar *mainToolBar;
    QWidget *centralWidget;
    QStatusBar *statusBar;

    void setupUi(QMainWindow *LuaDemo1Class)
    {
        if (LuaDemo1Class->objectName().isEmpty())
            LuaDemo1Class->setObjectName(QStringLiteral("LuaDemo1Class"));
        LuaDemo1Class->resize(600, 400);
        menuBar = new QMenuBar(LuaDemo1Class);
        menuBar->setObjectName(QStringLiteral("menuBar"));
        LuaDemo1Class->setMenuBar(menuBar);
        mainToolBar = new QToolBar(LuaDemo1Class);
        mainToolBar->setObjectName(QStringLiteral("mainToolBar"));
        LuaDemo1Class->addToolBar(mainToolBar);
        centralWidget = new QWidget(LuaDemo1Class);
        centralWidget->setObjectName(QStringLiteral("centralWidget"));
        LuaDemo1Class->setCentralWidget(centralWidget);
        statusBar = new QStatusBar(LuaDemo1Class);
        statusBar->setObjectName(QStringLiteral("statusBar"));
        LuaDemo1Class->setStatusBar(statusBar);

        retranslateUi(LuaDemo1Class);

        QMetaObject::connectSlotsByName(LuaDemo1Class);
    } // setupUi

    void retranslateUi(QMainWindow *LuaDemo1Class)
    {
        LuaDemo1Class->setWindowTitle(QApplication::translate("LuaDemo1Class", "LuaDemo1", 0));
    } // retranslateUi

};

namespace Ui {
    class LuaDemo1Class: public Ui_LuaDemo1Class {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_LUADEMO1_H
