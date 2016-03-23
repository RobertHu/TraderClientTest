
let counter = MailboxProcessor.Start(fun inbox ->
        let rec loop n = 
            async{ do printfn "n = %d, waiting..." n
                   let! msg = inbox.Receive()
                   return! loop(n + msg)}
        loop 0
    )

counter.Post(6)



type msg =
    | Incr of int
    | Fetch of AsyncReplyChannel<int>
 
let counter2 =
    MailboxProcessor.Start(fun inbox ->
        let rec loop n =
            async { let! msg = inbox.Receive()
                    match msg with
                    | Incr(x) -> return! loop(n + x)
                    | Fetch(replyChannel) ->
                        replyChannel.Reply(n)
                        return! loop(n) }
        loop 0)


counter2.Post(Incr 2)

counter2.PostAndReply(fun pr -> Fetch pr)