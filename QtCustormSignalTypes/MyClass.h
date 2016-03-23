#pragma once
#include <qstring.h>
class MyClass
{
public:
	MyClass(const QString& desc):
		_desc(desc)
	{
	}
	QString getDesc() const
	{
		return _desc;
	}
private:
	QString _desc;
};

