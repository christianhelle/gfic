open System
open System.Diagnostics
open System.IO
open System.Threading.Tasks

[<EntryPoint>]
let main argv =
    let options = CLIArguments.Parse "gfic" argv
    let sw = Stopwatch.StartNew()
    let crlb = Environment.NewLine
    for format in ["*.jpg"; "*.png"] do
        let opt = ParallelOptions()
        opt.MaxDegreeOfParallelism <- options.MaxDegreeOfParallelism
        Directory.GetFiles(options.InputDir, format)
        |> fun files -> 
            Parallel.ForEach(files, opt, fun file -> 
                ImageProcessor.Apply(file, options.OutputDir, options.Effect)) 
        |> ignore
    printfn "%sTotal time: %O%s" crlb sw.Elapsed crlb 
    0
