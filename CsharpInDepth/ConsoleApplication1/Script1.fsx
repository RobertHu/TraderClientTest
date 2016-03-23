let f() = printfn "f"

let g() = printfn "g"

let h()= printfn "f"

let ops =[
    1000,f
    2000,g
    1000,h
    ]

let runOps ops =
    async {
        for time ,op in ops do
            do! Async.Sleep time
            op()
    } |> Async.StartImmediate

runOps ops

