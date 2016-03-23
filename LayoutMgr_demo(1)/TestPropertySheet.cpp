// TestPropertySheet.cpp: implementation of the TestPropertySheet class.
//
//////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "resource.h"
#include "TestPropertySheet.h"

//=============================================================================
// 
//=============================================================================
const UINT Page1::_controlsToClip[] = 
	{
	IDC_EDIT1,
	0
	};

LRESULT Page1::OnInitDialog(UINT /*uMsg*/, WPARAM /*wParam*/, LPARAM /*lParam*/, BOOL& /*bHandled*/)
{
	return 0;
}
	
void Page1::DefineLayout()
{
	SetNPositions(2);
	AttachForm(IDC_EDIT1, ATTACH_LEFT);
	AttachForm(IDC_EDIT1, ATTACH_RIGHT);
	AttachPosition(IDC_EDIT1, ATTACH_TOP, 0);
	AttachPosition(IDC_EDIT1, ATTACH_BOTTOM, 1);

	AttachForm(IDC_MSCHART1, ATTACH_LEFT);
	AttachForm(IDC_MSCHART1, ATTACH_RIGHT);
	AttachPosition(IDC_MSCHART1, ATTACH_TOP, 1);
	AttachPosition(IDC_MSCHART1, ATTACH_BOTTOM, 2);
}

//=============================================================================
// 
//=============================================================================
const UINT Page2::_controlsToClip[] = 
	{
	IDC_LABEL,
	IDC_EDIT1,
	IDC_LABEL2,
	IDC_EDIT2,
	IDC_BUTTON1,
	0
	};

LRESULT Page2::OnInitDialog(UINT /*uMsg*/, WPARAM /*wParam*/, LPARAM /*lParam*/, BOOL& /*bHandled*/)
{
	return 0;
}

void Page2::DefineLayout()
{
	AttachForm(IDC_LABEL, ATTACH_LEFT);
	AttachForm(IDC_LABEL, ATTACH_RIGHT);
	AttachForm(IDC_LABEL, ATTACH_VCENTER);

	AttachForm(IDC_EDIT1, ATTACH_LEFT);
	AttachForm(IDC_EDIT1, ATTACH_RIGHT);
	AttachForm(IDC_EDIT1, ATTACH_VCENTER);

	AttachForm(IDC_LABEL2, ATTACH_LEFT);
	AttachForm(IDC_LABEL2, ATTACH_RIGHT);
	AttachForm(IDC_LABEL2, ATTACH_VCENTER);

	AttachForm(IDC_EDIT2, ATTACH_LEFT);
	AttachForm(IDC_EDIT2, ATTACH_RIGHT);
	AttachForm(IDC_EDIT2, ATTACH_VCENTER);

	AttachForm(IDC_BUTTON1, ATTACH_HCENTER);
	AttachForm(IDC_BUTTON1, ATTACH_VCENTER);
}

//=============================================================================
// 
//=============================================================================
TestPS::TestPS()
{
	AddPage(_page1);
	AddPage(_page2);
	SetActivePage(0);
}

void TestPS::OnInitDialog()
{
	CenterWindow(GetParent());
}

//=============================================================================
// 
//=============================================================================
TestPS2::TestPS2(UINT placeHolder, int activePage)
: dlgBase(placeHolder)
{
	AddPage(_page1);
	AddPage(_page2);
	SetActivePage(activePage);
}

