using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using Util;
namespace Util.Test
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void TestProduct()
        {
            IProductService productRepository = MockRepository.GenerateMock<IProductService>();
            productRepository.Stub(x => x.Quntity).Return(4);
            Assert.AreEqual(3, productRepository.Quntity);
        }

        [Test]
        public void CaculateHashTest()
        {
            string pwd1 = "hhb0403";
            string hash1 = HashHelper.EncodePassword(pwd1);
            string pwd2 = "hhb0402";
            string hash2 = HashHelper.EncodePassword(pwd2);
            Assert.AreEqual(hash1, hash2);
        }

    }
}
