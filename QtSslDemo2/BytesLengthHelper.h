#ifndef BYTESLENGTHHELPER
#define BYTESLENGTHHELPER
#include <qglobal.h>
#include <qbytearray.h>
class BytesLengthHelper
{
public:
	static QByteArray convertToBytes(int contentLength);
	static int getLength(const char* contentLengthBytes);
private:
	const static  int _bytesLength;
};
#endif

