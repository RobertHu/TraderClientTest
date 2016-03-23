#ifndef REQUESTCONDITION
#define REQUESTCONDITION
#include <qjsonobject.h>
#include <qwaitcondition.h>
#include <qmutex.h>
class RequestCondition
{
public:
	RequestCondition(void);
	~RequestCondition(void);
	void waitForCondition();
	void wakeAllForCondition();
	void wakeOneForCondition();
	void setError(bool isError);
	void setContent(const QJsonObject& content);
	const QJsonObject& getContent() const {return _content;}
	bool getIsError() const {return _isError;}
private:
	bool _isError;
	QJsonObject _content;
	QWaitCondition _waitCondition;
	QMutex _mutex;
};
#endif
