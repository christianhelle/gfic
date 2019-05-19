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

let Process (file:string, outputDir:string, effect:string, percentage:int) =
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
        | _ -> printfn "Unknown effect")

    GetOutputFile(file, outputDir) |> image.Save
    printfn "%O - (%s) %s" sw.Elapsed effect file

let ApplyAllEffects (file:string, opt:Options, popt:ParallelOptions) =
    let effects = ["grayscale"; "blackwhite"; "lomograph"; "kodachrome"; "oilpaint"]
    Parallel.ForEach(effects, popt, 
        fun e -> Process(file, Path.Combine(opt.OutputDir, e), e, opt.Resize)) 
    |> ignore

let Apply (file:string, opt:Options, popt:ParallelOptions) =
    match opt.Effect with
    | "all" -> ApplyAllEffects(file, opt, popt)
    | _ -> Process(file, opt.OutputDir, opt.Effect, opt.Resize)
