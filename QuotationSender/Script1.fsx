

type Attempt<'T> = (unit -> 'T option) 

let succeed x = (fun () -> Some(x)) : Attempt<'T>

let fail = (fun () -> None) : Attempt<'T>

let runAttempt (a: Attempt<'T>) = a()

let bind p rest = match runAttempt p with None -> fail | Some r -> (rest r)

let delay f = (fun () -> runAttempt (f()))

let combine p1 p2 = (fun () -> match p1() with None -> p2() | res -> res)

type AttemptBuilder() =
    
    member b.Return(x) = succeed x

    member b.Bind(p, rest) = bind p rest

    member b.Delay(f) = delay f

    member b.Let(p, rest) : Attempt<'a> = rest p

    member b.ReturnFrom (x: Attempt<'T>) = x

    member b.Combine(p1: Attempt<'T>, p2: Attempt<'T>) = combine p1 p2

    member b.Zero() = fail

let attemp = new AttemptBuilder()

let failIfBig n = attemp {if n > 1000 then return! fail else return n}

let failIfEitherBig (inp1, inp2) = attemp{
    let! n1 = failIfBig inp1
    let! n2 = failIfBig inp2
    return (n1, n2)
}

let sumIfBothSmall (inp1, inp2) =
    attemp{
        let! n1 = failIfBig inp1
        printfn "Hey, n1 was small!"
        let! n2 = failIfBig inp2
        printfn "n2 was also small"
        let sum = n1 + n2
        return sum
    }


