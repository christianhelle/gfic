// Learn more about F# at http://fsharp.org

open System.IO

open ImageProcessor

[<EntryPoint>]
let main argv =
    printfn "gfic image converter"
    let files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.jpg")
    printfn "Found %d file(s)" files.Length
    files
    |> Array.map Path.GetFullPath 
    |> Array.iter convertToGrayScale 
    0 // return an integer exit code
