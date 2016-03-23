#include "TextThread.h"
#include <qdebug.h>

#include <qmutex.h>

bool stopThreads = false;
QMutex qMutex;

TextThread::TextThread(const QString& text):_text(text)
{

}


TextThread::~TextThread(void)
{

}


void TextThread::run()
{
	while(true)
	{
		qMutex.lock();
		if(stopThreads)
		{
			qMutex.unlock();
			break;
		}
		qDebug() << _text;
		sleep(1);
		qMutex.unlock();
	}
}
