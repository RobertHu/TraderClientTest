using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
namespace Util2Test
{
    [TestFixture]
    public class StringTest
    {
        [Test]
        public void AggregateTest()
        {
            string[] source = new string[] { "one","two","there"};
            string target = source.Aggregate(string.Empty, (init, t) =>string.IsNullOrEmpty(init)?t:init+"|"+t);
            Assert.IsEmpty(target);
        }
    }
}
