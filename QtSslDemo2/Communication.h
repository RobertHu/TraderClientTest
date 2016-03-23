#ifndef COMMUNICATION
#define COMMUNICATION
#include <qthread.h>
#include <qsslsocket.h>
#include <qmutex.h>
#include <qbytearray.h>
#include <qwaitcondition.h>
class Communication :public QObject
{
	Q_OBJECT
public:
	static Communication Default;
	Communication(void);
	QSslSocket* getSocket() const {return _socket;}
	
public slots:
	void startConnect(const QString& host,int port);
	void sendPacket(QByteArray* packet);

private slots:
	void readyToRead();

private:
	QSslSocket* _socket;
	QMutex _mutex;
	bool _readHead;
	bool _readContent;
	int _leftLengthNeedToRead;
	QByteArray _headBytes;
	int parsePacketLength(QByteArray& headBytes);
	void enableReadHead();
	void enableReadContent();
};
#endif

