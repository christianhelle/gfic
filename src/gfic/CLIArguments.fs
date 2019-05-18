module CLIArguments

open Argu
open System.IO

type Options = {
  InputDir : string
  OutputDir : string
}

module Options = 
  let Empty = {
    InputDir = ""
    OutputDir = ".\output"
  }

type CLIArguments = 
    | [<AltCommandLine("-i")>] Input of path:string
    | [<AltCommandLine("-o")>] Output of path:string
with
    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | Input _ -> "specify a folder for source images"
            | Output _ -> "specify the output folder."

let validate = function
    | { InputDir = "" } -> "ERROR: missing parameter '--input'." |> Error
    | o -> o |> Ok

let Parse progName args = 
    let exiter = ProcessExiter () :> IExiter
    let parser = ArgumentParser.Create<CLIArguments>(progName, errorHandler = exiter)
    let rec loop o = function
        | Input u::t -> t |> loop { o with InputDir = Path.GetDirectoryName(u) }
        | Output u::t -> t |> loop { o with OutputDir = Path.GetDirectoryName(u) }
        | [] -> o
    parser.Parse args
    |> (fun a -> a.GetAllResults())
    |> loop Options.Empty
    |> validate
    |> function
        | Ok o -> o
        | Error e ->
            exiter.Exit
                (String.concat "\n" [e;parser.PrintUsage()], 
                ErrorCode.CommandLine)
