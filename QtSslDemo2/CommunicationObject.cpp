#include "CommunicationObject.h"


CommunicationObject:: CommunicationObject(const QString& session, const QString& invokeId,const QJsonObject& content)
	:_session(session),_invokeId(invokeId),_content(content)
{
}

CommunicationObject::~CommunicationObject(void)
{
}
