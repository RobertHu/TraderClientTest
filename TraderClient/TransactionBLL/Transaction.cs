using iExchange.Common;
using Protocal.Physical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TraderClient.OrderBLL;

namespace TraderClient.TransactionBLL
{
    internal class TransactionManager
    {
        internal static readonly TransactionManager Default = new TransactionManager();

        static TransactionManager() { }
        private TransactionManager() { }

        internal Protocal.TransactionData CreateSpotTran(Guid accountId, Guid instrumentId, string price, decimal lot, bool isBuy, Guid submitorId)
        {
            var tranData = this.CreateTranCommon(accountId, instrumentId, iExchange.Common.OrderType.SpotTrade, submitorId);
            tranData.Orders.Add(OrderManager.Default.CreateOpenOrder(price, lot, isBuy, iExchange.Common.TradeOption.Better, 0));
            return tranData;
        }

        internal Protocal.TransactionData CreateOpenPhysicalInstalmentSpotTran(Guid accountId, Guid instrumentId, string price, decimal lot, bool isBuy, PhysicalTradeSide tradeSide,PhysicalType physicalType   ,InstalmentParameter instalmentParameter, Guid submitorId)
        {
            var tranData = this.CreateTranCommon(accountId, instrumentId, iExchange.Common.OrderType.SpotTrade, submitorId);
            tranData.Orders.Add(OrderManager.Default.CreatePhysicalInstalmentOpenOrder(price, lot, true, isBuy, TradeOption.Better, 0, tradeSide, physicalType, instalmentParameter));
            return tranData;
        }

        internal Protocal.TransactionData CreatePhysicalOpenSportTran(Guid accountId, Guid instrumentId, string price, decimal lot, bool isBuy, PhysicalTradeSide tradeSide,Guid submitorId)
        {
            var tranData = this.CreateTranCommon(accountId, instrumentId, OrderType.SpotTrade, submitorId);
            tranData.Orders.Add(OrderManager.Default.CreatePhysicalOrder(price, lot, true, isBuy, TradeOption.Better, 0, tradeSide , PhysicalType.FullPayment));
            return tranData;
        }

        internal Protocal.TransactionData CreatePhysicalCloseSportTran(Guid accountId, Guid instrumentId, Guid openOrderId,string price, decimal lot, bool isBuy, PhysicalTradeSide tradeSide,Guid submitorId)
        {
            var tranData = this.CreateTranCommon(accountId, instrumentId, OrderType.SpotTrade,submitorId);
            tranData.Orders.Add(OrderManager.Default.CreateClosePhysicalOrder(openOrderId,price,lot,isBuy,TradeOption.Better,0, tradeSide, PhysicalType.FullPayment));
            return tranData;
        }


        internal Protocal.TransactionData CreateOOCOOpenLimitTran(Guid accountId, Guid instrumentId, string setPrice1, string setPrice2, bool isBuy, decimal lot, Guid submitorId)
        {
            var tranData = this.CreateTranCommon(accountId, instrumentId, OrderType.Limit, submitorId);
            var oneOrder = OrderManager.Default.CreateOpenOrder(setPrice1, lot, isBuy, TradeOption.Better, 0);
            var otherOrder = OrderManager.Default.CreateOpenOrder(setPrice2, lot, isBuy, TradeOption.Stop, 0);
            tranData.Orders.Add(oneOrder);
            tranData.Orders.Add(otherOrder);
            return tranData;
        }




        internal Protocal.TransactionData CreateCloseSpotTran(Guid accountId, Guid instrumentId, Guid openOrderId, string price, decimal lot, bool isBuy, Guid submitorId)
        {
            var tranData = this.CreateTranCommon(accountId, instrumentId, iExchange.Common.OrderType.SpotTrade, submitorId);
            tranData.Orders.Add(OrderManager.Default.CreateCloseOrder(openOrderId, price, lot, isBuy, iExchange.Common.TradeOption.Better, 0));
            return tranData;
        }


        internal Protocal.TransactionData CreateMultipleCloseSpotTran(Guid accountId, Guid instrumentId, iExchange.Common.OrderType orderType,Guid submitorId)
        {
            var tranData = this.CreateTranCommon(accountId, instrumentId, orderType,submitorId);
            var order1 = OrderManager.Default.CreateCloseOrder(Guid.Parse("3B5E33D8-E936-4D10-9BA5-049215E5BA74"), "1.4115", 2m, true, iExchange.Common.TradeOption.Better, 0);
            var order2 = OrderManager.Default.CreateCloseOrder(Guid.Parse("7056F5B8-300D-41B1-AF0E-517A08A2763D"), "1.4115", 2m, false, iExchange.Common.TradeOption.Better, 0);
            tranData.Orders.Add(order1);
            tranData.Orders.Add(order2);
            return tranData;
        }


        internal Protocal.TransactionData CreateOpenLimitTran(Guid accountId, Guid instrumentId, string price,decimal lot ,TimeSpan duringTime, bool isBuy, Guid submitorId)
        {
            var result = this.CreateTranCommon(accountId, instrumentId, iExchange.Common.OrderType.Limit, submitorId ,duringTime);
            result.Orders.Add(OrderManager.Default.CreateOpenOrder(price, lot, isBuy, iExchange.Common.TradeOption.Better, 0));
            return result;
        }


        private Protocal.TransactionData CreateTranCommon(Guid accountId, Guid instrumentId, iExchange.Common.OrderType orderType, Guid submitorId,TimeSpan? duringTime = null)
        {
            return new Protocal.TransactionData
             {
                 Id = Guid.NewGuid(),
                 AccountId = accountId,
                 InstrumentId = instrumentId,
                 BeginTime = DateTime.Now,
                 EndTime = DateTime.Now.Add(duringTime ?? new TimeSpan(1, 0, 0)),
                 SubmitorId = submitorId,
                 SubmitTime = DateTime.Now,
                 OrderType = orderType,
                 Type = iExchange.Common.TransactionType.Single,
                 SubType = iExchange.Common.TransactionSubType.None,
                 Orders = new List<Protocal.OrderData>()
             };
        }

    }
}
