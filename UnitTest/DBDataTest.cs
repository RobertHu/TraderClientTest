using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Protocal;
using System.Data;
using System.Xml.Linq;
using System.IO;

namespace UnitTest
{
    [TestFixture]
    public class DBDataTest
    {
        static readonly string DBConnectionString = "data source=localhost;initial catalog=iExchange_V3;user id=sa;password=Omni1234;Connect Timeout=60";

        [Test]
        public void GetCodeSequenceTest()
        {
            List<DBParameter> dbParameters = new List<DBParameter>
            {
                new DBParameter("@codeType", 1),
                new DBParameter("@prefix", "MHL20151103"),
                new DBParameter("@sequenceNo", null, ParameterDirection.Output){DbType= SqlDbType.Int}
            };
            DataBaseHelper.GetData("Trading.[GetLastCode]", DBConnectionString, null, dbParameters);
            var item = dbParameters.Single(m => m.Direction == ParameterDirection.Output).OutPutValue;
            Assert.AreEqual(0, (int)item);
        }

        [Test]
        public void UpdateTradingData()
        {
            XElement root = XElement.Parse(File.ReadAllText("tradingData.xml"));
            List<DBParameter> dbParameters = new List<DBParameter>()
            {
                new DBParameter("@accountXml", root.ToString())
            };
            DataBaseHelper.GetData("Trading.UpdateTradingData", DBConnectionString, null, dbParameters);
        }

        [Test]
        public void UpdateInstrumentDayOpenCloseHistoryTest()
        {
            Dictionary<string, object> sqlParams = new Dictionary<string, object>
            {
                {"@tradeDay", DateTime.Parse("2016-01-12 00:00:00.000")},
                {"@instrumentID", Guid.Parse("83e72748-f3eb-48c6-9410-7e22aa3824a7")}
            };
            bool result = DataBaseHelper.ExecuteNonQuery("Trading.[GenerateRealValueDate]", DBConnectionString, sqlParams);
            Assert.IsTrue(result);
        }

    }
}
