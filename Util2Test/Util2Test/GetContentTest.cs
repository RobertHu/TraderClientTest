using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfiguationPersistence;
using NUnit.Framework;
using System.Diagnostics;
namespace Util2Test
{
    [TestFixture]
    public class GetContentTest
    {
        [Test]
        public void GetContent()
        {
            string path = @"D:\iExchange\TradingConsoleMobile\Web.config";
            List<Tuple<string, string, string[]>> list = new List<Tuple<string, string, string[]>>();
            list.Add(Tuple.Create("appSettings", "add", new string[] { "key", "value" }));
            list.Add(Tuple.Create("system.serviceModel/services", "service", new string[] { "name", "address", "binding", "contract" }));

            list.Add(Tuple.Create("applicationSettings/TradingConsole.Mobile.Properties.Settings", "setting", new string[] { "name", "value" }));
            var target = FetchOriginContentModule.Get(path, list);
            Assert.IsNotNull(target);
            foreach (var item in target)
            {
                Debug.WriteLine("section={0},node={1},attrs={2}", item.Item1, item.Item2, string.Join("|", item.Item3));
                foreach (var subItem in item.Item4)
                {
                    foreach (var ssitem in subItem)
                    {
                        Debug.WriteLine("key={0};value={1}", ssitem.Key, ssitem.Value);
                    }
                    Debug.WriteLine("--------content---------------");
                }
                Debug.WriteLine("------------section--------------");
            }
        }

        [Test]
        public void GetConfigContent()
        {
            string path = @"D:\Teams\iExchangeCollection\iExchange3 Team\iExchange3\ConfigurationManager\Config\Security.xml";
            List<Tuple<string, string, string[]>> list = new List<Tuple<string, string, string[]>>();
            list.Add(Tuple.Create("appSettings", "add", new string[] { "key", "value","type","values" }));
            list.Add(Tuple.Create("SecuritySystemConfiguration", "add", new string[] { "key", "value", "type", "values" }));
            var target = FetchOriginContentModule.Get(path, list);
            Assert.IsNotNull(target);
            foreach (var item in target)
            {
                Debug.WriteLine("section={0},node={1},attrs={2}", item.Item1, item.Item2, string.Join("|", item.Item3));
                foreach (var subItem in item.Item4)
                {
                    foreach (var ssitem in subItem)
                    {
                        Debug.WriteLine("key={0};value={1}", ssitem.Key, ssitem.Value);
                    }
                    Debug.WriteLine("--------content---------------");
                }
                Debug.WriteLine("------------section--------------");
            }
        }
    }
}
