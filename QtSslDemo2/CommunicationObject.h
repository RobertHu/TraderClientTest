#ifndef COMMUNICATIONOBJECT
#define COMMUNICATIONOBJECT
#include <qstring.h>
#include <qjsonobject.h>
class CommunicationObject
{
public:
	CommunicationObject(const QString& session, const QString& invokeId,const QJsonObject& content);
	~CommunicationObject(void);
	const QString& getSession() const {return _session;}
	const QString& getInvokeId() const {return _invokeId;}
	const QJsonObject& getContent() const {return _content;}
private:
	QString _session;
	QString _invokeId;
	QJsonObject _content;
};
#endif

