// InsertSort.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

void insertSort(int a[],int length);

int _tmain(int argc, _TCHAR* argv[])
{
	int input[]={5,0,1,4,6,9,2,18,3};
	insertSort(input,9);
	for(int i=0;i<9;i++)
	{
		printf("%d  ",input[i]);
	}
	return 0;
}

void insertSort(int a[],int length)
{
	for(int i=1;i<length;i++)
	{
		int key = a[i];
		int j= i-1;
		while(j>=0 && a[j] > key)
		{
			a[j+1]= a[j];
			j--;
		}
		a[j+1]= key;
	}
	
}

