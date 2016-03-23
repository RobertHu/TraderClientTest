using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Core.TransactionServer.Agent.Reset;
using Core.TransactionServer.Agent;
using Core.TransactionServer.Agent.BLL.TransactionBusiness;
using iExchange.Common;
using Core.TransactionServer.Agent.BLL.OrderBusiness;
using Core.TransactionServer.Agent.Util;
using CoreSettings = Core.TransactionServer.Agent.Settings;

namespace UnitTest
{

    [TestFixture]
    public class GenericDictionaryTest
    {

        [TestFixtureSetUp]
        public void Startup()
        {
            SettingInitializer.LoadSettings();
        }

        [Test]
        public void AddOrderDayHistoryTest()
        {
            Account account = new Account(Guid.Parse("606D464F-4B7A-4481-97D0-00CF23C8DAE9"));
            CoreSettings.Instrument  instrument = CoreSettings.Setting.Default.GetInstrument(Guid.Parse("2E42C798-97E7-4702-AFBA-0E6ABA0575D6"));
            Transaction tran = new Transaction(account, this.CreateTransactionConstructParams(), GeneralTransactionServiceFactory.Default);
            Order order = new Order(tran, this.CreateOrderConstructParams(instrument), GeneralOrderServiceFactory.Default);
            OrderResetResult orderResetResult = new OrderResetResult { OrderId = order.Id, TradeDay = DateTime.Now.Date };
            ResetManager.Default.AddOrderDayHistory(order, orderResetResult);
            var orderDayHistorys = ResetManager.Default.GetOrderDayHistorysByOrderId(order.Id);
            Assert.AreEqual(1, orderDayHistorys.Count);
            var target = orderDayHistorys[0].Value;
            Assert.AreEqual(orderResetResult.OrderId, target.Id);
            Assert.AreEqual(orderResetResult.TradeDay, target.TradeDay);
        }

        private TransactionConstructParams CreateTransactionConstructParams()
        {
            return new TransactionConstructParams
            {
                 Id =Guid.Parse("A81BB13C-9DE5-4A56-B266-41521527B0D8"),
                 Code = "MHL151023SP00001",
                 ConstractSize=10.0m,
                 Phase =  TransactionPhase.Executed,
                 ExecuteTime = DateTime.Parse("2015-10-23 14:12:15.000"),
                 InstrumentId = Guid.Parse("2E42C798-97E7-4702-AFBA-0E6ABA0575D6")
            };
        }

        private OrderConstructParams CreateOrderConstructParams(CoreSettings.Instrument instrument)
        {
            return new OrderConstructParams
            {
                Id = Guid.Parse("E03901A9-1708-48E2-8389-3ADD436B07C7"),
                Code = "MHL2015102300001",
                Lot = 1,
                LotBalance= 1,
                Phase = OrderPhase.Executed,
                TradeOption= TradeOption.Better,
                IsOpen = true,
                IsBuy = true,
                SetPrice = PriceHelper.CreatePrice("1.05",instrument),
                ExecutePrice = PriceHelper.CreatePrice("1.05",instrument),
                DQMaxMove=0
            };
        }

    }
}
