[![Build Status](https://christianhelle.visualstudio.com/gfic/_apis/build/status/CI%20Build?branchName=master)](https://christianhelle.visualstudio.com/gfic/_build/latest?definitionId=20&branchName=master)

# gfic
A .NET Core command line image processor that I started for no other reason than to learn F#. 
The tool will scan the present working directory for image files and create monochrome copies of them in a sub folder called **grayscale**

### Installation
The tool can be installed as a .NET Core global tool that you can call from the shell / command line
```
dotnet tool install --global gfic
```

### Usage
```
USAGE: gfic [--help] [--effect <path>] [--input <path>] [--output <path>] [--threads <path>]

OPTIONS:

    --effect, -e <path>   Specify the image processing effect. Available effects are grayscale, blackwhite, lomograph, kodachrome, oilpaint, all
    --input, -i <path>    Specify a folder for source images
    --output, -o <path>   Specify the output folder.
    --threads, -m <path>  Specify the maximum degree of parallelism. Default is 1
    --help                display this list of options.
```

### Examples
Here's an example of how an original image might look like

![Before](https://github.com/christianhelle/gfic/blob/master/images/original/guitar1.jpg)

#### Then after applying some effects

```
gfic --input .\ --output .\output --effect grayscale
```

![After](https://github.com/christianhelle/gfic/blob/master/images/grayscale/guitar1.jpg)


```
gfic --input .\ --output .\output --effect lomograph
```

![After](https://github.com/christianhelle/gfic/blob/master/images/lomograph/guitar1.jpg)


```
gfic --input .\ --output .\output --effect oilpaint
```

![After](https://github.com/christianhelle/gfic/blob/master/images/oilpaint/guitar1.jpg)


### Future
I will add a bunch of CLI arguments for batch image processing operations like: resize, brigten, darken, and the ability to combine multiple images into a collage and apply effects to it
