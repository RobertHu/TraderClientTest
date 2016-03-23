using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Xml.Linq;

namespace BasicUnitTest
{
    [TestFixture]
    public class XmlTest
    {
        [Test]
        public void DeliveryRequestTest()
        {
            string xml = "<Account><DeliveryRequests></DeliveryRequests></Account>";
            var xelement = XElement.Parse(xml);
            Assert.AreEqual(false, xelement.Element("DeliveryRequests").HasElements);
        }
    }
}
