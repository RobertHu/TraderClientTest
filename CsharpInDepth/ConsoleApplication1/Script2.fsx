open System.IO


let makeFileWritable path =
    match File.Exists path with
    |false -> ()
    |true ->
        let fi= new FileInfo(path)
        if fi.IsReadOnly then fi.IsReadOnly <- false

let targetDir=Path.Combine(__SOURCE_DIRECTORY__,"bin\Release")
let backupDir=Path.Combine(targetDir,"Backup")
let destConfigDir=Path.Combine(__SOURCE_DIRECTORY__,"bin\Release\Config")
let sourceConfig = Path.Combine(__SOURCE_DIRECTORY__,"Config")
let coreFile=Path.Combine(__SOURCE_DIRECTORY__,"Lib\FSharp.Core.dll")
destConfigDir|>Directory.GetFiles|>Array.iter (fun path ->
     makeFileWritable path
     File.Delete path)
sourceConfig|>Directory.GetFiles|>Array.iter (fun p -> File.Copy(p,Path.Combine(destConfigDir,Path.GetFileName(p)),true))
do File.Copy(coreFile,Path.Combine(targetDir,Path.GetFileName(coreFile)),true)
match Directory.Exists(backupDir) with
|true -> 
    backupDir|>Directory.GetDirectories|>Array.iter (fun p ->
        p|>Directory.GetFiles|>Array.iter (fun f ->
            makeFileWritable f
            File.Delete f
            )
        Directory.Delete p
        )
|false -> Directory.CreateDirectory(backupDir)|>ignore

System.Console.ReadKey()|>ignore



