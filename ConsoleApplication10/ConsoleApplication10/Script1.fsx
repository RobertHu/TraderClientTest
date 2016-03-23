open System.Net
open System.IO

let http url =

    async { let req =  WebRequest.Create(System.Uri url)
            use! resp = req.AsyncGetResponse()

            use stream = resp.GetResponseStream()

            use reader = new StreamReader(stream)

            let contents = reader.ReadToEnd()

            return contents }

 

let sites = ["http://www.hao123.com"]

 

let htmlOfSites =

    Async.Parallel [for site in sites -> http site ]

    |> Async.RunSynchronously
