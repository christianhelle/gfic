module ImageConverter

open SixLabors.ImageSharp
open System.IO
open SixLabors.ImageSharp.Formats

let GetOutputFile (input, output) =
    Path.Combine(Directory.GetCurrentDirectory(), output)
    |> Directory.CreateDirectory
    |> fun dir -> Path.Combine(dir.FullName, FileInfo(input).Name)

let Save (file:string, image:Image, outputDir:string, format:string) =
    let ext = FileInfo(file).Extension
    let outputFile = GetOutputFile(file, outputDir)
    let lowerFormat = format.ToLowerInvariant()
    let Save (encoder:IImageEncoder) =
        image.Save(outputFile.Replace(ext, "." + lowerFormat), encoder)
    match lowerFormat with
    | "jpg" -> Save(Formats.Jpeg.JpegEncoder())
    | "png" -> Save(Formats.Png.PngEncoder())
    | "bmp" -> Save(Formats.Bmp.BmpEncoder())
    | "gif" -> Save(Formats.Gif.GifEncoder())
    | _ -> image.Save(outputFile)