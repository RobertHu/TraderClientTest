// WTLTestView.h : interface of the CWTLTestView class
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_WTLTESTVIEW_H__4F1D7FE8_847B_482A_B13C_BD10208F372A__INCLUDED_)
#define AFX_WTLTESTVIEW_H__4F1D7FE8_847B_482A_B13C_BD10208F372A__INCLUDED_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

#include "LayoutMgr.h"
#include "TestPropertySheet.h"

class CWTLTestView : public CResizableFormViewImpl<CWTLTestView>
{
public:
	enum { IDD = IDD_WTLTEST_FORM };
	typedef CResizableFormViewImpl<CWTLTestView> dlgBase;

	CWTLTestView();

	BOOL PreTranslateMessage(MSG* pMsg);
	LRESULT OnInitDialog(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled);
	// This method allows to define the layout constraints
	virtual void DefineLayout();

	BEGIN_MSG_MAP(CWTLTestView)
		CHAIN_MSG_MAP(dlgBase)
		MESSAGE_HANDLER(WM_INITDIALOG, OnInitDialog)
	END_MSG_MAP()
	
	TestPS2	_ps1;
	TestPS2	_ps2;
	static const UINT _controlsToClip[];
};


#endif // !defined(AFX_WTLTESTVIEW_H__4F1D7FE8_847B_482A_B13C_BD10208F372A__INCLUDED_)
