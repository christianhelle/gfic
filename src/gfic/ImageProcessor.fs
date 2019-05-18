module ImageProcessor

open System.Diagnostics
open System.IO
open SixLabors.ImageSharp
open SixLabors.ImageSharp.Processing

let ToGrayScale (file, outputDir:string) =
    let sw = Stopwatch.StartNew()
    let fi = FileInfo(file)
    let outputPath = Path.Combine(Directory.GetCurrentDirectory(), outputDir)
    let dir = Directory.CreateDirectory(outputPath)
    let output = Path.Combine(dir.FullName, fi.Name)
    use image = Image.Load(file)
    image.Mutate(fun x -> x.Grayscale() |> ignore)
    image.Save(output)
    printfn "%O - %s" sw.Elapsed output