open System
open System.Diagnostics
open System.IO
open System.Threading.Tasks

open Extensions.FsExtensions

[<EntryPoint>]
let main argv =
    Logger.Setup    
    let sw = Stopwatch.StartNew()
    let options = CLIArguments.Parse "gfic" argv
    options.Effect.ToEnum<ImageProcessor.MutateEffect>()
    |> fun enum -> 
        match enum with
        | Some effect -> 
            for format in ["*.jpg"; "*.png"; "*.bmp"; "*.gif"] do
                let opt = ParallelOptions()
                opt.MaxDegreeOfParallelism <- options.Threads
                Directory.GetFiles(options.InputDir, format)
                |> fun files ->
                    Parallel.ForEach(
                        files, 
                        opt, 
                        fun file -> ImageProcessor.Apply(file, options, opt, effect)) 
                |> ignore
            let crlb = Environment.NewLine
            printfn "%sTotal time: %O%s" crlb sw.Elapsed crlb
        | None -> printfn "Unrecognized effect"
    0
