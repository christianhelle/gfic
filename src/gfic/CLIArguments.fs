module CLIArguments

open Argu
open System.IO
open System.Linq

type Options = {
    Effect : string
    InputDir : string
    OutputDir : string
    Threads : int
    Resize : int
}

module Options = 
    let Empty = {
        Effect = ""
        InputDir = ""
        OutputDir = ".\output"
        Threads = 1
        Resize = 100
    }

[<Literal>]
let EffectsMessage = "Specify the image processing effect. Available effects are grayscale, blackwhite, lomograph, kodachrome, oilpaint, boxblur, all"

type CLIArguments = 
    | [<AltCommandLine("-e")>] Effect of path:string
    | [<AltCommandLine("-i")>] Input of path:string
    | [<AltCommandLine("-o")>] Output of path:string
    | [<AltCommandLine("-m")>] Threads of path:int
    | [<AltCommandLine("-r")>] Resize of path:int
with
    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | Effect _ -> EffectsMessage
            | Input _ -> "Specify a folder for source images"
            | Output _ -> "Specify the output folder."
            | Threads _ -> "Specify the maximum degree of parallelism. Default is 1"
            | Resize _ -> "Resize the image by percentage"

let Validate = function
    | { InputDir = "" } -> "ERROR: missing parameter '--input'." |> Error
    | o -> o |> Ok

let GetPath dir =
    Directory.GetCurrentDirectory()
    |> fun d -> Path.Combine(d, dir)
    |> Directory.CreateDirectory
    |> fun d -> d.FullName

let Parse progName args = 
    let exiter = ProcessExiter () :> IExiter
    let parser = ArgumentParser.Create<CLIArguments>(progName, errorHandler = exiter)
    let rec loop o = function
        | Effect u::t -> t |> loop { o with Effect = u }
        | Input u::t -> t |> loop { o with InputDir = Path.GetDirectoryName(u) }
        | Output u::t -> t |> loop { o with OutputDir = GetPath(u) }
        | Threads u::t -> t |> loop { o with Threads = u }
        | Resize u::t -> t |> loop { o with Resize = u }
        | [] -> o
    parser.Parse args
    |> (fun a -> a.GetAllResults())
    |> loop Options.Empty
    |> Validate
    |> function
        | Ok o -> o
        | Error e ->
            exiter.Exit
                (String.concat "\n" [e;parser.PrintUsage()], 
                ErrorCode.CommandLine)
