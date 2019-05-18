[![Build Status](https://christianhelle.visualstudio.com/gfic/_apis/build/status/CI%20Build?branchName=master)](https://christianhelle.visualstudio.com/gfic/_build/latest?definitionId=20&branchName=master)

# gfic
A .NET Core command line image processor that I started for no other reason than to learn F#. 
The tool will scan the present working directory for image files and create monochrome copies of them in a sub folder called **grayscale**

### Installation
The tool can be installed as a .NET Core global tool that you can call from the shell / command line
```
dotnet tool install --global gfic
```

### Future
I will add a bunch of CLI arguments for batch image processing operations like: resize, brigten, darken, and the ability to combine multiple images into a collage and apply effects to it
