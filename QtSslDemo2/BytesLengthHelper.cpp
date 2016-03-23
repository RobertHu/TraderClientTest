#include "BytesLengthHelper.h"


const int BytesLengthHelper::_bytesLength = 4;

QByteArray BytesLengthHelper::convertToBytes(int contentLength)
{
	const int mask = 0x000000ff;
	const int byteLength = 8;
	QByteArray result;
	result.resize(_bytesLength);
	for(int i=0;i< _bytesLength;i++)
	{
		result[i] = contentLength & mask;
		contentLength >>=  byteLength;
	}
	return result;
}

int BytesLengthHelper::getLength(const char* contentLengthBytes)
{
	int result = 0;
	const int byteLength = 8;
	const unsigned char byteMask = 0xff;
	for(int i=0;i<_bytesLength;i++)
	{
		result += (contentLengthBytes[i] & byteMask) << ( i * byteLength);
	}
	return result;

}