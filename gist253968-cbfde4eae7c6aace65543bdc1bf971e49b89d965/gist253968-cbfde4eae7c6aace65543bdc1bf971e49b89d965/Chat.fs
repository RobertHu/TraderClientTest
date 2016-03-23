#light

module internal Comet.Chating.Chat

open System
open System.Collections.Concurrent

type ChatMsg = {
    From: string;
    Text: string;
}

let private agentCache = new ConcurrentDictionary<string, MailboxProcessor<ChatMsg>>()

let private agentFactory = new Func<string, MailboxProcessor<ChatMsg>>(fun _ -> 
    MailboxProcessor.Start(fun o -> async { o |> ignore }))

let private GetAgent name = agentCache.GetOrAdd(name, agentFactory)

let send fromName toName msg = 
    let agent = GetAgent toName
    { From = fromName; Text = msg; } |> agent.Post

let receive name = 
    let rec receive' (agent: MailboxProcessor<ChatMsg>) messages = 
        async {
            let! msg = agent.TryReceive 0
            match msg with
            | None -> return messages
            | Some s -> return! receive' agent (s :: messages)
        }

    let agent = GetAgent name

    async {
        let! messages = receive' agent List.empty
        if (not messages.IsEmpty) then return messages
        else
            let! msg = agent.TryReceive 3000
            match msg with
            | None -> return []
            | Some s -> return [s]
    }