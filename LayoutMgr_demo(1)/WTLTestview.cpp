// WTLTestView.cpp : implementation of the CWTLTestView class
//
/////////////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "resource.h"

#include "WTLTestView.h"

//=============================================================================
// 
//=============================================================================
const UINT CWTLTestView::_controlsToClip[] = 
	{
	IDC_COMBO1,
	IDC_LIST1,
	IDC_BUTTON1,
	IDC_BUTTON2,
	IDC_PS1,
	IDC_PS2,
	0
	};

//const UINT CAxWtlTestView::_controlsToClip[] = 
//	{
//	IDC_COMBO1,
//	IDC_MSCHART1,
//	IDC_BUTTON1,
//	IDC_BUTTON2,
//	IDC_PS1,
//	IDC_PS2,
//	0
//	};

CWTLTestView::CWTLTestView()
: _ps1(IDC_PS1, 0), _ps2(IDC_PS2, 1)
{
}


BOOL CWTLTestView::PreTranslateMessage(MSG* pMsg)
{
	return IsDialogMessage(pMsg);
}

LRESULT CWTLTestView::OnInitDialog(UINT uMsg, WPARAM wParam, LPARAM lParam, BOOL& bHandled)
{		
	_ps1.Create(*this);
	_ps2.Create(*this);
	return 0;
}

// This method allows to define the layout constraints
void CWTLTestView::DefineLayout()
{
	SetNPositions(2);

	AttachForm(IDC_TITLE, ATTACH_TOP);
	AttachForm(IDC_TITLE, ATTACH_LEFT);
	AttachForm(IDC_TITLE, ATTACH_RIGHT);
	
	AttachForm(IDC_GROUP1, ATTACH_LEFT);
	AttachPosition(IDC_GROUP1, ATTACH_RIGHT, 1);
	AttachForm(IDC_GROUP1, ATTACH_TOP);
	AttachForm(IDC_GROUP1, ATTACH_BOTTOM);

	AttachOppositeWidget(IDC_COMBO1, ATTACH_LEFT, IDC_GROUP1);
	AttachOppositeWidget(IDC_COMBO1, ATTACH_RIGHT, IDC_GROUP1);
	AttachOppositeWidget(IDC_COMBO1, ATTACH_TOP, IDC_GROUP1);

	AttachOppositeWidget(IDC_LIST1, ATTACH_LEFT, IDC_GROUP1);
	AttachOppositeWidget(IDC_LIST1, ATTACH_RIGHT, IDC_GROUP1);
	AttachOppositeWidget(IDC_LIST1, ATTACH_TOP, IDC_GROUP1);
	AttachOppositeWidget(IDC_LIST1, ATTACH_BOTTOM, IDC_GROUP1);

	AttachPosition(IDC_PS1, ATTACH_LEFT, 1);
	AttachForm(IDC_PS1, ATTACH_RIGHT);
	AttachForm(IDC_PS1, ATTACH_TOP);
	AttachPosition(IDC_PS1, ATTACH_BOTTOM, 1);

	AttachPosition(IDC_PS2, ATTACH_LEFT, 1);
	AttachForm(IDC_PS2, ATTACH_RIGHT);
	AttachPosition(IDC_PS2, ATTACH_TOP, 1);
	AttachForm(IDC_PS2, ATTACH_BOTTOM);

	AttachOppositeWidget(IDC_BUTTON1, ATTACH_HCENTER, IDC_PS2);
	AttachForm(IDC_BUTTON1, ATTACH_BOTTOM);

	AttachOppositeWidget(IDC_BUTTON2, ATTACH_HCENTER, IDC_PS2);
	AttachForm(IDC_BUTTON2, ATTACH_BOTTOM);
}
