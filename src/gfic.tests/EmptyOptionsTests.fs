module ``Empty Options Tests``

open Xunit
open Options

[<Fact>]
let ``Empty returns not null`` () =
    Assert.NotNull(Options.Empty)

[<Fact>]
let ``Resize default value is 100 `` () =
    Assert.Equal(Options.Empty.Resize, 100)

[<Fact>]
let ``NoLogging default value is false`` () =
    Assert.False(Options.Empty.NoLogging)

[<Fact>]
let ``Threads default value is 1`` () =
    Assert.Equal(Options.Empty.Threads, 1)

[<Fact>]
let ``OutputDir default value is output`` () =
    Assert.Contains("output", Options.Empty.OutputDir)