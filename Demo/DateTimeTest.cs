using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Demo
{
    [TestFixture]
    public class DateTimeTest
    {
        [Test]
        public void TestDays()
        {
            DateTime dt1 = new DateTime(2015, 4, 3);
            DateTime dt2 = new DateTime(2015, 4, 28);
            DateTime dt3 = new DateTime(2015, 5, 6);
            Assert.AreEqual(25, (dt2 - dt1).Days);
            Assert.AreEqual(33, (dt3 - dt1).Days);
        }

    }
}
