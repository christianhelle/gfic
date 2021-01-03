module ImageResizer

open SixLabors.ImageSharp
open SixLabors.ImageSharp.Processing
open Exceptionless

let Resize (percentage:int, image:Image) =
    ExceptionlessClient.Default.CreateFeatureUsage("resize").Submit();
    let ToDecimal(value:int, percentage:int) = (value * percentage) / 100
    image.Mutate(fun x -> 
        x.Resize(
            ToDecimal(image.Width, percentage), 
            ToDecimal(image.Height, percentage)) 
        |> ignore)