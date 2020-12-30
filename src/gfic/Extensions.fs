namespace Extensions

open System

[<AutoOpen>]
module FsExtensions =
    type String with
        member this.ToEnum<'a when 'a :> Enum and 'a : struct and 'a : (new: unit -> 'a)> (?ignoreCase) =
            let ok, v = Enum.TryParse<'a>(this, defaultArg ignoreCase true)
            if ok then Some v else None