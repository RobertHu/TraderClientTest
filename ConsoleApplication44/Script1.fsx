
open System
open System.Text

let s = "NDIyMjAyMTk4NTA0MDM1Mjkx"

let bytes = Convert.FromBase64CharArray(s.ToCharArray(),0,s.Length)

let target = Encoding.ASCII.GetString(bytes)

printf "%s" target

