using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using iExchange.Common.Caching.Transaction;

namespace TraderClient.CacheTest
{
    internal sealed class CacheFileTester
    {
        internal static readonly CacheFileTester Default = new CacheFileTester();

        private string _connectString = "data source=ws0202;initial catalog=iExchange_V3;user id=sa;password=Omni1234;Connect Timeout=60";

        static CacheFileTester() { }
        private CacheFileTester() { }


        internal void UpdateToDB()
        {
            string content = File.ReadAllText(@"CacheTest\content.xml");
            Guid accountId =Guid.Parse("4d0c8197-3a4a-4b21-9563-ff64c2e8e783");
            CacheCenter.Default.Initialize("CacheFiles", _connectString);
            CacheCenter.Default.Add(accountId, content, CacheType.Transaciton);
        }

    }
}
