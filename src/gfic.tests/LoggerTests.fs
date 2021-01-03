module ``Logger Tests``

open Xunit
open Exceptionless

[<Fact>]
let ``GetUserIdentity returns not null`` () =
    Logger.GetUserIdentity |> Assert.NotNull

[<Fact>]
let ``GetAnonymousIdentity returns not null`` () =
    Logger.GetAnonymousIdentity |> Assert.NotNull

[<Fact>]
let ``Logging uses sessions`` () =
    Logger.Setup
    ExceptionlessClient.Default.Configuration.SessionsEnabled
    |> Assert.True
    
[<Fact>]
let ``Logging include username`` () =
    Logger.Setup
    ExceptionlessClient.Default.Configuration.IncludeUserName
    |> Assert.True
    
[<Fact>]
let ``Shutdown disables logging`` () =
    Logger.OptOut
    ExceptionlessClient.Default.Configuration.Enabled
    |> Assert.False