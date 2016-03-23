// Learn more about F# at http://fsharp.net

module Module1
open System.IO
open System.Xml
open System.Xml.Linq
open System.Linq
open System.Configuration
let rec getConfigFile dir =
    Seq.append (dir |> Directory.GetFiles |>Seq.filter (fun m -> m.IndexOf("Web.config")<> -1) |>(fun m -> if (Seq.isEmpty m) then Seq.empty else Seq.ofList [(dir,(Seq.nth 0 m))]))
               (dir |>Directory.GetDirectories|>Seq.map getConfigFile|>Seq.concat)

let getConfigContent (configFile:string) =
    let root= XElement.Load(configFile).Descendants(XName.Get "appSettings").Single()
    root.Descendants()|>Seq.map (fun m -> (m.Attribute(XName.Get "key").Value,m.Attribute(XName.Get "value").Value))
    

let rec IsDirectoryContainsConfigFile dir =
    dir|> Directory.GetFiles|>Seq.filter (fun m -> m.IndexOf("Web.config")<> -1)|> Seq.isEmpty
    



    
    






