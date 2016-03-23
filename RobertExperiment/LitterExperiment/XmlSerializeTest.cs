using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Drawing;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;
namespace LitterExperiment
{
    [TestFixture]
    public class XmlSerializeTest
    {
        [Test]
        public void MainTest()
        {
            MyCalendar calendar = new MyCalendar();
            List<MyEvent> listEvents = new List<MyEvent>();
            MyEvent e1 = new MyEvent();
            e1.Initialize("Event1", DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-8));
            MyEvent e2 = new MyEvent();
            e2.Initialize("Event2", DateTime.Now.AddDays(-7), DateTime.Now.AddDays(-5));
            MyEvent e3 = new MyEvent();
            e3.Initialize("Event3", DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-2));
            listEvents.Add(e1);
            listEvents.Add(e2);
            listEvents.Add(e3);
            calendar.Initialize("robert's calenda", true, Color.Yellow, listEvents);

            XmlSerializer serializer = new XmlSerializer(calendar.GetType());
            using (var ms = new MemoryStream())
            {
                serializer.Serialize(ms, calendar);
                string xml = Encoding.UTF8.GetString(ms.ToArray());
                Debug.WriteLine(xml);
                Assert.IsNotNullOrEmpty(xml);

            }
            


        }
    }
}
