using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FaxAndEmail.Helper;
using System.Diagnostics;
using System.IO;
namespace Util2Test
{
    [TestFixture]
    public class RiskLevelChangedTest
    {
        private Risk.RiskInnerService service;
        [TestFixtureSetUp]
        public void Initialize()
        {
            string path = @"D:\Teams\iExchangeCollection\iExchange3 Team\iExchange3\FaxAndEmailService\FaxFormat\data.xml";
            string macroPath = @"D:\Teams\iExchangeCollection\iExchange3 Team\iExchange3\FaxAndEmailService\FaxFormat\macro.xml";
            this.service = new Risk.RiskInnerService(path,macroPath);
            
        }
        [Test]
        public void GetDataTest()
        {
            var data = service.Data;
            Assert.IsNotNull(data);
            Assert.AreEqual(3, data.Count);
            foreach (var p in data)
            {
                Debug.WriteLine(p.Key);
                foreach (var q in p.Value)
                {
                    Debug.WriteLine(q.Key);
                    foreach (var item in q.Value)
                    {
                        Debug.WriteLine(string.Format("{0}----{1}", item.Key, item.Value));
                    }
                }
            }
        }

        [Test]
        public void TranslateTest()
        {
            string directory=@"D:\Teams\iExchangeCollection\iExchange3 Team\iExchange3\FaxAndEmailService\FaxFormat";
            string[] parameters = {"","RobertHu","11111","USD" ,DateTime.Now.ToString(),"Omnicare","4523.0","12.5","12312312312 34343534234234" };
            string language ="CHT";
            string fileName =string.Format("Risk.Style.{0}.txt",language);
            int riskLevel=3;
            string result = this.service.Translate(Path.Combine(directory, fileName), language, riskLevel.ToString(), parameters, true);
            Debug.WriteLine(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void FetchEmailSubjectTest()
        {
            string language = "CHT";
            string riskLevel = "3";
            string subject = this.service.Data[riskLevel]["Subject"][language];
            Debug.WriteLine(subject);
            Assert.IsNotNull(subject);
        }
    }
}
