module TradingEngine.Builder

open System
open iExchange.Common


type TranBuilder<'a> = 'a -> Protocal.TransactionData

type SpotTranArgs = {AccountId: Guid; Instrument: Guid; SubmitorId: Guid; orderType: OrderType}

let createTranCommon = 
    let builder (m: SpotTranArgs) = 
        let tran = new Protocal.TransactionData()
        tran.Id <- Guid.NewGuid()
        tran.AccountId <- m.AccountId
        tran.InstrumentId <- m.Instrument
        tran.BeginTime <- DateTime.Now
        tran.EndTime <-DateTime.Now.AddHours(1.0)
        tran.SubmitorId <- m.SubmitorId
        tran.SubmitTime <- DateTime.Now
        tran.OrderType <- m.orderType  
        tran.Type <- TransactionType.Single
        tran.SubType <- TransactionSubType.None
        tran.Orders <- new ResizeArray<Protocal.OrderData>()
        tran
    builder


    










