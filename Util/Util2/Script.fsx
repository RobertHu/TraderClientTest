// This file is a script that can be executed with the F# Interactive.  
// It can be used to explore and test the library project.
// Note that script files will not be part of the project build.

open System.Text.RegularExpressions

let pattern = @"^\+?\d+[0-9;]*\d+$"
let regex = new Regex(pattern)
let input = "86"
let x = regex.IsMatch(input)




