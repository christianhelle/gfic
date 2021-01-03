module ImageConverter

open SixLabors.ImageSharp
open System.IO

let GetOutputFile (input, output) =
    Path.Combine(Directory.GetCurrentDirectory(), output)
    |> Directory.CreateDirectory
    |> fun dir -> Path.Combine(dir.FullName, FileInfo(input).Name)

let Save (file:string, image:Image, outputDir:string, format:string) =
    let ext = FileInfo(file).Extension
    let outputFile = GetOutputFile(file, outputDir)
    match format with 
    | "jpg" -> image.Save(outputFile.Replace(ext, ".jpg"), Formats.Jpeg.JpegEncoder())
    | "png" -> image.Save(outputFile.Replace(ext, ".png"), Formats.Png.PngEncoder())
    | "bmp" -> image.Save(outputFile.Replace(ext, ".bmp"), Formats.Bmp.BmpEncoder())
    | "gif" -> image.Save(outputFile.Replace(ext, ".gif"), Formats.Gif.GifEncoder())
    | _ -> image.Save(outputFile)