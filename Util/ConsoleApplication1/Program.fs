// Learn more about F# at http://fsharp.net
open Module1
open FileExtension.FileHelper
open FileExtension.Model
open System.IO
open System.Xml
open System.Xml.Linq
open System.Linq
let dir="D:\Teams\iExchangeCollection\iExchange3 Team\iExchange3"
let target="D:\Work\hu.xml"
let sectionName="appSettings"
//let s = GetConfigFile "D:\Teams\iExchangeCollection\iExchange3 Team\iExchange3"|>Seq.nth 0 |>(fun m -> GetConfigContent (snd m))


let convertSeqToTuple init =
    init|>Seq.map (fun m -> (m,m*m))|>Map.ofSeq


//let main() =
//    GetConfigFile dir |>Seq.map (fun (m,n) ->( (GetDirectoryShortName m),(GetConfigContent n "configuration")))
//    |>Seq.iter (fun (m,n)->
//        let destination = target.Replace("hu",m)
//        File.Copy(target,destination)
//        let root=XElement.Load(destination)
//        //let s= getAppSettingsSection root
//        root.Descendants(XName.Get sectionName).Single().Descendants()|>List.ofSeq|>List.iter (fun ele-> ele.Remove())
//        root.Save(destination)
//        let root2=XElement.Load(destination)
//        n|>Seq.iter (fun (key,value)->
//            let element=new XElement(XName.Get "add")
//            element.SetAttributeValue((XName.Get "key"),key)
//            element.SetAttributeValue((XName.Get "value"),value)
//            root2.Descendants(XName.Get sectionName).Single().Add(element)
//              )
//        root2.Save(destination)
//        )
let moveFile (file:string) =
    let dir="Z:\Common\SwapFiles\Robert\NewPublish\FlexInterfaceService\sql"
    let index=file.LastIndexOf('\\')
    let des=Path.Combine(dir,file.Substring(index + 1))
    File.Move(file,des)


//let format (source:string array) =
//    source|>Array.fold (fun m n-> m + "," + n) ""
//
//let configFileInfo={Path="D:\Teams\iExchangeCollection\iExchange3 Team\iExchange3.Security\Web\web.config";RootElement="configuration";Sections=[|{Name="appSettings";Node={Name="add";Attrs=[|"key";"value"|]}};{Name="SecuritySystemConfiguration";Node={Name="add";Attrs=[|"key";"value"|]}}|]}
//GetConfigContent configFileInfo|>Seq.iter (fun (x,y,z,w) ->
//    printfn "%s   %s   %s  %d" x y (format z) (w|>Array.ofSeq|>(fun m ->m.Length))
//    w|>Seq.iter (fun dict -> 
//        printfn "%d" dict.Count
//        printfn "%d  %s %s" dict.Count (format (dict.Keys.ToArray())) (format (dict.Values.ToArray()))
//        )
//    )



do
moveFile("D:\Teams\iExchangeCollection\iExchange3 Team\iExchange3.SqlServer\iExchange.Db\Schema Objects\Schemas\dbo\Programmability\Functions\FV_FlexConvertPLTypeToAccountingType.function.sql")

System.Console.Read()|>ignore
