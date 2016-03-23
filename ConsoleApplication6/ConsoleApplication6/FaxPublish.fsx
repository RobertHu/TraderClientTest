open System.IO

let currentDir= __SOURCE_DIRECTORY__
let debug=Path.Combine(currentDir,"bin\Debug")
let release =Path.Combine(currentDir,"bin\Release")

let source=Path.Combine(currentDir,"ReferenceAssembly\FSharp.Core.dll")

let copy dest =
    let destFile = Path.Combine(dest,Path.GetFileName(source))
    match File.Exists(destFile) with
    |true -> ()
    |false -> File.Copy(source,destFile,true)

copy debug
copy release