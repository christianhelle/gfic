open System
open System.Diagnostics
open System.IO
open System.Threading.Tasks
open Exceptionless

[<EntryPoint>]
let main argv =
    ExceptionlessClient.Default.Startup("4f8WTcyL2cDDRJ90a6khQ1CIGv1lee2WfS3M8JsJ")
    let options = CLIArguments.Parse "gfic" argv
    let sw = Stopwatch.StartNew()
    let crlb = Environment.NewLine
    for format in ["*.jpg"; "*.png"; "*.bmp"; "*.gif"] do
        let opt = ParallelOptions()
        opt.MaxDegreeOfParallelism <- options.Threads
        Directory.GetFiles(options.InputDir, format)
        |> fun files -> 
            Parallel.ForEach(files, opt, fun file -> 
                ImageProcessor.Apply(file, options, opt)) 
        |> ignore
    printfn "%sTotal time: %O%s" crlb sw.Elapsed crlb 
    0
