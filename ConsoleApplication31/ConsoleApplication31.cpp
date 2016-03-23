// ConsoleApplication31.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <stdio.h>
#include <conio.h>
#include <Python.h>

int _tmain(int argc, _TCHAR* argv[])
{
	char fileName[] = "test1.py";
	FILE* fp = NULL;
	char myString[200];
	Py_Initialize();
	fp = fopen(fileName,"r");
	/*if(fp!=NULL)
	{
		fgets(myString,200,fp);
		puts(myString);
	}*/
	PyRun_SimpleFile(fp,fileName);
	Py_Finalize();
	return 0;
}

