#include "RequestCondition.h"


RequestCondition::RequestCondition(void):_isError(false)
{
}

RequestCondition::~RequestCondition(void)
{
}

void RequestCondition::setError(bool isError)
{
	QMutexLocker locker(&_mutex);
	_isError = isError;
}


void RequestCondition::setContent(const QJsonObject& content)
{
	QMutexLocker locker(&_mutex);
	_content = content;
}

void RequestCondition::waitForCondition()
{
	_mutex.lock();
	_waitCondition.wait(&_mutex);
	_mutex.unlock();
}


void RequestCondition::wakeAllForCondition()
{
	_waitCondition.wakeAll();
}

void RequestCondition::wakeOneForCondition()
{
	_waitCondition.wakeOne();
}