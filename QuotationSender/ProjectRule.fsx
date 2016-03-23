




//let sum n = Seq.fold (fun s t -> if t % 5 = 0 || t % 3 = 0 then  t + s else s ) 0 [1..(n - 1)]

open System.Collections.Generic


let rec normalFib n = 
    match n with
    | 1 -> 1
    | 2 -> 2
    | _  ->  normalFib (n - 1)  + normalFib (n - 2)

let memFib () = 
    let mem = new Dictionary<int, int>()
    let rec fib n = 
        if mem.ContainsKey(n) then mem.Item n
        else
            match n with
            | 1 ->  mem.Add(n, 1) |> ignore; 1
            | 2 -> mem.Add(n, 2) |> ignore ; 2
            | _  -> 
                let result = fib (n - 1)  + fib (n - 2)
                mem.Add(n, result) |> ignore
                result
    fib

let myFib = memFib()

let normalSumFib =
    let target = 400000000
    Seq.initInfinite (fun x -> x + 1) |>Seq.takeWhile (fun m -> (normalFib m) <= target ) 
    |>Seq.map (fun m -> normalFib m)
    |>Seq.filter (fun m -> m % 2 = 0)
    |>Seq.sum

let sumFib upperLimit  = 
    Seq.initInfinite (fun x -> x + 1) |>Seq.takeWhile (fun m -> (myFib m) <= upperLimit ) 
    |>Seq.map (fun m -> myFib m)
    |>Seq.filter (fun m -> m % 2 = 0)
    |>Seq.sum
    

let rec primeFactor xs xss acc =
    match xs with
    | x :: xy -> 
        if List.exists (fun m -> x % m = 0) (List.filter (fun m -> m <> x) xss) then primeFactor xy xss acc
        else primeFactor xy xss (x :: acc)
    | []  -> acc

let factor (n: uint64) = 
    let sqt =  int (System.Math.Sqrt(float n))
    let primes = 
        [2..sqt]
        |>Seq.filter(fun m -> n % uint64 m = uint64 0)
        |>List.ofSeq
    primeFactor primes primes []



let isPrime n =
    if n = 2 then true 
    else
        let sqt = int (System.Math.Sqrt(float n))
        let result =
            [2..sqt]
            |> Seq.exists (fun m -> n % m = 0)
        not result

    
let rec findPrime n acc result = 
    if acc = 10001 then result
    else 
        if isPrime n then findPrime (n + 1) (acc + 1) n
        else findPrime (n + 1) acc result


let findPrimes = findPrime 2 0 0





//let ishuiw x = 
    

let rec reverse x acc = 
    if x = 0 then acc 
    else
        reverse  (x / 10) (acc * 10 +  x % 10)

