using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace TraderClient.TypeExtensions
{
    internal static class XElementExtention
    {
        internal static string ParseContent(this XElement element, string key)
        {
            return element.Element(key).Value;
        }

        internal static XmlNode ToXmlNode(this XElement element)
        {
            using (XmlReader xmlReader = element.CreateReader())
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlReader);
                return xmlDoc;
            }
        }


    }
}
