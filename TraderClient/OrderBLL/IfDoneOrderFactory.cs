using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iExchange.Common;

namespace TraderClient.OrderBLL
{
    internal abstract class OrderFactory
    {
        protected Protocal.TransactionData CreateTransaction(Guid accountId, Guid instrumentId, TransactionType type, TransactionSubType subType, OrderType orderType)
        {
            var result = new Protocal.TransactionData()
            {
                Id = Guid.NewGuid(),
                AccountId = accountId,
                BeginTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2),
                InstrumentId = instrumentId,
                OrderType = orderType,
                SubmitorId = Guid.Parse("CB58B47D-A705-42DD-9308-6C6B26CE79A7"),
                SubmitTime = DateTime.Now,
                Type = type,
                SubType = subType
            };
            result.Orders = new List<Protocal.OrderData>();
            return result;
        }

    }

    internal sealed class IfDoneOrderFactory : OrderFactory
    {
        internal Protocal.TransactionData Create(Guid accountId, Guid instrumentId)
        {
            var tranData = this.CreateTransaction(accountId, instrumentId, TransactionType.Single, TransactionSubType.IfDone, OrderType.Limit);
            var orderData = this.CreateOrderData("1.234", 2, true, true, TradeOption.Better);
            tranData.Orders.Add(orderData);
            return tranData;
        }

        private Protocal.OrderData CreateOrderData(string setPrice, decimal lot, bool isBuy, bool isOpen, TradeOption tradeOption)
        {
            var result = new Protocal.OrderData
            {
                Id = Guid.NewGuid(),
                IsBuy = isBuy,
                IsOpen = isOpen,
                Lot = lot,
                TradeOption = TradeOption.Better
            };
            result.IfDoneOrderSetting = new Protocal.IfDoneOrderSetting()
            {
                 LimitPrice = "1.238",
                 StopPrice = "1.105"
            };
            return result;
        }
    }
}
