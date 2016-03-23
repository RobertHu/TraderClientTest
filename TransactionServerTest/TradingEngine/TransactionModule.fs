module TradingEngine.TransactionModule

open System
open iExchange.Common
open TradingEngine.OrderModule



type internal Transaction = {ID: Guid; AccountID: Guid; InstrumentID:Guid; Type: TransactionType; 
    SubType: TransactionSubType; OrderType: OrderType; BeginTime: DateTime; EndTime: DateTime;
     SubmitTime: DateTime; SubmitorID: Guid; SourceOrderId: Guid option }

let private convertTransactionToTranData (tran: Transaction) : Protocal.TransactionData = 
    let result = new Protocal.TransactionData()
    result.Id <- tran.ID
    result.InstrumentId <- tran.InstrumentID
    result.AccountId  <- tran.AccountID
    result.Type  <- tran.Type
    result.SubType <- tran.SubType
    result.OrderType <- tran.OrderType
    result.BeginTime <- tran.BeginTime
    result.EndTime <- tran.EndTime
    result.SubmitTime <- tran.SubmitTime
    result.SubmitorId <- tran.SubmitorID
    result.SourceOrderId <- match tran.SourceOrderId with | Some(id) -> Nullable<Guid>(id) | _ -> Nullable<Guid>()
    result



type private TranCommonData = {AccountID: Guid; InstrumentID:Guid; Type: TransactionType; 
    SubType: TransactionSubType; OrderType: OrderType; BeginTime: DateTime; EndTime: DateTime;
    SubmitTime: DateTime; SubmitorID: Guid}


type internal TranType = 
    | SportTran of Transaction * Order
    | OCOTran  of Transaction * Order * Order


let private createTranDataCommon accountid instrumentid trantype subtype ordertype begintime endtime submitid = 
    {AccountID = accountid; InstrumentID = instrumentid; Type = trantype; SubType = subtype; 
     OrderType = ordertype; BeginTime = begintime; EndTime = endtime; SubmitTime = DateTime.Now; 
     SubmitorID = submitid }

let private createSpotTranDataCommon  accountid instrumentid submitid = 
    let beginTime = DateTime.Now
    let endTime = beginTime.AddMinutes(20.0)
    createTranDataCommon accountid instrumentid TransactionType.Single TransactionSubType.None OrderType.SpotTrade beginTime endTime submitid

let private createTran (tranCommon: TranCommonData) : Transaction = 
    {ID= Guid.NewGuid(); AccountID=tranCommon.AccountID; InstrumentID = tranCommon.InstrumentID; Type = tranCommon.Type;
       SubType = tranCommon.SubType; OrderType = tranCommon.OrderType;BeginTime = tranCommon.BeginTime; EndTime = tranCommon.EndTime;
       SubmitTime  = tranCommon.SubmitTime; SubmitorID = tranCommon.SubmitorID; SourceOrderId = None
    }


let private createSpotTran(accountid, instrumentid, price, lot, isbuy, submitorid) = 
    SportTran ((createTran (createSpotTranDataCommon accountid instrumentid submitorid)) , (createOpenOrder price lot isbuy TradeOption.Better 0))


let createSportTranData(accountid, instrumentid, price, lot, isbuy, submitorid) = 
    let spotTran = createSpotTran(accountid, instrumentid, price, lot, isbuy, submitorid)
    match spotTran with
    | SportTran(tran, order) ->
        let result = convertTransactionToTranData tran
        result.Orders <-  new ResizeArray<Protocal.OrderData>()
        result.Orders.Add(convertOrderToOrderData order)
        result
    | _ -> null



     












