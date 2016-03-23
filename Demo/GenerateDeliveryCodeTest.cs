using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Core.TransactionServer.Agent.Util.Code;
using System.Diagnostics;

namespace Demo
{
    [TestFixture]
    public class GenerateDeliveryCodeTest
    {

        [Test]
        public void Test()
        {
            var code1 = DeliveryCodeGenerator.Default.Create();
            var code2 = DeliveryCodeGenerator.Default.Create();
            Debug.WriteLine(code1);
            Debug.WriteLine(code2);
            Assert.IsTrue(code1 != code2);
        }

    }
}
