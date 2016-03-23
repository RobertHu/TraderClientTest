module Agent
open System
open System.Net
open System.Net.Sockets
open System.Collections.Generic

type Agent<'a> = MailboxProcessor<'a>

type MsgData =
    |Read
    |Write of byte[]


type MsgAnalysis private() =
    static let instance = new MsgAnalysis()
    let container = new List<byte[]>(100000)
    let syncBlock = new obj()

    member this.Add(msg) =
        lock syncBlock (fun () -> container.Add(msg))

    static member Default = instance

    member this.Start() =
        let rec work() = async{
            lock syncBlock (fun () -> 
                let count = container.Count
                match count with
                |0 ->
                    printfn "msg count = 0 , total amount = 0"
                | _ ->
                    printfn "msg count = %d , total amount = %d" count (count * (container.[0]).Length)
                    container.Clear()
                )
            do! Async.Sleep(5000)
            return! work()
        }
        Async.Start(work())
    

    



type Client(socket: TcpClient) =
    let stream = socket.GetStream()
    let readArr = Array.zeroCreate 1024
    let rec read() = async{
            let! readed = stream.AsyncRead(readArr)
            let data = Array.zeroCreate readed
            System.Array.Copy(readArr,data,readed)
            MsgAnalysis.Default.Add(data) |> ignore
            return! read()
        }
    do
        Async.Start(read()) 
        
    let agent = new Agent<MsgData>(fun inbox ->
        let rec loop() = async{
            let! msg = inbox.Receive()
            match msg with
            |Write(data) ->
                do! stream.AsyncWrite(data)
            |Read -> ()
            return! loop()
            }
        loop()
        )
    
    member this.Start()= agent.Start()

    member this.Send(msg: byte[])=
        match msg with
        | null -> agent.Post(Read)
        | _  ->
             agent.Post(Write(msg))




  