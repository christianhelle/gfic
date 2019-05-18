open System
open System.Diagnostics
open System.IO

open ImageProcessor

[<EntryPoint>]
let main argv =
    let sw = Stopwatch.StartNew()
    let crlb = Environment.NewLine
    for f in ["*.jpg"; "*.png"] do
        Directory.GetFiles(Directory.GetCurrentDirectory(), f)
        |> Array.map Path.GetFullPath 
        |> Array.iter convertToGrayScale    
    printfn "%sTotal time: %O%s" crlb sw.Elapsed crlb 
    0 // return an integer exit code
