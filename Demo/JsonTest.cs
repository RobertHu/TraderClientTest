using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;
using Newtonsoft.Json;
using Protocal.TradingInstrument;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace Demo
{


    public class TradingSession
    {
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
    }

    public class RootObject
    {
        public string Id { get; set; }
        public string TradeDay { get; set; }
        public string DayOpenTime { get; set; }
        public string DayCloseTime { get; set; }
        public bool IsTrading { get; set; }
        public object Code { get; set; }
        public List<TradingSession> TradingSessions { get; set; }
    }


    [TestFixture]
    public sealed class JsonTest
    {
        [Test]
        public void DeserializeInstrumentStatusTest()
        {
            string jsonContent = File.ReadAllText(@"D:\VsProjects\Demo\test\instrumentState.txt");
            Debug.WriteLine(jsonContent);
            JArray jarray = JArray.Parse(jsonContent);
            JObject jobject = (JObject)jarray[2];
            List<TradingSession> sessions = JsonConvert.DeserializeObject<List<TradingSession>>(jobject["TradingSessions"].ToString());
            Assert.IsTrue(sessions != null && sessions.Count > 0);
            foreach (var eachSession in sessions)
            {
                Debug.WriteLine(string.Format("{0}   --------  {1}", eachSession.BeginTime, eachSession.EndTime));
            }
        }

        [Test]
        public void DeserializeUseTrace()
        {
            string jsonContent = File.ReadAllText(@"D:\VsProjects\Demo\test\instrumentState.txt");
            var result = JsonConvert.DeserializeObject<List<InstrumentDayOpenCloseParams>>(jsonContent);
            foreach (var eachItem in result)
            {
                Assert.IsTrue(eachItem.TradingSessions != null);
            }
        }

    }
}
