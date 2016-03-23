using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Protocal;
using Core.TransactionServer.Agent;
using Core.TransactionServer.Agent.AccountClass;
using Core.TransactionServer.Agent.Market;
using iExchange.Common;
using Protocal.TradingInstrument;

namespace UnitTest.OrderBLLTest
{
    [TestFixture]
    public class PhysicalOrderTest
    {
        [TestFixtureSetUp]
        public void StartUp()
        {
            SettingInitializer.LoadSettings();
        }

        [Test]
        public void OpenOrderTest()
        {
            Guid instrumentId = Guid.Parse("2a346b2a-bd3d-4950-8ce0-384d5c16a73e");
            TransactionData tranData = new TransactionData
            {
                Id = Guid.NewGuid(),
                BeginTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(10),
                InstrumentId = instrumentId,
                OrderType = iExchange.Common.OrderType.SpotTrade
            };
            tranData.Orders = new List<OrderData>();

            OrderData orderData = new OrderData
            {
                Id = Guid.NewGuid(),
                SetPrice = "1.10",
                Lot = 2,
                IsBuy = true,
                IsOpen = true,
                TradeOption = iExchange.Common.TradeOption.Better,
                DQMaxMove = 0
            };
            tranData.Orders.Add(orderData);
            Account account = new Account(Guid.Parse("47381a10-6010-453e-ac55-a9a9eaf8908a"));
            SubFund fund = new SubFund(account, Guid.Parse("0DA665B5-9AA5-49D7-A301-048F1428CA4A"), 99999911.50m, 0m, Core.TransactionServer.Agent.Framework.OperationType.None);
            MarketManager.Default.UpdateQuotation(instrumentId, Guid.Parse("9CA623E7-1E7D-4B3D-BC84-47B92059B26E"), "1.10", "1.11", "1.15", "1.08");
            MarketManager.Default.UpdateQuotation(instrumentId, Guid.Parse("5B691D3C-319D-4402-9908-5AA2BD2CD429"), "1.10", "1.11", "1.15", "1.08");
            MarketManager.Default.UpdateQuotation(instrumentId, Guid.Parse("AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"), "1.10", "1.11", "1.15", "1.08");
            var instrumentTradingStatus = InstrumentsTradingStatus.Create();
            instrumentTradingStatus.Add(instrumentId, InstrumentStatus.SessionOpen,null);
            SettingInitializer.Server.UpdateInstrumentsTradingStatus(instrumentTradingStatus);
            var error =  account.Place(tranData);
            Assert.AreEqual(TransactionError.OK, error);
        }
    }
}
