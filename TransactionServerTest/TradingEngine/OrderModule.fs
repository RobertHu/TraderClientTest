module TradingEngine.OrderModule

open System
open Protocal
open iExchange.Common

type internal OrderRelation = {CloseOrderId: Guid; OpenOrderId: Guid; ClosedLot: decimal}

let private convertOrderRelationToData (orr: OrderRelation): OrderRelationData = 
    let result = new Protocal.OrderRelationData()
    result.CloseOrderId <- orr.CloseOrderId
    result.OpenOrderId <- orr.OpenOrderId
    result.ClosedLot <- orr.ClosedLot
    result

type internal Order = {Id: Guid; IsOpen: bool; IsBuy: bool ; SetPrice: string; Lot: decimal; TradeOption: iExchange.Common.TradeOption; DQMaxMove: int;
                 OrderRelations: ResizeArray<OrderRelation> }

type private CommonOrderData = {IsOpen: bool; IsBuy: bool ; SetPrice: string; Lot: decimal; TradeOption: iExchange.Common.TradeOption; DQMaxMove: int}

let private createOrderCommon price lot isOpen isBuy tradeOption dqMaxMove =  
    {SetPrice = price; Lot = lot; IsOpen = isOpen; IsBuy = isBuy; TradeOption = tradeOption; DQMaxMove = dqMaxMove}

let internal createOpenOrder price lot isBuy tradeOption dqMaxMove = 
    let commonOrder = createOrderCommon price lot true isBuy tradeOption  dqMaxMove 
    {Id = Guid.NewGuid(); IsOpen = commonOrder.IsOpen; IsBuy = commonOrder.IsBuy;  SetPrice = commonOrder.SetPrice;
     Lot = commonOrder.Lot; TradeOption = commonOrder.TradeOption; DQMaxMove = commonOrder.DQMaxMove;  OrderRelations = null   }


let private createOrderRelation closeOrderId openOrderId closedLot =
    {CloseOrderId= closeOrderId ; OpenOrderId= openOrderId; ClosedLot = closedLot}


let internal convertOrderToOrderData (order: Order): Protocal.OrderData = 
    let result = new Protocal.OrderData()
    result.Id <- order.Id
    result.IsOpen <- order.IsOpen
    result.IsBuy <- order.IsBuy
    result.SetPrice <- order.SetPrice
    result.Lot <- order.Lot
    result.TradeOption <- order.TradeOption
    result.DQMaxMove <- order.DQMaxMove
    if order.OrderRelations <> null then 
        result.OrderRelations <- new ResizeArray<Protocal.OrderRelationData>()
        order.OrderRelations |> Seq.map (fun m -> convertOrderRelationToData m)
        |> Seq.iter (fun m -> result.OrderRelations.Add(m))
    result




