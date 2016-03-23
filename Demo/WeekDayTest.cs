using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Demo
{
    [TestFixture]
    public class WeekDayTest
    {

        [Test]
        public void Test()
        {
            DateTime day1 = DateTime.Now;
            DateTime day2 = day1.AddDays(-1);
            Assert.AreEqual(DayOfWeek.Thursday, day1.DayOfWeek);
            Assert.AreEqual(DayOfWeek.Wednesday, day2.DayOfWeek);
        }

    }
}
