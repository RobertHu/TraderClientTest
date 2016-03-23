// Learn more about F# at http://fsharp.net
open System.Net


let work =async{
    use wb = new WebClient();
    let! content = wb.AsyncDownloadString(new System.Uri("http://www.baidu.com"))
    printfn "%s" content
}

do
    work|>Async.RunSynchronously
   
