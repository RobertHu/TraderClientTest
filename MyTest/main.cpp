#include "mytest.h"
#include <QtWidgets/QApplication>
#include <qdebug.h>
#include <quuid.h>
#include <qdatetime.h>
#include <iostream>
#include <iomanip>
#include <boost/multiprecision/cpp_dec_float.hpp>
#include <boost/optional/optional.hpp>
#include <sstream>
#include <qstring.h>
#include <qbytearray.h>
#include <qdom.h>
#include <algorithm>
#include <string>
#include <vector>

void testXml();

int main(int argc, char *argv[])
{
	QApplication app(argc, argv);
	testXml();

	MyTest w;
	w.show();
	return app.exec();
}

bool isRowDelimeter(char input)
{
	return input == '\n';
}

bool isColDelimeter(char input)
{
	return input == '\t';
}


void testXml()
{
	QString source = "<Commands FirstSequence=\"7883\" LastSequence=\"7883\"><Quotation Overrided=\"66adc06c-c5fe-4428-867f-be97650eb3b4	2013-12-03 17:23:03	1223.0	1221.4	1226.1	1219.6\" /></Commands>";
	QDomDocument doc;
	doc.setContent(source.toUtf8());
	QDomNodeList list = doc.elementsByTagName("Commands");
	QDomElement quotationElement = list.at(0).firstChildElement("Quotation");
	const char colDelimeter= '\t';
	const char rowDelimeter = '\n';
	if(quotationElement.hasAttribute("Overrided"))
	{
		std::string quotationString = quotationElement.attribute("Overrided").toStdString();
		typedef std::string::const_iterator iter;
		std::vector<std::string> quotations;
		iter i = quotationString.begin();
		iter end = quotationString.end();
		while(i<end)
		{
			iter j = std::find_if(i,end,isRowDelimeter);
			if(j < end)
			{
				quotations.push_back(std::string(i,j));
				i = j+ 1;
			}
			else
			{
				quotations.push_back(std::string(i, j));
				break;
			}
		}
		std::vector<std::string> parsePrice(const std::string& quotation);
		for(auto begin = quotations.begin(); begin!=quotations.end(); ++begin)
		{
			auto result = parsePrice(*begin);
			if(result.size() > 0)
			{
				qDebug() << "instrumentID: " << QString::fromStdString(result[0])
					<< "timestamp: " << QString::fromStdString(result[1])
					<< "ask: " << QString::fromStdString(result[2])
					<< "bid: " <<QString::fromStdString(result[3])
					<< "high: " <<QString::fromStdString(result[4])
					<< "low: " << QString:: fromStdString(result[5]);
			}
		}
	}

}


std::vector<std::string> parsePrice(const std::string& quotation)
{
	std::vector<std::string> result;
	auto i = quotation.begin();
	while(i < quotation.end())
	{
		auto j = std::find_if(i,quotation.end(),isColDelimeter);
		result.push_back(std::string(i,j));
		if( j == quotation.end())
		{
			break;
		}
		i = j + 1;
	}
	return result;
}



void testDecimal()
{

	QString guid = "63590fda-5d6f-4672-a7ad-05dc248006c6";
	QUuid uuid(guid);
	qDebug() << uuid.toString();
	QString dateTimeStr = "2013-11-18T14:07:48";
	QDateTime datetime = QDateTime::fromString(dateTimeStr,"yyyy-MM-ddThh:mm:ss");
	qDebug() << datetime.toString();

	namespace mp = boost::multiprecision;
	typedef mp::cpp_dec_float_100 decimal;
	decimal tiny("0.0000000000000000000000000000000000000000000001");
    decimal huge("100000000000000000000000000000000000000000000000");
	
    decimal a = tiny;         
	boost::optional<int> imax;
	qDebug() <<imax.is_initialized();
	imax = 2;
	qDebug() <<imax.is_initialized() << imax.get();

	std::stringstream ss;
	
    //while (a != huge)
    //{
    //    std::cout.precision(100);
    //    std::cout << std::fixed << a << '\n';
    //    a *= 10;
    //}    
}
