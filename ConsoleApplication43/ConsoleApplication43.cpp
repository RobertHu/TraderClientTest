// ConsoleApplication43.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

struct  A
{
	int value;
};

void Reasign(A* a);

int _tmain(int argc, _TCHAR* argv[])
{
	A *p = new A();
	p->value = 2;
	Reasign(p);
	return 0;
}

void Reasign(A* a)
{
	a = new A();
	a->value = 3;
}

