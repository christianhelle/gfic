// Learn more about F# at http://fsharp.org

open System
open System.IO

open ImageProcessor

[<EntryPoint>]
let main argv =
    printfn "%sgfic image converter" Environment.NewLine
    let files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.jpg")
    printfn "Found %d file(s)%s" files.Length Environment.NewLine
    files
    |> Array.map Path.GetFullPath 
    |> Array.iter convertToGrayScale
    printfn "%s" Environment.NewLine 
    0 // return an integer exit code
