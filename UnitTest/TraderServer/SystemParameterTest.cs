using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Trader.Server.Bll;
namespace UnitTest.TraderServer
{
    [TestFixture]
    public class SystemParameterTest
    {
        [Test]
        public void LoadTest()
        {
            SystemParameter.Default.Load("data source=ws0301;initial catalog=iExchange_V3;user id=sa;password=Omni1234;Connect Timeout=60");
            Assert.AreEqual(false,SystemParameter.Default.AllowMultipleLogin);
        }

    }
}
