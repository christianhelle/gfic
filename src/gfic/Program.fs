open System
open System.Diagnostics
open System.IO
open System.Threading.Tasks

open ImageProcessor

[<EntryPoint>]
let main argv =
    let options = CLIArguments.Parse "gfic" argv
    let sw = Stopwatch.StartNew()
    let crlb = Environment.NewLine
    for format in ["*.jpg"; "*.png"] do
        let files = Directory.GetFiles(options.InputDir, format)
        Parallel.ForEach(files, fun file -> 
            convertToGrayScale(file, options.OutputDir)) |> ignore
    printfn "%sTotal time: %O%s" crlb sw.Elapsed crlb 
    0
