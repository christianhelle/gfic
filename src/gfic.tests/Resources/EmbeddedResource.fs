module EmbeddedResource

open System.IO
open System.Reflection

let GetStream name = 
    let ns = "gfic.tests.Resources."
    Assembly.GetExecutingAssembly().GetManifestResourceStream(ns + name)

let ExtractAll() =
    for name in ["guitar1.jpg";"guitar2.jpg"] do
        let path = Path.Combine(Directory.GetCurrentDirectory(), name)
        if not (File.Exists path) then do
            use fileStream = File.OpenWrite path
            use writer = new BinaryWriter(fileStream)
            use src = GetStream name
            use mem = new MemoryStream()
            src.CopyTo(mem)
            writer.Write(mem.ToArray())
            writer.Flush()