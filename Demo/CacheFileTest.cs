using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using iExchange.Common.Caching;
using iExchange.Common.Caching.Transaction;

namespace Demo
{
    [TestFixture]
    public class CacheFileTest
    {
        [TestFixtureSetUp]
        public void Init()
        {
           // Assistant.Instance.Initialize(@"D:\VsProjects\Demo\CacheFile", "");
            CacheCenter.Default.Initialize(@"D:\Teams\iExchangeCollection\iExchange3 Team\iExchange3Promotion\TransactionServerTest\bin\Debug\CacheFiles", "");
        }

        [Test]
        public void AddFileTest()
        {
            Guid accountId = Guid.Parse("606d464f-4b7a-4481-97d0-00cf23c8dae9");
            string rawData = "testData, testData , testData, testData, testData, testData, testData";
            CacheCenter.Default.Add(accountId, rawData, CacheType.Transaciton);
        }

    }
}
