#ifndef TEXTTHREAD
#define TEXTTHREAD
#include <qthread.h>
class TextThread : public QThread
{
public:
	TextThread(const QString &text);
	~TextThread(void);
	void run();
private:
	QString _text;
};
#endif