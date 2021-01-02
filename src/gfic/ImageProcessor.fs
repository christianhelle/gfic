module ImageProcessor

open System
open System.Diagnostics
open System.IO
open System.Threading.Tasks
open SixLabors.ImageSharp
open SixLabors.ImageSharp.Processing
open Exceptionless

open Options

type MutateEffect = 
    None = 0
    | Grayscale = 1
    | Blackwhite = 2
    | Lomograph = 3
    | Kodachrome = 4
    | Oilpaint = 5
    | Boxblur = 6
    | Gaussianblur = 7
    | Gaussiansharpen = 8
    | Glow = 9
    | Invert = 10
    | Pixelate = 11
    | Polaroid = 12
    | Sepia = 13
    | Vignette =14
    | All = 15

let GetOutputFile (input, output) =
    Path.Combine(Directory.GetCurrentDirectory(), output)
    |> Directory.CreateDirectory
    |> fun dir -> Path.Combine(dir.FullName, FileInfo(input).Name)

let Resize (percentage:int, image:Image) =
    ExceptionlessClient.Default.CreateFeatureUsage("resize").Submit();
    let ToDecimal(value:int, percentage:int) = (value * percentage) / 100
    image.Mutate(fun x -> 
        x.Resize(
            ToDecimal(image.Width, percentage), 
            ToDecimal(image.Height, percentage)) 
        |> ignore)

let Save (file:string, image:Image, outputDir:string, format:string) =
    let ext = FileInfo(file).Extension
    let outputFile = GetOutputFile(file, outputDir)
    match format with 
    | "jpg" -> image.Save(outputFile.Replace(ext, ".jpg"), Formats.Jpeg.JpegEncoder())
    | "png" -> image.Save(outputFile.Replace(ext, ".png"), Formats.Png.PngEncoder())
    | "bmp" -> image.Save(outputFile.Replace(ext, ".bmp"), Formats.Bmp.BmpEncoder())
    | "gif" -> image.Save(outputFile.Replace(ext, ".gif"), Formats.Gif.GifEncoder())
    | _ -> image.Save(outputFile)

let Process (file:string, outputDir:string, effect:MutateEffect, percentage:int, format:string) =
    let sw = Stopwatch.StartNew()
    use image = Image.Load(file)
    if not (percentage = 100) then Resize(percentage, image)
    image.Mutate(fun x ->
        match effect with
        | MutateEffect.Grayscale -> x.Grayscale() |> ignore
        | MutateEffect.Blackwhite -> x.BlackWhite() |> ignore
        | MutateEffect.Lomograph -> x.Lomograph() |> ignore
        | MutateEffect.Kodachrome -> x.Kodachrome() |> ignore
        | MutateEffect.Oilpaint -> x.OilPaint() |> ignore
        | MutateEffect.Boxblur -> x.BoxBlur() |> ignore
        | MutateEffect.Gaussianblur -> x.GaussianBlur() |> ignore
        | MutateEffect.Gaussiansharpen -> x.GaussianSharpen() |> ignore
        | MutateEffect.Glow -> x.Glow() |> ignore
        | MutateEffect.Invert -> x.Invert() |> ignore
        | MutateEffect.Pixelate -> x.Pixelate() |> ignore
        | MutateEffect.Polaroid -> x.Polaroid() |> ignore
        | MutateEffect.Sepia -> x.Sepia() |> ignore
        | MutateEffect.Vignette -> x.Vignette() |> ignore
        | _ -> ())

    if (effect <> MutateEffect.All && effect <> MutateEffect.None) 
    then Save(file, image, outputDir, format)
    
    let e = effect.ToString()
    printfn "%O - (%s) %s" sw.Elapsed e file

let ApplyAllEffects (file:string, opt:Options, popt:ParallelOptions) =
    Enum.GetValues(typeof<MutateEffect>) :?> (MutateEffect[])
    |> fun effects ->
        Parallel.ForEach(
            effects, 
            popt,
            fun effect ->
                Process(
                    file,
                    Path.Combine(opt.OutputDir, effect.ToString()), 
                    effect,
                    opt.Resize,
                    opt.Format))
    |> ignore

let Apply (file:string, opt:Options, popt:ParallelOptions, effect:MutateEffect) =
    ExceptionlessClient.Default.CreateFeatureUsage(opt.Effect).Submit()
    match effect with
    | MutateEffect.All -> ApplyAllEffects(file, opt, popt)
    | _ -> Process(file, opt.OutputDir, effect, opt.Resize, opt.Format)