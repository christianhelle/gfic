open System
open System.IO

open ImageProcessor

[<EntryPoint>]
let main argv =
    printfn "%sgfic image converter" Environment.NewLine
    for f in ["*.jpg"; "*.png"] do
        Directory.GetFiles(Directory.GetCurrentDirectory(), f)
        |> Array.map Path.GetFullPath 
        |> Array.iter convertToGrayScale
    
    printfn "%s" Environment.NewLine
    0 // return an integer exit code
