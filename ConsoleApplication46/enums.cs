using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransactionServerTester
{
    public enum TransactionType
    {
        Single,
        Pair,
        OneCancelOther,
        //Mapping,
        MultipleClose = 4,
        Assign = 100,   //AssigningOrderID == SourceOrderID (id of the order been assigned from)
    }

    public enum TransactionSubType
    {
        None = 0,
        Amend,  //AssigningOrderID == AmendedOrderId (id of the order been amended)
        IfDone, //AssigningOrderID == IfOrderId (id of the order used as condition)
        Match,  //AssigningOrderID == SourceOrderID (id of the order been split from) NOTE: TransactionType===Single
        Assign, //AssigningOrderID == AssigningOrderID (id of the order been assigned from) //NotImplemented
        Mapping,
    }

    public enum OrderType
    {
        SpotTrade,
        Limit,
        Market,
        MarketOnOpen,
        MarketOnClose,
        OneCancelOther,
        Risk,
        Stop,
        MultipleClose,
        MarketToLimit,
        StopLimit,
    }


    public enum TradeOption
    {
        Invalid,
        Stop,
        Better
    }

}
