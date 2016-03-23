using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Diagnostics;

namespace Demo
{
    internal sealed class Transaction
    {
        private List<Order> _orders = new List<Order>();

        internal List<Order> Orders
        {
            get { return _orders; }
        }

        internal void AddOrder(Order order)
        {
            _orders.Add(order);
        }

    }

    internal class Order
    {

    }

    internal sealed class PhysicalOrder : Order
    {
    }




    [TestFixture]
    public class OrderCastTest
    {
        [Test]
        public void TestCastTo()
        {
            Transaction tran = new Transaction();
            tran.AddOrder(new PhysicalOrder());
            tran.AddOrder(new PhysicalOrder());
            Assert.IsTrue(tran.Orders.Count == 2);
            foreach (PhysicalOrder order in tran.Orders)
            {
                Debug.WriteLine(order.GetType());
            }
        }
    }
}
