// Learn more about F# at http://fsharp.net

module Module1
open System.Net
open System.IO
type pubDelegate = delegate of obj*string -> unit

type Publisher(name:string) =
    let m = new Event<pubDelegate,string>()
    let myName = name
    
    [<CLIEvent>]
    member this.Interest = m.Publish

    member this.Publish(content:string) = 
        m.Trigger(this,content)


type SampingAgent() as this =
    let notify = new Event<pubDelegate,string>()
    let agent =
        new MailboxProcessor<_>(fun inbox ->
            async{
                let count =ref 0
                while true do
                    let! msg = inbox.Receive()
                    this.RaiseEvent (sprintf "%s %d" msg !count)
                    let uri = System.Uri(msg)
                    //use webclient = new WebClient()
                    //le//userAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0;)"
                    //webclient.Headers.Add(HttpRequestHeader.UserAgent, userAgent)
                    //let html = webclient.DownloadString(uri)
                    let request =WebRequest.Create(uri)
                    let response = request.GetResponse()
                    let stream = response.GetResponseStream()
                    use sr = new StreamReader(stream)
                    let content = sr.ReadToEnd()
                    incr count
                    this.RaiseEvent (sprintf "%s %d" msg !count)
            }
            )
    [<CLIEventAttribute>]
    member this.Notified = notify.Publish

    member this.RaiseEvent m = notify.Trigger(this,m)

    member this.Start() = agent.Start()

    member this.Post msg = agent.Post msg