// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

open System

[<EntryPoint>]
let main argv = 
    System.Console.Write("start")
    let rand = new System.Random()
    QuotationAgent.QuotationMaker.Default.Start()
    while true do
        let integer = rand.Next(10)
        let decimals = Math.Round(rand.NextDouble(), 2)
        let ask = float(integer) + decimals
        let bid = ask + 0.01
        QuotationAgent.QuotationMaker.Default.SetPrice (ask.ToString()) (bid.ToString())
        System.Threading.Thread.Sleep(1000)
    0
