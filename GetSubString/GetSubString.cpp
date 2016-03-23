// GetSubString.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <string>
#include <algorithm>
#include <iostream>
using namespace std;

string getCommon(string& bid,string& ask);


int _tmain(int argc, _TCHAR* argv[])
{
	string ask = "1.4579";
	string bid = "1.3468";
	string common=getCommon(bid,ask);
	cout << common <<"  bid: " << bid << " ask:  " << ask << endl;
	return 0;
}



string getCommon(string& bid, string& ask)
{
	if(bid.empty() || ask.empty())
	{
		return "";
	}
	string result;
	auto iterBid= bid.begin();
	auto iterAsk = ask.begin();
	for(;iterBid <bid.end()&&iterAsk < ask.end(); ++iterBid, ++iterAsk)
	{
		if(*iterBid!=*iterAsk)
		{
			break;
		}
		result.push_back(*iterBid);
	}
	if(result.empty())
	{
		return "";
	}
	bid=bid.substr(result.size());
	ask = ask.substr(result.size());
	return result;
}