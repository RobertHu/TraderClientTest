#include "SendCenter.h"
#include "Communication.h"

SendCenter SendCenter::Default;

SendCenter::SendCenter(void):_stopped(false)
{
	connect(this,&SendCenter::sendPacket,&Communication::Default,&Communication::sendPacket,Qt::BlockingQueuedConnection);
}

SendCenter::~SendCenter(void)
{
	while(!_packets.empty())
	{
		delete _packets.dequeue();
	}
}

void SendCenter::run()
{
	while(!_stopped)
	{
		_mutex.lock();
		_packetAdded.wait(&_mutex);
		_mutex.unlock();
		while(!_packets.empty())
		{
			_mutex.lock();
			QByteArray* packet = _packets.dequeue();
			_mutex.unlock();
			emit sendPacket(packet);
			qDebug() << "send a packet";
		}
	}
}


void SendCenter::stop()
{
	_stopped = true;
}

void SendCenter::add(QByteArray* packet)
{
	QMutexLocker lock(&_mutex);
	_packets.enqueue(packet);
	_packetAdded.wakeAll();
}