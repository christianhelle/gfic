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
USAGE: gfic [--help] [--effect <name>] [--format <format>] [--input <path>] [--output <path>] [--threads <number>] [--resize <percentage>]

OPTIONS:

    --effect, -e <name>   Specify the image processing effect. Available effects are grayscale, blackwhite, lomograph, kodachrome,
                          oilpaint, boxblur, gaussianblur, gaussiansharpen, glow, invert, pixelate, polaroid, sepia, vignette, all
    --format, -f <format> File format (jpg, png, bmp, gif)
    --input, -i <path>    Specify a folder for source images
    --output, -o <path>   Specify the output folder.
    --threads, -m <number>
                          Specify the maximum degree of parallelism. Default is 1
    --resize, -r <percentage>
                          Resize the image by percentage
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


```
gfic --input .\ --output .\output --effect gaussiansharpen
```

![After](https://github.com/christianhelle/gfic/blob/master/images/gaussiansharpen/guitar1.jpg)


```
gfic --input .\ --output .\output --effect sepia
```

![After](https://github.com/christianhelle/gfic/blob/master/images/sepia/guitar1.jpg)


```
gfic --input .\ --output .\output --effect vignette
```

![After](https://github.com/christianhelle/gfic/blob/master/images/vignette/guitar1.jpg)


```
gfic --input .\ --output .\output --effect polaroid
```

![After](https://github.com/christianhelle/gfic/blob/master/images/polaroid/guitar1.jpg)


It's also possible to apply all supported effects by specifying **--effect all**

```
gfic --input .\input\ --output .\output --effect all

00:00:00.1495793 - (grayscale) .\images\guitar1.jpg
00:00:00.0688985 - (blackwhite) .\images\guitar1.jpg
00:00:00.0880889 - (lomograph) .\images\guitar1.jpg
00:00:00.0662329 - (kodachrome) .\images\guitar1.jpg
00:00:00.1943901 - (oilpaint) .\images\guitar1.jpg
00:00:00.1257821 - (boxblur) .\images\guitar1.jpg
00:00:00.1368524 - (gaussianblur) .\images\guitar1.jpg
00:00:00.0871363 - (gaussiansharpen) .\images\guitar1.jpg
00:00:00.0497155 - (glow) .\images\guitar1.jpg
00:00:00.0476376 - (invert) .\images\guitar1.jpg
00:00:00.0564792 - (pixelate) .\images\guitar1.jpg
00:00:00.0675103 - (polaroid) .\images\guitar1.jpg
00:00:00.0488536 - (sepia) .\images\guitar1.jpg
00:00:00.0552082 - (vignette) .\images\guitar1.jpg
00:00:00.0540388 - (grayscale) .\images\guitar2.jpg
00:00:00.0526321 - (blackwhite) .\images\guitar2.jpg
00:00:00.0533533 - (lomograph) .\images\guitar2.jpg
00:00:00.0527740 - (kodachrome) .\images\guitar2.jpg
00:00:00.1286677 - (oilpaint) .\images\guitar2.jpg
00:00:00.0709969 - (boxblur) .\images\guitar2.jpg
00:00:00.0743339 - (gaussianblur) .\images\guitar2.jpg
00:00:00.0773348 - (gaussiansharpen) .\images\guitar2.jpg
00:00:00.0597320 - (glow) .\images\guitar2.jpg
00:00:00.0287875 - (invert) .\images\guitar2.jpg
00:00:00.0150577 - (pixelate) .\images\guitar2.jpg
00:00:00.0156321 - (polaroid) .\images\guitar2.jpg
00:00:00.0128757 - (sepia) .\images\guitar2.jpg
00:00:00.0203237 - (vignette) .\images\guitar2.jpg

Total time: 00:00:01.9884109

```

This will create sub folders under the specified output folder that describes the applied effect

```
ls .\output\

    Directory: C:\test\images\output

Mode                LastWriteTime         Length Name
----                -------------         ------ ----
d-----        5/19/2019   2:19 PM                blackwhite
d-----        5/19/2019   2:55 PM                boxblur
d-----        5/19/2019   2:55 PM                gaussianblur
d-----        5/19/2019   2:55 PM                gaussiansharpen
d-----        5/19/2019   2:55 PM                glow
d-----        5/19/2019   2:19 PM                grayscale
d-----        5/19/2019   2:55 PM                invert
d-----        5/19/2019   2:19 PM                kodachrome
d-----        5/19/2019   2:19 PM                lomograph
d-----        5/19/2019   2:19 PM                oilpaint
d-----        5/19/2019   2:55 PM                pixelate
d-----        5/19/2019   2:55 PM                polaroid
d-----        5/19/2019   2:55 PM                sepia
d-----        5/19/2019   2:55 PM                vignette
```


### Future
I will add a bunch of CLI arguments for batch image processing operations like: brigten, darken, and the ability to combine multiple images into a collage and apply effects to it

#
For tips and tricks on software development, check out [my blog](https://christian-helle.blogspot.com)

If you find this useful and feel a bit generous then feel free to [buy me a coffee](https://www.buymeacoffee.com/christianhelle) :)
