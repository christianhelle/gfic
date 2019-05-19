[![Build Status](https://christianhelle.visualstudio.com/gfic/_apis/build/status/CI%20Build?branchName=master)](https://christianhelle.visualstudio.com/gfic/_build/latest?definitionId=20&branchName=master)

# gfic
A .NET Core command line image processor that I started for no other reason than to learn F#. 

The tool will scan the specified input folder for image files, resize the image by percentage if specified, apply the specified effect, and then save them to the specified output folder. The tool has multi-threading support where you can specify the maximum degree of parallelism but will by default only run on a single thread

#### Supported effects
- grayscale
- blackwhite
- lomograph
- kodachrome
- oilpaint
- box blur 
- gaussian blur 
- gaussian sharpen 
- glow
- invert
- pixelate
- polaroid
- sepia
- vignette

### Installation
The tool can be installed as a .NET Core global tool that you can call from the shell / command line
```
dotnet tool install --global gfic
```
or by following the instructions [here](https://www.nuget.org/packages/gfic) to install a specific version of tool

### Usage
```
USAGE: gfic [--help] [--effect <path>] [--input <path>] [--output <path>] [--threads <path>] [--resize <path>]

OPTIONS:

    --effect, -e <path>   Specify the image processing effect. Available effects are grayscale, blackwhite, lomograph, kodachrome, oilpaint, boxblur, gaussianblur,
                          gaussiansharpen, glow, invert, pixelate, polaroid, sepia, vignette, all
    --input, -i <path>    Specify a folder for source images
    --output, -o <path>   Specify the output folder.
    --threads, -m <path>  Specify the maximum degree of parallelism. Default is 1
    --resize, -r <path>   Resize the image by percentage
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


It's also possible to apply all supported effects by specifying **--effect all**

```
gfic --input .\input\ --output .\output --effect all

00:00:00.1634858 - (grayscale) .\input\guitar1.jpg
00:00:00.0664107 - (blackwhite) .\input\guitar1.jpg
00:00:00.0845469 - (lomograph) .\input\guitar1.jpg
00:00:00.0664452 - (kodachrome) .\input\guitar1.jpg
00:00:00.1909609 - (oilpaint) .\input\guitar1.jpg
00:00:00.0749696 - (grayscale) .\input\guitar2.jpg
00:00:00.0741036 - (blackwhite) .\input\guitar2.jpg
00:00:00.0815556 - (lomograph) .\input\guitar2.jpg
00:00:00.0758306 - (kodachrome) .\input\guitar2.jpg
00:00:00.0871689 - (oilpaint) .\input\guitar2.jpg

Total time: 00:00:00.9850806

```

This will create sub folders under the specified output folder that describes the applied effect

```
ls .\output\

    Directory: C:\test\images\output

Mode                LastWriteTime         Length Name
----                -------------         ------ ----
d-----        5/19/2019   1:10 AM                blackwhite
d-----        5/19/2019   1:10 AM                grayscale
d-----        5/19/2019   1:10 AM                kodachrome
d-----        5/19/2019   1:10 AM                lomograph
d-----        5/19/2019   1:10 AM                oilpaint
```


### Future
I will add a bunch of CLI arguments for batch image processing operations like: resize, brigten, darken, and the ability to combine multiple images into a collage and apply effects to it
