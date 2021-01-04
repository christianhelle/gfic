module ``ImageResizer Tests``

open Xunit
open System.IO

open SixLabors.ImageSharp

[<Fact>]
let ``Can Resize`` () = 
    EmbeddedResource.ExtractAll
    for name in ["guitar1.jpg";"guitar2.jpg"] do
        Path.Combine(Directory.GetCurrentDirectory(), name)
        |> Image.Load
        |> fun img -> 
            let height = img.Height
            let width = img.Width
            ImageResizer.Resize(50, img)
            Assert.True(img.Height < height)
            Assert.True(img.Width < width)