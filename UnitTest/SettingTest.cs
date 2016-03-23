using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Xml.Linq;
using Core.TransactionServer.Agent.Settings;

namespace UnitTest
{
    [TestFixture]
    public class SettingTest
    {
        [Test]
        public void AddInstrumentTest()
        {
            string settingPath = "SettingsFile/setting.xml";
            XElement root = XElement.Load(settingPath);
            Setting.Default.Update(root);
            Assert.AreEqual(42, Setting.Default.InstrumentCount);
        }
    }
}
