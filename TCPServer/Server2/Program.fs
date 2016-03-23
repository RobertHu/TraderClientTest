open System.Net
open System.Net.Sockets
open System.Text
open System.IO;
open System.Xml
open System.Xml.Linq
open System.Linq

let buffer =Array.create 4996 0uy 
let SUCCESS = 2

let ConvertToByte (content:string) = Encoding.ASCII.GetBytes content
let ConvertToString buf len = Encoding.ASCII.GetString(buf,0,len)


let md5Cache = new System.Collections.Generic.Dictionary<string,string>()
let GetMD5 path =
    match md5Cache.ContainsKey(path) with
    | true -> md5Cache.[path]
    | false ->
        let sb = System.Text.StringBuilder()
        let md5Hasher = System.Security.Cryptography.MD5.Create()
        use fs =File.OpenRead path
        md5Hasher.ComputeHash(fs)
        |>Array.iter (fun byte -> sb.Append(byte.ToString("x2").ToLower())|>ignore)
        md5Cache.[path] <- sb.ToString()
        md5Cache.[path]

let GetUpdateFile() =
    let root = XElement.Load("UpdateFiles.xml");
    root.Elements(XName.Get "File")
    |>Seq.map (fun m -> m.Value)

let GetShoudUpdateFile (md5Str:string) =
    md5Str.Split('|')
    |>Array.filter(fun m -> not (System.String.IsNullOrEmpty(m)))
    |>Array.map (fun m -> 
        let target = m.Split(',')
        target.[0],target.[1]
        )
    |>Array.filter (fun (file,md5) ->
        match md5 with
        |"" -> true
        |_  -> (GetMD5 file) <> md5
        )
    |>Array.map(fun (m,n) -> m);
    
    


//let fileName ="Clock.exe"
let DoWork (client:TcpClient) fileName =
    async{
        let stream = client.GetStream()
        let startContent = ConvertToByte "|"
        do! stream.AsyncWrite(startContent,0,startContent.Length)
        let! rc1 =stream.AsyncRead(buffer,0,buffer.Length)
        printfn "%s" (ConvertToString buffer rc1)
        if rc1=SUCCESS then
            let fileNameContent =ConvertToByte fileName
            do! stream.AsyncWrite(fileNameContent,0,fileNameContent.Length)
            let! rc2 =stream.AsyncRead(buffer,0,buffer.Length)
            printfn "%s" (ConvertToString buffer rc2)
            if rc2 =SUCCESS then
                use fs= new FileStream(fileName,FileMode.Open,FileAccess.Read)
                let buf =Array.create 1024 0uy
                let count= ref (fs.Read(buf,0,buf.Length))
                while !count > 0 do
                    do! stream.AsyncWrite(buf,0,!count)
                    count:=fs.Read(buf,0,buf.Length)
                do! Async.Sleep(1000)
                do! stream.AsyncWrite(startContent,0,startContent.Length)
                let! rc3 =stream.AsyncRead(buffer,0,buffer.Length)
                printfn "%s" (ConvertToString buffer rc3)
    }


let getIPAndPort() =
    let root = XElement.Load("Setting.xml")
    let ip = root.Elements(XName.Get "Setting").Single(fun m -> m.Attribute(XName.Get "name").Value = "ip").Value
    let port =root.Elements(XName.Get "Setting").Single(fun m -> m.Attribute(XName.Get "name").Value = "port").Value
    (ip,port)



let main()= 
    let addr,port = getIPAndPort()
    let ip= IPAddress.Parse(addr)
    let listener = TcpListener(ip,System.Int32.Parse(port))
    listener.Start()
    printfn "Start listening..."
    while true do
        let client = listener.AcceptTcpClient()
        try
            async{
                let files =GetUpdateFile()|>Seq.fold (fun acc m -> sprintf "%s|%s" acc m) ""
                let fileBuf =files|>ConvertToByte
                let stream = client.GetStream()
                do! stream.AsyncWrite(fileBuf,0,fileBuf.Length)
                do! Async.Sleep(1000)
                let! md5FileCount = stream.AsyncRead(buffer,0,buffer.Length)
                let md5Str= ConvertToString buffer md5FileCount
                for m in GetShoudUpdateFile md5Str do
                    do! DoWork client m
                client.Close()       
            }|>Async.Start
        with
        |ex -> client.Close()

do
    main()

