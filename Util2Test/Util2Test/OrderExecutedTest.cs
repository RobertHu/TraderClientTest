using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FaxAndEmail.Helper;
using System.IO;
using System.Diagnostics;
namespace Util2Test
{
    [TestFixture]
    public class OrderExecutedTest
    {
        private Order.OrderInnerService service;
        [TestFixtureSetUp]
        public void Inialize()
        {
            string path = @"D:\Teams\iExchangeCollection\iExchange3 Team\iExchange3\FaxAndEmailService\FaxFormat\data.xml";
            string macroPath = @"D:\Teams\iExchangeCollection\iExchange3 Team\iExchange3\FaxAndEmailService\FaxFormat\macro.xml";
            service = new Order.OrderInnerService(path,macroPath);
        }

        [Test]
        public void TranslateTest()
        {
            string directory = @"D:\Teams\iExchangeCollection\iExchange3 Team\iExchange3\FaxAndEmailService\FaxFormat";
            string[] parameters = { "", "Bill", "Jobs", "ts123433", "USD/JPY", "45.6", "----", "3.25", "25.2", DateTime.Now.ToString(), "Omnicare" };
            string lanuage = "CHS";
            string stylePath = Path.Combine(directory,string.Format("Order.Style.{0}.txt",lanuage));
            string result = this.service.Translate(stylePath, parameters,false,lanuage);
            Debug.WriteLine(result);
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetSubjectTest()
        {
            string language = "ENG";
            string result = this.service.Data["Subject"][language];
            Debug.WriteLine(result);
            Assert.IsNotNull(result);
        }
    }
}
