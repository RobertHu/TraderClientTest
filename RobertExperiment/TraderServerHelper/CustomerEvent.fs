module CustomerEvent

type MyEventArgs(msg: string) =
   inherit System.EventArgs()
   member this.Message = msg

type myEvent = delegate of obj * MyEventArgs -> unit

type MyEventSource() =
    
    let event = new Event<myEvent,MyEventArgs>()

    [<CLIEvent>]
    member this.Started = event.Publish

    member this.Start() =
        event.Trigger(this,new MyEventArgs("triggered"))
