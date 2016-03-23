
type Distribution<'T when 'T: comparison> =
    abstract Sample : 'T
    abstract Support: Set<'T>
    abstract Expectation: ('T -> float) -> float


let always x =
    { new Distribution<'T> with
        member d.Sample = x
        member d.Support = Set.singleton x
        member d.Expectation(H) = H(x)}

let rnd = System.Random()

let coinFlip (p: float) (d1: Distribution<'T>) (d2: Distribution<'T>) = 
    if p < 0.0 || p > 1.0 then failwith "invalid probability in coinFlip"
    {new Distribution<'T> with
        member d.Sample = 
            if rnd.NextDouble() < p then d1.Sample else d2.Sample
        member d.Support = Set.union d1.Support d2.Support
        member d.Expectation(H) =
            p * d1.Expectation(H) + (1.0 - p) * d2.Expectation(H) }


let bind (dist: Distribution<'T>) (k: 'T -> Distribution<'U>) =
    { new Distribution<'U> with
        member d.Sample = 
            (k dist.Sample).Sample
        member d.Support =
            Set.unionMany (dist.Support |> Set.map(fun d -> (k d).Support))
        member d.Expectation H =
            dist.Expectation(fun x -> (k x).Expectation H) }


type DistributionBuilder() =
    member x.Delay f = bind (always ()) f

    member x.Bind(d, f) = bind d f

    member x.Return v = always v

    member x.ReturnFrom vs = vs

let dist = new DistributionBuilder()


let weightedCases (inp: ('T * float) list) = 
    let rec coinFlips w l = 
        match l with
        | [] -> failwith "no coinFlips"
        | [(d, _)] -> always d
        | (d, p) :: rest -> coinFlip (p / (1.0 - w)) (always d) (coinFlips (w + p) rest)
    coinFlips 0.0 inp


let countedCases inp = 
    let total = Seq.sumBy (fun (_, v) -> v) inp
    weightedCases (inp |> List.map (fun (x, v) -> (x, float v / float total)))


type Outcome = Even | Odd | Zero

let roulette = countedCases [Even, 18; Odd, 18; Zero, 1]

type Light = Red | Green | Yellow

let trafficLightD = weightedCases [Red, 0.50; Yellow, 0.10; Green, 0.40]

type Action = Stop | Drive

let cautiousDriver light = 
    dist{
        match light with
        | Red -> return Stop
        | Yellow -> return! weightedCases [Stop, 0.9; Drive, 0.1]
        | Green -> return Drive }

let aggressiveDriver light = 
    dist{
        match light with
        | Red -> return! weightedCases [Stop, 0.9; Drive, 0.1]
        | Yellow -> return! weightedCases [Stop, 0.1; Drive, 0.9]
        | Green -> return Drive
    }

let otherLight light = 
    match light with
    | Red -> Green
    | Yellow -> Red
    | Green -> Red

type CrashResult = Crash | NoCrash

let crash (driverOneD, driverTwoD, lightD) =
    dist{
        let! light = lightD
        let! driverOne = driverOneD light
        let! driverTwo = driverTwoD (otherLight light)
        match driverOne, driverTwo with
        | Drive, Drive -> return! weightedCases [Crash, 0.9; NoCrash, 0.1]
        | _ -> return NoCrash
    }


