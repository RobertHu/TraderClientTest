using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using Domain;
namespace RhinoMock.Test
{
    [TestFixture]
    public class ProductTest
    {
        [Test]
        public void TestStubAbstract()
        {
            ProductBase product = MockRepository.GenerateStub<ProductBase>();
            product.Name = "Laptop Computer";
            product.Price = 3200.00m;
            Assert.AreEqual(3200.00m, product.Price);
        }

        [Test]
        public void TestStubInterface()
        {
            decimal price = 3200.00m;
            IProduct product = MockRepository.GenerateStub<IProduct>();
            product.Name = "Computer";
            product.Price = price;
            ProductManager.DoublePrice(product);
            Assert.AreEqual(price * 2, product.Price);
        }

        [Test]
        public void BarkTestStrickReplay()
        {
            MockRepository mocks = new MockRepository();
            Rover rover = mocks.StrictMock<Rover>();
            using (mocks.Record())
            {
                rover.Bark(17);
                LastCall.IgnoreArguments().Repeat.Times(2);
            }

            using (mocks.Playback())
            {
                rover.Bark(17);
                rover.Bark(23);
                //rover.Bark(17);
            }
        }


    }
}
