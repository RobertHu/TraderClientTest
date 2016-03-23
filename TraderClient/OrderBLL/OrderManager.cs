using iExchange.Common;
using Protocal.Physical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TraderClient.OrderBLL
{
    internal sealed class InstalmentParameter
    {
        internal InstalmentParameter(Guid instalmentPolicyId, decimal downPayment, InstalmentType instalmentType, InstalmentFrequence instalmentFrequence, RecalculateRateType recalculateRateType, int period)
        {
            this.InstalmentPolicyId = instalmentPolicyId;
            this.DownPayment = downPayment;
            this.InstalmentType = instalmentType;
            this.InstalmentFrequence = instalmentFrequence;
            this.RecalculateRateType = recalculateRateType;
            this.Period = period;
        }

        internal Guid InstalmentPolicyId { get; private set; }

        internal decimal DownPayment { get; private set; }

        internal InstalmentType InstalmentType { get; private set; }

        internal InstalmentFrequence InstalmentFrequence { get; private set; }

        internal RecalculateRateType RecalculateRateType { get; private set; }

        internal int Period { get; private set; }
    }

    internal sealed class OrderManager
    {
        internal static readonly OrderManager Default = new OrderManager();
        static OrderManager() { }
        private OrderManager() { }


        internal Protocal.OrderData CreateOpenOrder(string price, decimal lot, bool isBuy, TradeOption tradeOption, int dqMaxMove)
        {
            Protocal.OrderData result = new Protocal.OrderData();
            this.CreateCommon(result, price, lot, true, isBuy, tradeOption, dqMaxMove);
            return result;
        }

        internal Protocal.OrderData CreateCloseOrder(Guid openOrderId, string price, decimal closeLot, bool isBuy, TradeOption tradeOption, int dqMaxMove)
        {
            Protocal.OrderData result = new Protocal.OrderData();
            this.CreateCommon(result, price, closeLot, false, isBuy, tradeOption, dqMaxMove);
            result.OrderRelations = new List<Protocal.OrderRelationData>();
            result.OrderRelations.Add(this.CreateOrderRelation(openOrderId, result.Id, closeLot));
            return result;
        }

        internal Protocal.OrderData CreatePhysicalInstalmentOpenOrder(string price, decimal lot, bool isOpen, bool isBuy, TradeOption tradeOption, int dqMaxMove, PhysicalTradeSide tradeSide, PhysicalType physicalType, InstalmentParameter instalmentParameter)
        {
            var result = this.CreatePhysicalOrder(price, lot, isOpen, isBuy, tradeOption, dqMaxMove, tradeSide, physicalType);
            result.InstalmentPart = new InstalmentPart
            {
                InstalmentPolicyId = instalmentParameter.InstalmentPolicyId,
                DownPayment = instalmentParameter.DownPayment,
                InstalmentFrequence = instalmentParameter.InstalmentFrequence,
                InstalmentType = instalmentParameter.InstalmentType,
                Period = instalmentParameter.Period,
                RecalculateRateType = instalmentParameter.RecalculateRateType
            };
            return result;
        }


        internal Protocal.Physical.PhysicalOrderData CreatePhysicalOrder(string price, decimal lot, bool isOpen, bool isBuy, TradeOption tradeOption, int dqMaxMove, PhysicalTradeSide tradeSide, PhysicalType physicalType)
        {
            var result = new Protocal.Physical.PhysicalOrderData();
            this.CreateCommon(result, price, lot, isOpen, isBuy, tradeOption, dqMaxMove);
            result.PhysicalTradeSide = tradeSide;
            result.PhysicalType = physicalType;
            return result;
        }

        internal Protocal.Physical.PhysicalOrderData CreateClosePhysicalOrder( Guid openOrderId, string price, decimal lot,  bool isBuy, TradeOption tradeOption, int dqMaxMove, PhysicalTradeSide tradeSide, PhysicalType physicalType)
        {
            var result = this.CreatePhysicalOrder(price, lot, false, isBuy, tradeOption, dqMaxMove, tradeSide, physicalType);
            result.OrderRelations = new List<Protocal.OrderRelationData>();
            result.OrderRelations.Add(this.CreateOrderRelation(openOrderId, result.Id, lot));
            return result;
        }



        private void CreateCommon(Protocal.OrderData order, string price, decimal lot, bool isOpen, bool isBuy, TradeOption tradeOption, int dqMaxMove)
        {
            order.Id = Guid.NewGuid();
            order.SetPrice = price;
            order.Lot = lot;
            order.IsOpen = isOpen;
            order.IsBuy = isBuy;
            order.TradeOption = tradeOption;
            order.DQMaxMove = dqMaxMove;
        }

        private Protocal.OrderRelationData CreateOrderRelation(Guid openOrderId, Guid closeOrderId, decimal closedLot)
        {
            return new Protocal.OrderRelationData
            {
                OpenOrderId = openOrderId,
                CloseOrderId = closeOrderId,
                ClosedLot = closedLot
            };
        }

    }
}
