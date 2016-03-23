#ifndef PACKETPARSER
#define PACKETPARSER
#include <qthread.h>
#include <qqueue.h>
#include <qbytearray.h>
#include <qmutex.h>
#include <qwaitcondition.h>
#include <CommunicationObject.h>
class PacketParser: public QThread
{
public:
	void run();
	void add(const QByteArray& packet);
	void stop();
	static PacketParser Default;
private:
	PacketParser(void):_stopped(false){}
	PacketParser(const PacketParser& parser){}
	QQueue<QByteArray> _packetQueue;
	QMutex _mutex;
	QWaitCondition _packetAdded;
	bool _stopped;
	CommunicationObject* parse(const QByteArray& packet);
	
};
#endif
