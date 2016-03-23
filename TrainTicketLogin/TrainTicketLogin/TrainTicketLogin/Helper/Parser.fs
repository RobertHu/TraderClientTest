module Parser 
open System
open System.Text.RegularExpressions
open System.IO

let getAllText path =
    File.ReadAllText(path,System.Text.Encoding.Default)

let (|Regexer|) pattern =
    new Regex(pattern)

let rec getEqualSignletter (source : char list) (equal : char list) (colon : char list) acc =
    match source with
    |[] -> (List.rev acc)
    | x::xs -> 
        match equal with
        |[] -> 
            match colon with
            |y::_ when y=x -> getEqualSignletter xs equal [] acc
            |_ -> getEqualSignletter xs equal colon (x::acc)
        |y::_  when x=y -> getEqualSignletter xs [] colon acc
        | _ -> getEqualSignletter xs equal colon acc


let toString (input : char list) =
    new System.String(input|>Array.ofList)

let strToList (line: string) =
    line.ToCharArray()
    |>List.ofArray

let test() =
    let line = "var u_name = '胡华波';"
    let result = getEqualSignletter (line.ToCharArray()|>List.ofArray) ['='] [] []
    printfn "%s" (new System.String(result|>Array.ofList))
                

let (|Login|User|Message|Neither|) (line : string) =
    match line.Contains("var isLogin") with
    |true -> 
        Login(getEqualSignletter (strToList(line)) ['='] [] [] |> toString)
    | _ ->
        match line.Contains("var u_name") with
        |true ->
            User(getEqualSignletter(strToList(line)) ['='] [';'] [] |>toString)
        | _  -> 
            match line.Contains("var message") with
            |true ->
                Message(getEqualSignletter(strToList(line)) ['='] [';'] [] |>toString)
            |_  ->
                Neither
    

let (|IsLogin|Failed|) html =
    let sr= new StringReader(html)
    let rec getResult line acc =
        match line with
        |null -> List.rev acc
        |Login loginState -> 
            getResult (sr.ReadLine()) (loginState::acc)
        |User userName ->
            getResult (sr.ReadLine()) (userName::acc)
        |Message msg ->
            getResult (sr.ReadLine()) (msg::acc)
        | _ -> getResult (sr.ReadLine()) acc
    let result = getResult (sr.ReadLine()) [] |> Array.ofList
    match (result.[0]).Trim() with
    |"true" -> IsLogin(result.[1])
    | _ -> Failed(result.[2])




let IsLoginSuccess(input : string) =
    match input with
    |IsLogin result -> (true,result)
    |Failed msg -> (false,msg)



let doTest() =
    let path = @"D:\Work\12306\html.txt"
    let html = getAllText path
    match html with
    |IsLogin result ->
        printfn "%s" result
    |Failed msg -> printfn "%s" msg


