module ImageProcessor

open System.Diagnostics
open System.IO
open SixLabors.ImageSharp
open SixLabors.ImageSharp.Processing

let GetOutputFile (input, output) =
    Path.Combine(Directory.GetCurrentDirectory(), output)
    |> Directory.CreateDirectory
    |> fun dir -> Path.Combine(dir.FullName, FileInfo(input).Name)

let ApplyEffect (file:string, outputDir:string, effect:string) =
    let sw = Stopwatch.StartNew()
    use image = Image.Load(file)
    image.Mutate(fun x -> 
        match effect with 
        | "grayscale" -> x.Grayscale() |> ignore
        | "blackwhite" -> x.BlackWhite() |> ignore
        | "lomograph" -> x.Lomograph() |> ignore
        | "kodachrome" -> x.Kodachrome() |> ignore
        | "oilpaint" -> x.OilPaint() |> ignore
        | _ -> printfn "Unknown effect")

    GetOutputFile(file, outputDir) |> image.Save
    printfn "%O - (%s) %s" sw.Elapsed effect file
    
let Apply (file:string, outputDir:string, effect:string) =
    let effects = ["grayscale"; "blackwhite"; "lomograph"; "kodachrome"; "oilpaint"]
    match effect with
    | "all" -> for f in effects do ApplyEffect(file, Path.Combine(outputDir, f), f)
    | _ -> ApplyEffect(file, outputDir, effect)