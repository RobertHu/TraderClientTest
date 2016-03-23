using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FaxAndEmail.Helper;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;
namespace HelperTest
{
    [TestFixture]
    public class XmlHelperTest
    {
        [Test]
        public void Get_XmlElement_Name_Content_Test()
        {
            string xml = @"
  <SMS>
    <UserName>95866920</UserName>
    <Password>s0d13b2taaa</Password>
  </SMS>
";
            XElement node = XElement.Parse(xml);
            var result = XmlModule.getXmlElementNodeNameAndContent(node);
            Assert.AreEqual(result.Item1, "SMS");
            Trace.WriteLine(result.Item1);
            Trace.WriteLine(result.Item2);
        }

        [Test]
        public void Get_XmlElement_Key_ValueTest()
        {
            string xml = @"
  <SMS>
    <UserName>95866920</UserName>
    <Password>s0d13b2taaa</Password>
  </SMS>
";
            XElement node = XElement.Parse(xml);
            var dict = XmlModule.getXmlElementKeyValue(node);
            Assert.AreEqual(2, dict.Keys.Count);
            Assert.AreEqual("95866920", dict["UserName"]);
            Assert.AreEqual("s0d13b2taaa", dict["Password"]);
        }
    }
}
