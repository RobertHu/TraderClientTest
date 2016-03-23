using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;
namespace NHibernateTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            Stream m = p.GetType().Assembly.GetManifestResourceStream("NHibernateTest.Book.xml");
            using (XmlTextReader xmlReader = new XmlTextReader(m))
            {
                XElement xel = XElement.Load(xmlReader);
                var authorXel = xel.Element("Author");
                var titleXel = xel.Element("Title");
                Console.WriteLine(authorXel.Value);
                Console.WriteLine(titleXel.Value);
            }
            Console.Read();
        }
    }
}
