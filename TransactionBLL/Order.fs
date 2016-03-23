module OrderModule
open System

type TradeOption
    |Invalid = 0
    |Stop = 1
    |Better =2


type OrderInfo = {Id: Guid; Lot: decimal; LotBalance: decimal ; IsOpen: bool; IsBuy: bool; TradeOption: TradeOption; DqMaxMove: int }

type TransactionInfo = {Id: Guid; AccountId: Guid; InstrumentId: Guid; BegineTime: DateTime; }


type Order
    |SpotOrder  of OrderInfo 
    |LimitOrder of OrderInfo
    |IfDoneOrder of OrderInfo * OrderInfo
    |OCOOrder  of OrderInfo * OrderInfo 














