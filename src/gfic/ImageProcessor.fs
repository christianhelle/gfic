module ImageProcessor

open System.Diagnostics
open System.IO
open SixLabors.ImageSharp
open SixLabors.ImageSharp.Processing

open CLIArguments
open System.Threading.Tasks

let GetOutputFile (input, output) =
    Path.Combine(Directory.GetCurrentDirectory(), output)
    |> Directory.CreateDirectory
    |> fun dir -> Path.Combine(dir.FullName, FileInfo(input).Name)

let Resize (percentage:int, image:Image<PixelFormats.Rgba32>) =
    let toDecimal(value:int, percentage:int) = (value * percentage) / 100
    image.Mutate(fun x -> 
        x.Resize(
            toDecimal(image.Width, percentage), 
            toDecimal(image.Height, percentage)) 
        |> ignore)

let Process (file:string, outputDir:string, effect:string, percentage:int, format:string) =
    let sw = Stopwatch.StartNew()
    use image = Image.Load(file)    
    if not (percentage = 100) then Resize(percentage, image)
    image.Mutate(fun x ->
        match effect with
        | "grayscale" -> x.Grayscale() |> ignore
        | "blackwhite" -> x.BlackWhite() |> ignore
        | "lomograph" -> x.Lomograph() |> ignore
        | "kodachrome" -> x.Kodachrome() |> ignore
        | "oilpaint" -> x.OilPaint() |> ignore
        | "boxblur" -> x.BoxBlur() |> ignore
        | "gaussianblur" -> x.GaussianBlur() |> ignore
        | "gaussiansharpen" -> x.GaussianSharpen() |> ignore
        | "glow" -> x.Glow() |> ignore
        | "invert" -> x.Invert() |> ignore
        | "pixelate" -> x.Pixelate() |> ignore
        | "polaroid" -> x.Polaroid() |> ignore
        | "sepia" -> x.Sepia() |> ignore
        | "vignette" -> x.Vignette() |> ignore
        | _ -> printfn "No effect applied")

    let ext = FileInfo(file).Extension
    let outputFile = GetOutputFile(file, outputDir)
    match format with 
    | "jpg" -> image.Save(outputFile.Replace(ext, ".jpg"), Formats.Jpeg.JpegEncoder())
    | "png" -> image.Save(outputFile.Replace(ext, ".png"), Formats.Png.PngEncoder())
    | "bmp" -> image.Save(outputFile.Replace(ext, ".bmp"), Formats.Bmp.BmpEncoder())
    | "gif" -> image.Save(outputFile.Replace(ext, ".gif"), Formats.Gif.GifEncoder())
    | _ -> image.Save(outputFile)
    
    printfn "%O - (%s) %s" sw.Elapsed effect file

let ApplyAllEffects (file:string, opt:Options, popt:ParallelOptions) =
    let effects = [
        "grayscale"; 
        "blackwhite"; 
        "lomograph"; 
        "kodachrome"; 
        "oilpaint";
        "boxblur"; 
        "gaussianblur"; 
        "gaussiansharpen"; 
        "glow";
        "invert";
        "pixelate";
        "polaroid";
        "sepia";
        "vignette"
    ]
    Parallel.ForEach(effects, popt, 
        fun e -> Process(file, Path.Combine(opt.OutputDir, e), e, opt.Resize, opt.Format)) 
    |> ignore

let Apply (file:string, opt:Options, popt:ParallelOptions) =
    match opt.Effect with
    | "all" -> ApplyAllEffects(file, opt, popt)
    | _ -> Process(file, opt.OutputDir, opt.Effect, opt.Resize, opt.Format)
