# AdventOfCodeBase
Template project for solving Advent of Code in C#, running on [.NET 5.0](https://dotnet.microsoft.com/download/dotnet/5.0).

- [Features](#features)
- [Getting started](#getting-started)
- [Usage](#usage)
  - [Configuration](#configuration)
  - [Running the project](#running-the-project)
  - [Example projects](#example-projects)
- [Notes](#notes)
  - [Generating Previous Year's Solution Files](#generating-previous-years-solution-files)
  - [Using a Solution's Constructor](#using-a-solutions-constructor)
  - [Using DebugInput](#using-debuginput)
  - [Using .NET Core](#using-net-core)
  - [Automatic Debugger Break On Exception](#automatic-debugger-break-on-exception)
- [Background](#background)
- [Contributing](#contributing)
- [License](#license)

## Features
* Simple configuration with `config.json`.
* Fetches puzzle input from adventofcode.com and stores it locally.
* Supports easily switching between debug-input and real input.
* Naive benchmarking, showing as millisecond count.
* Includes various useful utilities for typical puzzle problems.

## Getting started
If you haven't already, use the button shown below (or [this link](https://github.com/sindrekjr/AdventOfCodeBase/generate)) to create a new repository of your own from this template.

<kbd style>![use-this-template](https://user-images.githubusercontent.com/23259585/95107477-3e522300-073a-11eb-8c80-c0cd4e1b5c11.png)</kbd>

Feel free to make any modifications you want. However, you probably do not want to remove any files outside of `AdventOfCode/Solutions/` unless you know what you're doing.

If any solution files that you need are not already included, see **[Generating Previous Year's Solution Files](#generating-previous-years-solution-files)**.

## Usage
### Configuration
Create `config.json` with the following key/value pairs. If you run the program without adding a `config.json` file, one will be created for you without a cookie field. The program will not be able to fetch puzzle inputs from the web before a valid cookie is added to the configuration. 
```json
{
  "cookie": "session=c0nt3nt",
  "year": 2020,
  "days": [0] 
}
```

`cookie` - Note that `c0nt3nt` must be replaced with a valid cookie value that your browser stores when logging in at adventofcode.com. Instructions on locating your session cookie can be found here: https://github.com/wimglenn/advent-of-code-wim/issues/1

`year` - Specifies which year you wish to output solutions for when running the project. Defaults to the current year if left unspecified.

`days` - Specifies which days you wish to output solutions for when running the project. Defaults to current day if left unspecified and an event is actively running, otherwise defaults to `0`.

The field supports list comprehension syntax and strings, meaning the following notations are valid.
* `"1..4, 10"` - runs day 1, 2, 3, 4, and 10.
* `[1, 3, "5..9", 15]` - runs day 1, 3, 5, 6, 7, 8, 9, and 15.
* `0` - runs all days

### Running the project
Write your code solutions to advent of code within the appropriate day classes in the Solutions folder, and run the project. From the command line you may do as follows.
```
> cd AdventOfCode
> dotnet build
> dotnet run
```
Using `dotnet run` from the root of the repository will also work as long as you specify which project to run by adding `-p AdventOfCode`. Note that your `config.json` must be stored in the location from where you run your project.

### Example projects
* [sindrekjr/AdventOfCode](https://github.com/sindrekjr/AdventOfCode)

## Notes
### Generating Previous Year's Solution Files
Use the included PowerShell script `AdventOfCode/UserScripts/GenerateSolutionFiles.ps1` to generate a year's solution files following the same layout as those already included.

Usage: `GenerateSolutionFiles.ps1 [-Year <Int>]`

If no value is provided it will generate files for the current year. The script will avoid overwriting existing files.  

Requires PowerShell v3 or later due to the way `$PSScriptRoot` behaves. If you have Windows 8+ you should be set. Upgrades for previous versions, and installs for macOS and Linux can be found in [Microsoft's Powershell Documentation](https://docs.microsoft.com/en-us/powershell/scripting/install/installing-powershell?view=powershell-7.1)

### Using a Solution's Constructor
* Code may be written in the solution constructor if it will be beneficial to both parts of the problem (such as parsing the data). Example:
```CSharp
public Day07() : base(07, 2015, "")
{
    string[] lines = Input.SplitByNewLine();
    foreach (string line in Lines)
    {
        //Parse out input here
    }
}

protected override string SolvePartOne()
{
    //Manipulations specific to Part 1 here
    return result;
}

protected override string SolvePartTwo()
{
    //Manipulations specific to Part 2 here
    return result;
}
```
* The variable `Input` will contain your input as a long raw string.
* The extension method `SplitByNewLine()` will do exactly that, example: `string[] lines = Input.SplitByNewLine()` will split your input into lines for enumeration.

### Using DebugInput
* Real inputs are stored in a file named `input`; for debug inputs, simply store them at same level in a file `debug`.
* Both input files are loaded at runtime, but you must toggle the DebugInput on to use it, as shown below.
```diff
-   public Day07() : base(07, 2015, "")
+   public Day07() : base(07, 2015, "", true)
        {
    
        }
```
* Setting `Debug = true` within the constructor body also works fine.

### Using .NET Core
Simply swap out the target framework in `AdventOfCode.csproj`.
```diff
-    <TargetFramework>net5.0</TargetFramework>
+    <TargetFramework>netcoreapp3.1</TargetFramework>
```

### Automatic Debugger Break On Exception
When running your Solutions with a Debugger attached e.g. [VSCode](https://code.visualstudio.com/docs/editor/debugging) or [Visual Studio](https://docs.microsoft.com/en-us/visualstudio/debugger/quickstart-debug-with-managed?view=vs-2019) the ASolution base class will try to pause/break the debugger when there is an uncaught Exception thrown by your solution part. This allows for inspection with the debugger without having to specifically set-up additional exception handling within the debugger or your solution.

## Background
I intended to use Advent of Code 2019 to learn C#, and found that I wanted to try to put together a small solutions framework of my own. In that way this template came about as an introductory project to C# and .NET Core.

## Contributing 
Sure! Check out the [contributing guide](https://github.com/sindrekjr/AdventOfCodeBase/blob/master/CONTRIBUTING.md).

## License
[MIT](https://github.com/sindrekjr/AdventOfCodeBase/blob/master/LICENSE.md)
