// StaddardTemplateLibrary.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <vector>
#include <algorithm>
#include <iterator>
using namespace std;

int _tmain(int argc, _TCHAR* argv[])
{
	vector<int> col;
	for(int i=0;i<9;i++)
		col.push_back(i);
	copy(col.rbegin(),col.rend(),std::ostream_iterator<int>(cout," "));
	cout <<endl;
	return 0;
}

