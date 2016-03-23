module TradingEngine.Order 

open System
open System.Xml
open System.Xml.Linq
open iExchange.Common
open Protocal


let toXName (str: string) = XName.op_Implicit(str)

let setAttr (node: XElement) attr value = node.SetAttributeValue(toXName attr, value)

let formatDateTime (dt: DateTime) = dt.ToString("yyyy-MM-dd HH:mm:ss")

let toXmlOrderRelation (orr: Protocal.OrderRelationData) =
    let result = new XElement( toXName "OrderRelation")
    let attrVals = seq{
        yield "OpenOrderID", orr.OpenOrderId.ToString()
        yield "ClosedLot", orr.ClosedLot.ToString()
    }
    attrVals |> Seq.iter (fun (attr, value) -> setAttr result attr value)
    result

let toXmlOrder (orderData: Protocal.OrderData) = 
    let xmlOrder =  new XElement(toXName "Order")
    let attrVals = seq{
        yield "ID", orderData.Id.ToString()
        yield "TradeOption", (int(orderData.TradeOption)).ToString()
        yield "IsOpen", orderData.IsOpen.ToString()
        yield "IsBuy", orderData.IsBuy.ToString()
        yield "SetPrice", orderData.SetPrice
        yield "DQMaxMove", orderData.DQMaxMove.ToString()
        yield "Lot", orderData.Lot.ToString()
        yield "OriginalLot", orderData.OriginalLot.ToString()
        if orderData.PriceTimestamp.HasValue then
            yield "PriceTimestamp", formatDateTime (orderData.PriceTimestamp.Value)

        match orderData with
        | :? Physical.PhysicalOrderData as pod ->
            yield "PhysicalTradeSide", (int pod.PhysicalTradeSide).ToString()
            if pod.InstalmentPart <> null then
                yield "InstalmentPolicyId", pod.InstalmentPart.InstalmentPolicyId.ToString()
                yield "DownPayment", pod.InstalmentPart.DownPayment.ToString()
                yield "PhysicalInstalmentType", (int pod.InstalmentPart.InstalmentType).ToString()
                yield "RecalculateRateType", (int pod.InstalmentPart.RecalculateRateType).ToString()
                yield "Period", (int pod.InstalmentPart.Period).ToString()
                yield "InstalmentFrequence", (int pod.InstalmentPart.InstalmentFrequence).ToString()
        | _  -> ()
    }

    attrVals |> Seq.iter (fun (attr, value) -> setAttr xmlOrder attr value)

    let ios = orderData.IfDoneOrderSetting
    if ios <> null then
        let ifDoneNode = new XElement(toXName "IfDone")
        if ios.LimitPrice <> null then setAttr ifDoneNode "LimitPrice" ios.LimitPrice
        if ios.StopPrice <> null then setAttr ifDoneNode "StopPrice" ios.StopPrice
        let extensionNode = new XElement(toXName "Extension")
        xmlOrder.Add(extensionNode)
        extensionNode.Add(ifDoneNode)

    if orderData.OrderRelations <> null then
        for orr in orderData.OrderRelations do
            let xmlOrr = toXmlOrderRelation orr
            xmlOrder.Add(xmlOrr)

    xmlOrder


let toXmlTran (tranData: Protocal.TransactionData) =
    let xmlTran = new XElement(toXName "Transaction")
    let attrVals = seq{
        yield "ID", tranData.Id.ToString()
        yield "AccountID", tranData.AccountId.ToString()
        yield "InstrumentID", tranData.InstrumentId.ToString()
        yield "Type", (int(tranData.Type)).ToString()
        yield "SubType", (int(tranData.SubType)).ToString()
        yield "OrderType", (int(tranData.OrderType)).ToString()
        yield "BeginTime", formatDateTime (tranData.BeginTime)
        yield "EndTime", formatDateTime (tranData.EndTime)
        yield "ExpireType", (int(tranData.ExpireType)).ToString()
        yield "SubmitTime", formatDateTime (tranData.SubmitTime)
        yield "SubmitorID", tranData.SubmitorId.ToString()
        if tranData.SourceOrderId.HasValue then yield "AssigningOrderID", tranData.SourceOrderId.Value.ToString()

    }
    attrVals |> Seq.iter (fun (attr,value) -> setAttr xmlTran attr value)
    for order in tranData.Orders do
        let xmlOrder = toXmlOrder order
        xmlTran.Add(xmlOrder)
    xmlTran

    




    
