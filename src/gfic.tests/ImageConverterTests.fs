module ``ImageConverter Tests``

open Xunit
open System.IO

open SixLabors.ImageSharp

let SaveTo format =
    EmbeddedResource.ExtractAll()
    for name in ["guitar1.jpg";"guitar2.jpg"] do
        Path.Combine(Directory.GetCurrentDirectory(), name)
        |> Image.Load
        |> fun img ->
            let newName = "new-"+name
            ImageConverter.Save(newName, img, "./output", format)
            let path = Path.Combine(Directory.GetCurrentDirectory(), "output", newName)
            File.Exists(path) |> Assert.True

[<Fact>]
let ``Can Save to JPG`` () = SaveTo "JPG"

[<Fact>]
let ``Can Save to PNG`` () = SaveTo "PNG"

[<Fact>]
let ``Can Save to BMP`` () = SaveTo "BMP"

[<Fact>]
let ``Can Save to GIF`` () = SaveTo "GIF"