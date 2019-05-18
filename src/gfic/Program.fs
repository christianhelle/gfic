open System
open System.Diagnostics
open System.IO
open System.Threading
open System.Threading.Tasks

[<EntryPoint>]
let main argv =
    let options = CLIArguments.Parse "gfic" argv
    let sw = Stopwatch.StartNew()
    let crlb = Environment.NewLine
    for format in ["*.jpg"; "*.png"] do
        Directory.GetFiles(options.InputDir, format)
        |> fun files -> 
            Parallel.ForEach(files, fun file -> 
                ImageProcessor.ToGrayScale(file, options.OutputDir)) 
        |> ignore
    printfn "%sTotal time: %O%s" crlb sw.Elapsed crlb 
    0
