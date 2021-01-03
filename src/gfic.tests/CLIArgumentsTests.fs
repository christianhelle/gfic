module ``CLIArguments Tests``

open Xunit
open System
open System.IO

let GetArgs =
    [| "--input";"../../../../../images/original/";"--effect";"all";"--no-logging" |]

[<Fact>]
let ``Parse returns not null`` () =
    GetArgs |> CLIArguments.Parse |> Assert.NotNull
    
[<Fact>]
let ``Parse reads --no-logging`` () =
    GetArgs |> CLIArguments.Parse |> fun o -> Assert.True(o.NoLogging)
    
[<Fact>]
let ``Parse reads --effect`` () =
    GetArgs |> CLIArguments.Parse |> fun o -> Assert.Equal("all", o.Effect)
    
[<Fact>]
let ``Parse reads --input`` () =
    GetArgs 
    |> CLIArguments.Parse
    |> fun o -> 
        let expected = Path.GetDirectoryName("../../../../../images/original/")
        Assert.Equal(expected, o.InputDir)