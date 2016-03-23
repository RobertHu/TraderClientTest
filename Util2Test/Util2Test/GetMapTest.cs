using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ConfiguationPersistence;
using System.Diagnostics;
namespace Util2Test
{
    [TestFixture]
   public class GetMapTest
    {
        [Test]
        public void Get()
        {
            string path=@"D:\Teams\iExchangeCollection\iExchange3 Team\iExchange3\ConfigurationManager\Config\Map.xml";
            var target = ConfigurationHelperModule.GetMapInfo(path);
            Assert.IsNotNull(target);

            foreach (var item in target)
            {
                Debug.WriteLine(string.Format("key={0},value={1}", item.Item1, item.Item2));
                try
                {
                    foreach (var subItem in item.Item3)
                    {
                        Debug.WriteLine(string.Format("section={0},node={1},attrs={2}", subItem.Item1, subItem.Item2, subItem.Item3));
                    }
                    Debug.WriteLine("---------------------------------");
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }
    }
}
