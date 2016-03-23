// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.


open System.Net
open Microsoft.FSharp.Control.WebExtensions

let urlList = [ "Microsoft.com", "http://www.microsoft.com/"
                "MSDN", "http://msdn.microsoft.com/"
                "Bing", "http://www.bing.com"
              ]

let fetchAsync(name, url:string) =
    async { 
        try
            let uri = new System.Uri(url)
            let webClient = new WebClient()
            let! html = webClient.AsyncDownloadString(uri)
            printfn "Read %d characters for %s" html.Length name
        with
            | ex -> printfn "%s" (ex.Message);
    }

let runAll() =
    urlList
    |> Seq.map fetchAsync
    |> Async.Parallel 
    |> Async.RunSynchronously
    |> ignore

runAll()

let rec fib x = if x <= 2 then 1 else fib(x-1) + fib(x-2)
 
let fibs =
    Async.Parallel [ for i in 0..40 -> async { return fib(i) } ]
    |> Async.RunSynchronously

let fibs2 = [for i in 0..40 -> fib(i)]



type Shape = 
    | Rectangle of float * float
    | Circle of float
    | Prism of float * float * float

let rectangle = Rectangle(2.0, 3.0)





[<EntryPoint>]
let main argv = 
    runAll()
    0 // return an integer exit code
