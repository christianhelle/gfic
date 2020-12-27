open System
open System.Diagnostics
open System.IO
open System.Text
open System.Threading.Tasks
open Exceptionless
open System.Security.Cryptography

let GetUserIdentity =
    try Environment.UserName + "@" + Environment.MachineName
    with | :? System.Exception -> Environment.UserName + "@localhost"

let GetAnonymousIdentity =
    let sb = new StringBuilder()
    GetUserIdentity
        |> Encoding.UTF8.GetBytes
        |> (new SHA256Managed()).ComputeHash
        |> fun hash -> for b in hash do sb.Append(b.ToString("x2")) |> ignore
    sb.ToString()

let SetupLogger =
    GetAnonymousIdentity |> fun sha -> ExceptionlessClient.Default.Configuration.SetUserIdentity(sha, sha.Substring(0,7))
    ExceptionlessClient.Default.Configuration.UseSessions()
    ExceptionlessClient.Default.Startup("4f8WTcyL2cDDRJ90a6khQ1CIGv1lee2WfS3M8JsJ")

[<EntryPoint>]
let main argv =
    SetupLogger
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
