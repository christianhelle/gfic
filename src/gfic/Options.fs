module Options

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
        OutputDir = ".\output"
        Threads = 1
        Resize = 100
        NoLogging = false
    }