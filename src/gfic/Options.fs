module Options

open System.IO

type Options = {
    Effect : string
    Format : string
    InputDir : string
    OutputDir : string
    Threads : int
    Resize : int
    NoLogging : bool
}

module Options = 
    let Empty = {
        Effect = ""
        Format = ""
        InputDir = ""
        OutputDir = "." + Path.DirectorySeparatorChar.ToString() + "output"
        Threads = 1
        Resize = 100
        NoLogging = false
    }