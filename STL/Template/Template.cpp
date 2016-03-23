// Template.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
template<int N>
struct Factorial
{
	enum{Value =N * Factorial<N-1>::Value};
};

template<>
struct Factorial<0>
{
	enum{Value = 1};
};




int _tmain(int argc, _TCHAR* argv[])
{
	std::cout << Factorial<5>::Value<< std::endl;
	return 0;
}

