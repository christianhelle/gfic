module ImageProcessor

open System.IO
open SixLabors.ImageSharp
open SixLabors.ImageSharp.Processing

let convertToGrayScale file =
    let fi = new FileInfo(file)
    let dir = Directory.CreateDirectory(Path.Combine(fi.DirectoryName, "grayscale"))
    let output = Path.Combine(dir.FullName, fi.Name)
    let image = Image.Load(file)
    image.Mutate(fun x -> x.Grayscale() |> ignore)
    image.Save(output)
    printfn "%s - Done!" output