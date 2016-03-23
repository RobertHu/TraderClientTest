using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Core.TransactionServer.Agent.Framework;

namespace BasicUnitTest
{
    internal sealed class NecessaryCalculator
    {
        internal NecessaryCalculator()
        {
            this.Margin = BuySellPair.Empty;
        }

        internal BuySellPair Margin { get; private set; }

        internal void Add(BuySellPair margin)
        {
            this.Margin += margin;
        }

    }

    [TestFixture]
    public class BuySellPairTest
    {
        [Test]
        public void AddTest()
        {
            NecessaryCalculator calculator = new NecessaryCalculator();
            calculator.Add(new BuySellPair(40, 0));
            Assert.AreEqual(40, calculator.Margin.Buy);
        }
    }
}
