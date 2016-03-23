using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;
using System.Diagnostics;

namespace UnitTest
{
    [TestFixture]
    public class XmlTest
    {
        [Test]
        public void GenerateInstrumentXmlTest()
        {
            List<string> instrumentIds = new List<string>
            {
                "1DFB99D4-2B76-48B0-9109-0A67265F5B9F",
                "2E42C798-97E7-4702-AFBA-0E6ABA0575D6",
                "36EA5E9D-A12C-45F4-AC5C-0F9D8E12BDF7",
                "0ADF8B7D-238D-4F29-8B13-14307FDA9701"
            };

            string tradeDay = DateTime.Now.Date.ToString("yyyy-MM-dd");

            XElement root = new XElement("Instruments");
            foreach (var eachId in instrumentIds)
            {
                var child = new XElement("Instrument");
                child.SetAttributeValue("ID", eachId);
                child.SetAttributeValue("TradeDay", tradeDay);
                root.Add(child);
            }
            Debug.WriteLine(root);
        }

    }
}
