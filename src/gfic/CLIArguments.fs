module CLIArguments

open Argu
open System.IO

type Options = {
  InputDir : string
  OutputDir : string
  MaxDegreeOfParallelism : int
}

module Options = 
  let Empty = {
    InputDir = ""
    OutputDir = ".\output"
    MaxDegreeOfParallelism = 1
  }

type CLIArguments = 
    | [<AltCommandLine("-i")>] Input of path:string
    | [<AltCommandLine("-o")>] Output of path:string
    | [<AltCommandLine("-m")>] MaxDegreeOfParallelism of path:int
with
    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | Input _ -> "Specify a folder for source images"
            | Output _ -> "Specify the output folder."
            | MaxDegreeOfParallelism _ -> "Specify the maximum degree of parallelism (Default is 1)"

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
        | Input u::t -> t |> loop { o with InputDir = Path.GetDirectoryName(u) }
        | Output u::t -> t |> loop { o with OutputDir = GetPath(u) }
        | MaxDegreeOfParallelism u::t -> t |> loop { o with MaxDegreeOfParallelism = u }
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
