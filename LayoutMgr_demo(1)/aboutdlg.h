// aboutdlg.h : interface of the CAboutDlg class
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_ABOUTDLG_H__DB514AD6_E441_4774_AA30_8993004F60DE__INCLUDED_)
#define AFX_ABOUTDLG_H__DB514AD6_E441_4774_AA30_8993004F60DE__INCLUDED_

#include "LayoutMgr.h"

class CAboutDlg : public CResizableDialogImpl<CAboutDlg>
{
public:
	enum { IDD = IDD_ABOUTBOX };
	typedef CResizableDialogImpl<CAboutDlg> inherited;

	BEGIN_MSG_MAP(CAboutDlg)
		CHAIN_MSG_MAP(inherited)
		MESSAGE_HANDLER(WM_INITDIALOG, OnInitDialog)
		COMMAND_ID_HANDLER(IDOK, OnCloseCmd)
		COMMAND_ID_HANDLER(IDCANCEL, OnCloseCmd)
	END_MSG_MAP()

	LRESULT OnInitDialog(UINT /*uMsg*/, WPARAM /*wParam*/, LPARAM /*lParam*/, BOOL& /*bHandled*/);
	LRESULT OnCloseCmd(WORD /*wNotifyCode*/, WORD wID, HWND /*hWndCtl*/, BOOL& /*bHandled*/);

	static const UINT _controlsToClip[];
};

class CAxAboutDlg : public CAxResizableDialogImpl<CAxAboutDlg>
{
public:
	enum { IDD = IDD_AX_ABOUTBOX };
	typedef CAxResizableDialogImpl<CAxAboutDlg> inherited;

	BEGIN_MSG_MAP(CAxAboutDlg)
		CHAIN_MSG_MAP(inherited)
		MESSAGE_HANDLER(WM_INITDIALOG, OnInitDialog)
		COMMAND_ID_HANDLER(IDOK, OnCloseCmd)
		COMMAND_ID_HANDLER(IDCANCEL, OnCloseCmd)
	END_MSG_MAP()

	LRESULT OnInitDialog(UINT /*uMsg*/, WPARAM /*wParam*/, LPARAM /*lParam*/, BOOL& /*bHandled*/);
	LRESULT OnCloseCmd(WORD /*wNotifyCode*/, WORD wID, HWND /*hWndCtl*/, BOOL& /*bHandled*/);

	static const UINT _controlsToClip[];
};

#endif // !defined(AFX_ABOUTDLG_H__DB514AD6_E441_4774_AA30_8993004F60DE__INCLUDED_)
