#ifndef PACKETBULDER
#define PACKETBULDER
#include <qbytearray.h>
#include "CommunicationObject.h"
class PacketBulder
{
public:
	static QByteArray* build(CommunicationObject* response);
};
#endif
