open System
open System.Diagnostics
open System.IO
open System.Threading.Tasks

open Extensions.EnumExtensions

[<EntryPoint>]
let main argv =
    Logger.Setup()
    let sw = Stopwatch.StartNew()
    let opt = CLIArguments.Parse argv
    if (opt.NoLogging) then Logger.OptOut()
    let popt = ParallelOptions()
    popt.MaxDegreeOfParallelism <- opt.Threads
    opt.Effect.ToEnum<ImageProcessor.MutateEffect>()
    |> fun enum -> 
        match enum with
        | Some effect -> 
            for format in ["*.jpg"; "*.png"; "*.bmp"; "*.gif"] do
                Directory.GetFiles(opt.InputDir, format)
                |> fun files ->
                    Parallel.ForEach(
                        files, 
                        popt, 
                        fun file -> ImageProcessor.Apply(file, opt, popt, effect)) 
                |> ignore
            let crlb = Environment.NewLine
            printfn "%sTotal time: %O%s" crlb sw.Elapsed crlb
        | None -> printfn "Unrecognized effect"
    0
