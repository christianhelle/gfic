﻿module EmbeddedResource

open System.IO
open System.Reflection

let GetStream name = 
    let ns = "gfic.tests.Resources."
    Assembly.GetExecutingAssembly().GetManifestResourceStream(ns + name)

let ExtractAll =
    for name in ["guitar1.jpg";"guitar2.jpg"] do
        let path = Path.Combine(Directory.GetCurrentDirectory(), name)
        if not (File.Exists path) then do
            File.OpenWrite path
            |> fun fileStream -> new BinaryWriter(fileStream)
            |> fun writer -> 
                use src = GetStream name
                new MemoryStream() 
                    |> fun mem -> src.CopyTo(mem) |> mem.ToArray
                    |> writer.Write
                    |> writer.Dispose
                    |> ignore