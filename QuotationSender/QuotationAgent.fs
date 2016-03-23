module QuotationAgent

open Microsoft.FSharp.Control
open Protocal
open iExchange.Common
open System
open System.Collections.Generic

let gateWayChannel= ChannelFactory.CreateHttpChannel<Protocal.IGatewayService>("http://localhost:5060/GatewayService")

let instruments =
    seq{
        yield "1214720E-01DC-4E68-A271-0AFA8B5E72E1", seq{ yield "AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2" }
        yield "36EA5E9D-A12C-45F4-AC5C-0F9D8E12BDF7", seq{ yield "AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"}
        yield "0ADF8B7D-238D-4F29-8B13-14307FDA9701", seq{yield "AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"}
        yield "2A346B2A-BD3D-4950-8CE0-384D5C16A73E", seq{yield "9CA623E7-1E7D-4B3D-BC84-47B92059B26E"; yield "5B691D3C-319D-4402-9908-5AA2BD2CD429"; yield "AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"}
        yield "706FB632-AB1E-4EA9-B146-4633E39C161E", seq{yield "AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"}
        yield "3C4B1A43-4D50-4EF3-9FAD-521413BADAF9", seq{yield "AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"}
        yield "670590DB-8CE6-4676-AC68-7701FFC5410A", seq{yield "AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"}
        yield "C2EF11CC-E976-4A68-9D9E-8B7DDAFA89F3", seq{yield "9CA623E7-1E7D-4B3D-BC84-47B92059B26E"; yield "AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"}
        yield "66ADC06C-C5FE-4428-867F-BE97650EB3B4", seq{yield "AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"}
        yield "ABC59161-5F85-488D-AD1E-CCDC0FD82D76", seq{yield "AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"}
        yield "7B7E1944-4B61-4657-8DE6-DE623430C387", seq{yield "AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"}
        yield "3D4FA404-C1ED-4B64-9390-E14B6D04610F", seq{yield "9CA623E7-1E7D-4B3D-BC84-47B92059B26E"; yield "5B691D3C-319D-4402-9908-5AA2BD2CD429"; yield "AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"}
        yield "C604B7DB-16C8-4A92-B076-EA0DC38BC84A", seq{yield "9CA623E7-1E7D-4B3D-BC84-47B92059B26E"; yield "AD0BEE1C-7E75-4C80-B8FD-920B6B0B0EF2"}
    }


let createQuotation  insId qid  ask bid =
    let result = new OverridedQuotation()
    result.InstrumentID <- insId
    result.QuotePolicyID <- qid
    result.Timestamp <- DateTime.Now
    result.Ask <- ask
    result.Bid <- bid
    result


type QuotationMaker private() =
    static let instance =  new QuotationMaker() 

    let agent = new MailboxProcessor<_>(fun inbox ->
        let rec loop () =
            async{
                let! ask, bid = inbox.Receive()
                printfn "ask = %s, bid = %s" ask bid
                let quotations = new ResizeArray<_>()
                for eachInstrument, quotePolicies in instruments do
                    for eachQuotePolicy in quotePolicies do
                        let quotation = createQuotation (Guid.Parse(eachInstrument))  (Guid.Parse(eachQuotePolicy))  ask bid
                        quotations.Add(quotation)
                gateWayChannel.SetQuotation(quotations.ToArray())
                return! loop ()
            }
        loop ()
        )
    
    static member Default with get () = instance

    member this.Start() = agent.Start()

    member this.SetPrice ask bid = agent.Post(ask,bid)


