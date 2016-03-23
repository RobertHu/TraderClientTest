#ifndef SENDCENTER
#define SENDCENTER
#include <qthread.h>
#include <qmutex.h>
#include <qwaitcondition.h>
#include <qqueue.h>
#include <qbytearray.h>

class SendCenter : public QThread
{
	Q_OBJECT
public:
	static SendCenter Default;
	SendCenter(void);
	~SendCenter(void);
	void run();
	void add(QByteArray* packet);
	void stop();

signals:
	 void sendPacket(QByteArray* packet);

private:
	QQueue<QByteArray*> _packets;
	QMutex _mutex;
	QWaitCondition _packetAdded;
	bool _stopped;
};
#endif

