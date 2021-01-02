module Logger

open System
open System.Text
open Exceptionless
open System.Security.Cryptography

let GetUserIdentity =
    try Environment.UserName + "@" + Environment.MachineName
    with | _ex -> Environment.UserName + "@localhost"

let GetAnonymousIdentity =
    let sb = new StringBuilder()
    GetUserIdentity
    |> Encoding.UTF8.GetBytes
    |> (new SHA256Managed()).ComputeHash
    |> fun hash -> for b in hash do sb.Append(b.ToString("x2")) |> ignore
    sb.ToString()

let Setup =
    GetAnonymousIdentity |> fun sha -> ExceptionlessClient.Default.Configuration.SetUserIdentity(sha, sha.Substring(0,7))
    ExceptionlessClient.Default.Configuration.UseSessions()
    ExceptionlessClient.Default.Startup("4f8WTcyL2cDDRJ90a6khQ1CIGv1lee2WfS3M8JsJ")

let OptOut = ExceptionlessClient.Default.Shutdown()