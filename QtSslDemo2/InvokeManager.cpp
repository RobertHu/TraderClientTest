#include "InvokeManager.h"
#include <quuid.h>

InvokeManager InvokeManager::Default;

InvokeManager::InvokeManager(void)
{
}


InvokeManager::~InvokeManager(void)
{
	for(auto begin=_SignalContainer.begin(),end = _SignalContainer.end(); begin!=end;++begin)
	{
		delete begin.value();
	}
	_SignalContainer.clear();
}

bool InvokeManager::exist(const QString& key)
{
	QReadLocker locker(&_readWriteLock);
	return _SignalContainer.contains(key);
}


RequestCondition* InvokeManager::get(const QString& key)
{
	QReadLocker locker(&_readWriteLock);
	if(_SignalContainer.contains(key))
		return _SignalContainer.value(key);
	return nullptr;
}

QString InvokeManager::generateKey()
{
	QWriteLocker locker(&_readWriteLock);
	QString key = QUuid::createUuid().toString();
	_SignalContainer.insert(key,new RequestCondition);
	return key;
}

bool InvokeManager::remove(const QString& key)
{
	QWriteLocker locker(&_readWriteLock);
	if(_SignalContainer.contains(key))
	{
		RequestCondition* condition = _SignalContainer.value(key);
		_SignalContainer.remove(key);
		delete condition;
		return true;
	}
	return false;
}