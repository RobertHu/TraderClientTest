using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Core.TransactionServer.Agent;

namespace Demo
{
    [TestFixture]
    public class UpdateTradingDataTest
    {


        [Test]
        public void SaveTest()
        {
            string tradingData = "";
            bool result = DatabaseHelper.SaveTradingData(tradingData, "");
            Assert.IsTrue(result);
        }

    }
}
