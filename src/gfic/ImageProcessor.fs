module ImageProcessor

open System.Diagnostics
open System.IO
open SixLabors.ImageSharp
open SixLabors.ImageSharp.Processing

let convertToGrayScale file =
    let sw = Stopwatch.StartNew()
    let fi = FileInfo(file)
    let dir = Directory.CreateDirectory(Path.Combine(fi.DirectoryName, "grayscale"))
    let output = Path.Combine(dir.FullName, fi.Name)
    use image = Image.Load(file)
    image.Mutate(fun x -> x.Grayscale() |> ignore)
    image.Save(output)
    printfn "%O - %s" sw.Elapsed output