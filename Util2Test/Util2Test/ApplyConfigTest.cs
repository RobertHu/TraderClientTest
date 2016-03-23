using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfiguationPersistence;
using NUnit.Framework;
namespace Util2Test
{
    [TestFixture]
    public class ApplyConfigTest
    {
        [Test]
        public void Apply()
        {
            string path = @"D:\iExchange\TradingConsoleMobile\Web.config";
            List<Tuple<string, string, string[], IEnumerable<Dictionary<string, string>>>> list = new List<Tuple<string, string, string[], IEnumerable<Dictionary<string, string>>>>();
            List<Dictionary<string, string>> dictList = new List<Dictionary<string, string>>();
            Dictionary<string, string> dict1 = new Dictionary<string, string>();
            dict1.Add("key", "StylesName");
            dict1.Add("value", "Robert111");
            dictList.Add(dict1);
            Dictionary<string, string> dict2 = new Dictionary<string, string>();
            dict2.Add("key", "ConnectionString");
            dict2.Add("value", "dfeeeeeeeeeeeeeee111");
            dictList.Add(dict2);

            List<Dictionary<string, string>> dictList2 = new List<Dictionary<string, string>>();
            Dictionary<string, string> dict3 = new Dictionary<string, string>();
            dict3.Add("name", "ParticipantServicesURL");
            dict3.Add("value", "111111111");
            dictList2.Add(dict3);

            Dictionary<string, string> dict4 = new Dictionary<string, string>();
            dict4.Add("name", "SecurityServicesURL");
            dict4.Add("value", "222222222");
            dictList2.Add(dict4);


            List<Dictionary<string, string>> dictList3 = new List<Dictionary<string, string>>();
            Dictionary<string, string> dict5 = new Dictionary<string, string>();
            dict5.Add("name", "TradingConsole.Mobile.Inner.CommandCollectService");
            dict5.Add("address", "3333");
            dict5.Add("binding", "4444");
            dict5.Add("contract", "5555");

            dictList3.Add(dict5);
            list.Add(Tuple.Create("appSettings", "add", new string[] { "key", "value" }, (IEnumerable<Dictionary<string, string>>)dictList));
            list.Add(Tuple.Create("applicationSettings/TradingConsole.Mobile.Properties.Settings", "setting", new string[] { "name", "value" }, (IEnumerable<Dictionary<string, string>>)dictList2));
            list.Add(Tuple.Create("system.serviceModel/services", "service", new string[] { "name", "address", "binding", "contract" }, (IEnumerable<Dictionary<string, string>>)dictList3));
            var result = ApplyConfigModule.Apply(path, list);
            Assert.IsTrue(result);
        }
    }
}
