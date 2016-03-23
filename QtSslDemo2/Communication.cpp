#include "Communication.h"
#include <qdebug.h>
#include "BytesLengthHelper.h"
#include "PacketParser.h"

Communication Communication::Default;

Communication::Communication():_leftLengthNeedToRead(0),_readHead(true),_readContent(false)
{
	_socket = new QSslSocket(this);
}


void  Communication::startConnect(const QString& host,int port)
{
	connect(_socket,SIGNAL(readyRead()),this,SLOT(readyToRead()));
	_socket->connectToHostEncrypted(host,port);
	_socket->ignoreSslErrors();
	if(_socket->waitForEncrypted(100000))
	{
		qDebug() << "ok connected";
		qDebug() << "currentThreadid: "<<QThread::currentThreadId();
	}
	else
	{
		qDebug() << "connecting failed";
	}
}


void Communication::readyToRead()
{
	const static int headLength = 6;
	qDebug() << "readyToRead currentThreadid: "<<QThread::currentThreadId();
	if(_readHead && _socket->bytesAvailable() < headLength)
		return;
	if(_readHead)
	{
		QByteArray headBytes = _socket->read(headLength);
		Q_ASSERT(headBytes.size() == headLength);
		_headBytes.clear();
		_headBytes.append(headBytes);
		int packetLength = parsePacketLength(headBytes);
		_leftLengthNeedToRead = packetLength - headLength;
		enableReadContent();
	}
	if(_readContent &&_socket->bytesAvailable() < _leftLengthNeedToRead)
		return;
	if(_readContent)
	{
		QByteArray contentBytes = _socket->read(_leftLengthNeedToRead);
		Q_ASSERT(contentBytes.size() == _leftLengthNeedToRead);
		QByteArray packet;
		packet.append(_headBytes);
		packet.append(contentBytes);
		PacketParser::Default.add(packet);
		enableReadHead();
	}
}

void Communication::enableReadHead()
{
	_readHead = true;
	_readContent = false;
}

void Communication::enableReadContent()
{
	_readContent = true;
	_readHead = false;
}

int Communication::parsePacketLength(QByteArray& headBytes)
{
	const static int sessionLengthIndex = 1;
	const static int contentLengthIndex = 2;
	const static int contentLengthBytesSize = 4;
	const static int headLength=6;
	int sessionLength = headBytes[1];
	QByteArray contentLengthBytes;
	contentLengthBytes.append(headBytes.constData() + contentLengthIndex,contentLengthBytesSize);
	int contentLength = BytesLengthHelper::getLength(contentLengthBytes.constData());
	int packetLength = headLength + sessionLength + contentLength;
	return packetLength;
}

void Communication::sendPacket(QByteArray* packet)
{
	_socket->write(*packet);
	delete packet;
}


