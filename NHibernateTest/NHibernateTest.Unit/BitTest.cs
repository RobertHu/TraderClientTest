using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
namespace NHibernateTest.Unit
{
    [TestFixture]
    public class BitTest
    {
        [Test]
        public void LeftShiftTest()
        {
            int target = 1 << 12;
            Console.WriteLine(target);
            Assert.AreEqual(4096, target);
        }
    }
}
