using iExchange.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionServerTest
{
    internal abstract class TransactionDataFactory
    {
        protected TransactionDataFactory()
        {
            var baseTime = DateTime.Now;
            this.BeginTime = baseTime;
            this.EndTime = baseTime.AddMinutes(30);
        }

        internal virtual Guid AccountId
        {
            get
            {
                return Guid.Parse("B940D4B7-4A4E-46DF-8EA4-77B0C3CC1A6B");
            }
        }

        internal virtual Guid InstrumentId
        {
            get
            {
                return Guid.Parse("33C4C6E2-E33C-4A21-A01A-35F4EC647890");
            }
        }

        internal virtual TransactionType Type
        {
            get
            {
                return TransactionType.Single;
            }
        }

        internal virtual TransactionSubType SubType
        {
            get { return TransactionSubType.None; }
        }

        internal abstract OrderType OrderType { get; }

        internal DateTime BeginTime { get; private set; }

        internal DateTime EndTime { get; private set; }

        internal virtual Guid SubmitorID
        {
            get { return Guid.Parse("CB58B47D-A705-42DD-9308-6C6B26CE79A7"); }
        }

        internal virtual DateTime SubmitTime
        {
            get { return DateTime.Now; }
        }

        internal virtual bool PlaceByRiskMonitor { get { return false; } }


        internal Protocal.TransactionData Create(OrderDataFactory orderDataFactory)
        {
            var tranData = new Protocal.TransactionData()
            {
                Id = Guid.NewGuid(),
                AccountId = this.AccountId,
                InstrumentId = this.InstrumentId,
                OrderType = this.OrderType,
                Type = this.Type,
                SubType = this.SubType,
                BeginTime = this.BeginTime,
                EndTime = this.EndTime,
                SubmitorId = this.SubmitorID,
                SubmitTime = this.SubmitTime,
                PlaceByRiskMonitor = this.PlaceByRiskMonitor
            };
            tranData.Orders = new List<Protocal.OrderData>();
            tranData.Orders.Add(orderDataFactory.Create());

            return tranData;
        }
    }

    internal abstract class OrderDataFactory
    {
        internal Protocal.OrderData Create()
        {
            var orderData = new Protocal.OrderData
            {
                Id = Guid.NewGuid(),
                IsOpen = this.IsOpen,
                IsBuy = this.IsBuy,
                SetPrice = this.SetPrice,
                Lot = this.Lot,
                LotBalance = this.LotBalance,
                TradeOption = this.TradeOption,
                DQMaxMove = this.DQMaxMove,
            };
            return orderData;
        }

        internal abstract TradeOption TradeOption { get; }

        internal abstract bool IsOpen { get; }

        internal abstract bool IsBuy { get; }

        internal abstract string SetPrice { get; }

        internal abstract decimal Lot { get; }

        internal abstract decimal LotBalance { get; }

        internal abstract int DQMaxMove { get; }

    }

    internal abstract class OrderRelationDataFactory
    {
        internal abstract Guid OpenOrderId { get; }
        internal abstract Guid CloseOrderId { get; }
        internal abstract decimal CloseLot { get; }

        internal Protocal.OrderRelaitonData Create()
        {
            var result = new Protocal.OrderRelaitonData
            {
                OpenOrderId = this.OpenOrderId,
                CloseOrderId = this.CloseOrderId,
                ClosedLot = this.CloseLot
            };
            return result;
        }
    }


    internal sealed class SpotTransactionDataFactory : TransactionDataFactory
    {
        internal static readonly SpotTransactionDataFactory Default = new SpotTransactionDataFactory();
        static SpotTransactionDataFactory() { }
        private SpotTransactionDataFactory() { }

        internal override OrderType OrderType
        {
            get { return OrderType.SpotTrade; }
        }
    }

    internal sealed class OpenBuyOrder : OrderDataFactory
    {
        internal static readonly OpenBuyOrder Default = new OpenBuyOrder();
        static OpenBuyOrder() { }
        private OpenBuyOrder() { }

        internal override TradeOption TradeOption
        {
            get { return TradeOption.Better; }
        }

        internal override bool IsOpen
        {
            get { return true; }
        }

        internal override bool IsBuy
        {
            get { return true; }
        }

        internal override string SetPrice
        {
            get { return "19.52"; }
        }

        internal override decimal Lot
        {
            get { return 2; }
        }

        internal override decimal LotBalance
        {
            get { return 2; }
        }

        internal override int DQMaxMove
        {
            get { return 5; }
        }
    }


}
