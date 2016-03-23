type Agent<'msg> = MailboxProcessor<'msg>


let agent =
   Agent.Start(fun inbox ->
     async { while true do
               let! msg = inbox.Receive()
               printfn "got message '%s'" msg } )

for i in 1 .. 10000 do
   agent.Post (sprintf "message %d" i)
