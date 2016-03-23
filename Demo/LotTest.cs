using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Demo
{
    [TestFixture]
    public class LotTest
    {
        [Test]
        public void SplitLotTest()
        {
            decimal lot = 4.5m;
            int lotIntegerPart = (int)lot;
            decimal lotRemain = lot - lotIntegerPart;
            Assert.AreEqual(4, lotIntegerPart);
            Assert.AreEqual(0.5m, lotRemain);
        }
    }
}
