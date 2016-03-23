// aboutdlg.cpp : implementation of the CAboutDlg class
//
/////////////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "resource.h"

#include "aboutdlg.h"

//=============================================================================
// 
//=============================================================================
const UINT CAboutDlg::_controlsToClip[] = 
	{
	IDC_IMG,
	IDOK,
	0
	};

LRESULT CAboutDlg::OnInitDialog(UINT /*uMsg*/, WPARAM /*wParam*/, LPARAM /*lParam*/, BOOL& /*bHandled*/)
{
	AttachForm(IDOK, ATTACH_RIGHT);
	AttachForm(IDOK, ATTACH_BOTTOM);
	AttachForm(IDC_FRAME, ATTACH_LEFT);
	AttachForm(IDC_FRAME, ATTACH_TOP);
	AttachForm(IDC_FRAME, ATTACH_RIGHT);
	AttachForm(IDC_FRAME, ATTACH_BOTTOM);
	AttachForm(IDC_IMG, ATTACH_HCENTER);
	AttachForm(IDC_IMG, ATTACH_VCENTER);
	AttachForm(IDC_LABEL, ATTACH_HCENTER);
	AttachForm(IDC_LABEL, ATTACH_VCENTER);

	CenterWindow(GetParent());
	return TRUE;
}

LRESULT CAboutDlg::OnCloseCmd(WORD /*wNotifyCode*/, WORD wID, HWND /*hWndCtl*/, BOOL& /*bHandled*/)
{
	EndDialog(wID);
	return 0;
}

//=============================================================================
// 
//=============================================================================
const UINT CAxAboutDlg::_controlsToClip[] = 
	{
	IDC_MSCHART1,
	IDOK,
	0
	};
LRESULT CAxAboutDlg::OnInitDialog(UINT /*uMsg*/, WPARAM /*wParam*/, LPARAM /*lParam*/, BOOL& /*bHandled*/)
{
	AttachForm(IDOK, ATTACH_RIGHT);
	AttachForm(IDOK, ATTACH_BOTTOM);
	AttachForm(IDC_FRAME, ATTACH_LEFT);
	AttachForm(IDC_FRAME, ATTACH_TOP);
	AttachForm(IDC_FRAME, ATTACH_RIGHT);
	AttachForm(IDC_FRAME, ATTACH_BOTTOM);
	AttachForm(IDC_MSCHART1, ATTACH_LEFT);
	AttachForm(IDC_MSCHART1, ATTACH_TOP);
	AttachForm(IDC_MSCHART1, ATTACH_RIGHT);
	AttachForm(IDC_MSCHART1, ATTACH_BOTTOM);
	AttachForm(IDC_LABEL, ATTACH_HCENTER);
	AttachForm(IDC_LABEL, ATTACH_BOTTOM);

	CenterWindow(GetParent());
	return TRUE;
}

LRESULT CAxAboutDlg::OnCloseCmd(WORD /*wNotifyCode*/, WORD wID, HWND /*hWndCtl*/, BOOL& /*bHandled*/)
{
	EndDialog(wID);
	return 0;
}
