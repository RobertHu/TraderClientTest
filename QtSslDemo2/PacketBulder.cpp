#include "PacketBulder.h"
#include <qjsondocument.h>
#include "BytesLengthHelper.h"


QByteArray* PacketBulder::build(CommunicationObject* response)
{
	const char jsonFormatFlag = 0x20;
	QByteArray sessionBytes = response->getSession().toUtf8();
	QJsonDocument jsonDoc;
	jsonDoc.setObject(response->getContent());
	delete response;
	QByteArray contentBytes = jsonDoc.toJson(QJsonDocument::Compact);
	char sessionLength = sessionBytes.size();
	int contentLength = contentBytes.size();
	QByteArray contentLengthBytes = BytesLengthHelper::convertToBytes(contentLength);
	QByteArray* packet = new QByteArray;
	packet->append(jsonFormatFlag);
	packet->append(sessionLength);
	packet->append(contentLengthBytes);
	packet->append(sessionBytes);
	packet->append(contentBytes);
	return packet;
}