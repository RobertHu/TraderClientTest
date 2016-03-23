#include "PacketParser.h"
#include "BytesLengthHelper.h"
#include <qjsondocument.h>
#include <qjsonobject.h>
#include <qdebug.h>
#include "InvokeManager.h"
#include "ResponseConstants.h"

PacketParser PacketParser::Default;

void PacketParser::run()
{
	while(!_stopped)
	{
		_mutex.lock();
		_packetAdded.wait(&_mutex);
		_mutex.unlock();
		while(!_packetQueue.empty())
		{
			_mutex.lock();
			QByteArray packet = _packetQueue.dequeue();
			_mutex.unlock();
			CommunicationObject* result = parse(packet);
			if(!InvokeManager::Default.exist(result->getInvokeId()))
			{
				qDebug() << "not a request response" << result->getInvokeId();
				continue;
			}
			RequestCondition* requestConditon =InvokeManager::Default.get(result->getInvokeId());
			bool isResponseError = result->getContent().contains(ResponseConstants::ErrorNodeName);
			requestConditon->setError(isResponseError);
			requestConditon->setContent(result->getContent());
			requestConditon->wakeAllForCondition();
		}
	}
}
CommunicationObject* PacketParser::parse(const QByteArray& packet)
{
	static const int headLength=6;
	static const int contentBytesLengthIndex = 2;
	static const int contentBytesLength = 4;
	static const int sessionLengthIndex = 1;
	qDebug() << "packet parser currentThreadid: "<<QThread::currentThreadId();
	QByteArray contentLengthBytes;
	contentLengthBytes.append(packet.constData() + contentBytesLengthIndex,contentBytesLength);
	int contentLength = BytesLengthHelper::getLength(contentLengthBytes.constData());
	int sessionLength = packet[sessionLengthIndex];
	int contentLengthIndex = headLength + sessionLength;
	QByteArray contentBytes;
	contentBytes.append(packet.constData() + contentLengthIndex);
	qDebug() << QString::fromUtf8(contentBytes);
	QJsonDocument jsonDoc = QJsonDocument::fromJson(contentBytes);
	QJsonObject content = jsonDoc.object();
	qDebug() << "json:  |" << QString::fromUtf8(jsonDoc.toJson());
	QByteArray sessionBytes;
	sessionBytes.append(packet.constData() + headLength,sessionLength);
	QString session = QString::fromUtf8(sessionBytes);
	QString invokeId = content["InvokeId"].toString();
	qDebug() << invokeId;
	CommunicationObject* request = new CommunicationObject(session,invokeId,content);
	return request;
}

void PacketParser::add(const QByteArray& packet)
{
	_mutex.lock();
	_packetQueue.append(packet);
	_mutex.unlock();
	_packetAdded.wakeOne();
}


void PacketParser::stop()
{
	_stopped = true;
}