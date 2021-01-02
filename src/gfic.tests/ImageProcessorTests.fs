module ``ImageProcessor Tests``

open Xunit
open System.IO
open System.Threading.Tasks

open Options
open ImageProcessor

let ArrangeAndAct (effect:MutateEffect) =
    EmbeddedResource.ExtractAll
    for name in ["guitar1.jpg";"guitar2.jpg"] do
        Path.Combine(Directory.GetCurrentDirectory(), name)
        |> fun file ->
            ImageProcessor.Apply(
                file,
                Options.Empty,
                ParallelOptions(),
                effect)
    effect

let Assert effect =
    if effect <> MutateEffect.All then do
        let output = Path.Combine(Directory.GetCurrentDirectory(), "output", effect.ToString())
        output |> Directory.Exists |> Assert.True
        output |> Directory.EnumerateFiles |> Assert.NotEmpty

[<Fact>]
let ``Can Process Blackwhite`` () = ArrangeAndAct MutateEffect.Blackwhite |> Assert

[<Fact>]
let ``Can Process Boxblur`` () = ArrangeAndAct MutateEffect.Boxblur |> Assert

[<Fact>]
let ``Can Process Gaussianblur`` () = ArrangeAndAct MutateEffect.Gaussianblur |> Assert

[<Fact>]
let ``Can Process Gaussiansharpen`` () = ArrangeAndAct MutateEffect.Gaussiansharpen |> Assert

[<Fact>]
let ``Can Process Glow`` () = ArrangeAndAct MutateEffect.Glow |> Assert

[<Fact>]
let ``Can Process Grayscale`` () = ArrangeAndAct MutateEffect.Grayscale |> Assert

[<Fact>]
let ``Can Process Invert`` () = ArrangeAndAct MutateEffect.Invert |> Assert

[<Fact>]
let ``Can Process Kodachrome`` () = ArrangeAndAct MutateEffect.Kodachrome |> Assert

[<Fact>]
let ``Can Process Lomograph`` () = ArrangeAndAct MutateEffect.Lomograph |> Assert

[<Fact>]
let ``Can Process Oilpaint`` () = ArrangeAndAct MutateEffect.Oilpaint |> Assert

[<Fact>]
let ``Can Process Pixelate`` () = ArrangeAndAct MutateEffect.Pixelate |> Assert

[<Fact>]
let ``Can Process Polaroid`` () = ArrangeAndAct MutateEffect.Polaroid |> Assert

[<Fact>]
let ``Can Process Sepia`` () = ArrangeAndAct MutateEffect.Sepia |> Assert

[<Fact>]
let ``Can Process Vignette`` () = ArrangeAndAct MutateEffect.Vignette |> Assert