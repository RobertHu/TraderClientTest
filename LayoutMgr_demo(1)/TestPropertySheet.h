// TestPropertySheet.h: interface for the TestPropertySheet class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_TESTPROPERTYSHEET_H__6044F1BE_859E_4F2F_A836_8F85639E4D58__INCLUDED_)
#define AFX_TESTPROPERTYSHEET_H__6044F1BE_859E_4F2F_A836_8F85639E4D58__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "LayoutMgr.h"

class Page1 : public CAxResizablePropertyPageImpl<Page1>
{
public:
	typedef CAxResizablePropertyPageImpl<Page1> inherited;
	enum { IDD = IDD_PAGE1 };

	BEGIN_MSG_MAP(Page1)
		CHAIN_MSG_MAP(CAxResizablePropertyPageImpl<Page1>)
		MESSAGE_HANDLER(WM_INITDIALOG, OnInitDialog)
	END_MSG_MAP()

	//! Called during the initialisation of the dialog
	LRESULT OnInitDialog(UINT /*uMsg*/, WPARAM /*wParam*/, LPARAM /*lParam*/, BOOL& /*bHandled*/);
	//! This method allows to define the layout constraints
	virtual void DefineLayout();
	static const UINT _controlsToClip[];
};

class Page2 : public CResizablePropertyPageImpl<Page2>
{
public:
	enum { IDD = IDD_PAGE2 };

	BEGIN_MSG_MAP(Page2)
		CHAIN_MSG_MAP(CResizablePropertyPageImpl<Page2>)
		MESSAGE_HANDLER(WM_INITDIALOG, OnInitDialog)
	END_MSG_MAP()

	//! Called during the initialisation of the dialog
	LRESULT OnInitDialog(UINT /*uMsg*/, WPARAM /*wParam*/, LPARAM /*lParam*/, BOOL& /*bHandled*/);
	//! This method allows to define the layout constraints
	virtual void DefineLayout();
	static const UINT _controlsToClip[];
};

class TestPS : public CPopupResizablePropertySheetImpl<TestPS>
{
public:
	typedef CPopupResizablePropertySheetImpl<TestPS> dlgBase;
	//! Constructor
	TestPS();

	void OnInitDialog();

	BEGIN_MSG_MAP(TestPS)
		CHAIN_MSG_MAP(dlgBase)
	END_MSG_MAP()

private:
	Page1 _page1;
	Page2 _page2;
};

class TestPS2 : public CChildResizablePropertySheetImpl<TestPS2>
{
public:
	typedef CChildResizablePropertySheetImpl<TestPS2> dlgBase;

	//! Constructor
	TestPS2(UINT placeHolder, int activePage);

	BEGIN_MSG_MAP(TestPS)
		CHAIN_MSG_MAP(dlgBase)
	END_MSG_MAP()

private:
	Page1 _page1;
	Page2 _page2;
};

#endif // !defined(AFX_TESTPROPERTYSHEET_H__6044F1BE_859E_4F2F_A836_8F85639E4D58__INCLUDED_)
