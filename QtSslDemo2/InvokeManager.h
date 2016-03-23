#ifndef INVOKEMANAGER
#define INVOKEMANAGER
#include <qhash.h>
#include <qreadwritelock.h>
#include "RequestCondition.h"
class InvokeManager
{
public:
	static InvokeManager Default;
	RequestCondition* get(const QString& key);
	QString generateKey();
	bool remove(const QString& key);
	bool exist(const QString& key);
private:
	InvokeManager(void);
	~InvokeManager(void);
	QHash<QString,RequestCondition*> _SignalContainer;
	QReadWriteLock _readWriteLock;
};
#endif
