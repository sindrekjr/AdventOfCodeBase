# AdventOfCodeBase
A basic C# program for Advent of Code, retrieving puzzle inputs on the go and creating instances of solutions as they are created. It runs on .NET Core 3.0 and can be built easily in Visual Studio Code or Visual Studio. 

## Requirements
* .NET Core 3.1

## Features
* Fetches puzzle input from adventofcode.com and stores it locally.
* Dynamically outputs current date's puzzle solution, depending on configuration.
* Includes various useful utilities for typical puzzle problems.
* Simple configuration with config.json.

## Usage
### Create Project
Create a new project of your own from this template repository, through the button shown below.
![usetemplate](https://user-images.githubusercontent.com/23259585/95107477-3e522300-073a-11eb-8c80-c0cd4e1b5c11.png)

### Cleanup
Make any file additions/modifications you want, such as removing solution files for previous years if you've no interest in completing those. You probably do not want to remove any files outside of `AdventOfCode/Solutions/` unless you know what you're doing.

### Generating Previous Year's Solution Files
Use the included PowerShell script `AdventOfCode/UserScripts/GenerateSolutionFiles.ps1` to generate a year's solution files following the same layout as those already included.

Usage: `GenerateSolutionFiles.ps1 [-Year <Int>]`

If no value is provided it will generate files for the current year. The script will avoid overwriting existing files.  

Requires PowerShell v3 or later due to the way `$PSScriptRoot` behaves. If you have Windows 8+ you should be set. Upgrades for previous versions, and installs for macOS and Linux can be found in [Microsoft's Powershell Documentation](https://docs.microsoft.com/en-us/powershell/scripting/install/installing-powershell?view=powershell-7.1)

### Configure
Create `config.json` with the following key/value pairs. If you run the program without adding a `config.json` file, one will be created for you without a cookie field. The program will not be able to fetch puzzle inputs from the web before a valid cookie is added to the configuration. 
```json
{
  "cookie": "session=c0nt3nt",
  "year": 2020,
  "days": [0] 
}
```
#### `cookie`
Note that `c0nt3nt` must be replaced with a valid cookie value that your browser stores when logging in at adventofcode.com.

Instructions on locating your session cookie can be found here: https://github.com/wimglenn/advent-of-code-wim/issues/1

#### `year`
Specifies which year you wish to output solutions for when running the project. If left unspecified will default to the current year.

#### `days`
Specifies which days you wish to output solutions for when running the project. The field supports list comprehension syntax and strings, meaning the following notations are valid.
* `"1..4, 10"` - runs day 1, 2, 3, 4, and 10.
* `[1, 3, "5..9", 15]` - runs day 1, 3, 5, 6, 7, 8, 9, and 15.
* `0` - runs all days

Will default to current day if left unspecified and an event is actively running. Otherwise will default to `0`

### Code
Write your code solutions to advent of code within the appropriate day classes in the Solutions folder, and run the project. From the command line you may do as follows.
```
> cd AdventOfCode
> dotnet build
> dotnet run
```
Using `dotnet run` from the root of the project will also work as long as you specify which project to run by adding `-p AdventOfCode`. Note that `config.json` must be stored in the folder from where you run the project.

#### Notes
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
* If stuck you can set the `DebugInput` variable at the top of the constructor, and it will overwrite `Input` variable, so you won't need to change all your references. 
* The extension method `SplitByNewLine()` will do exactly that, example: `string[] lines = Input.SplitByNewLine()` will split your input into lines for enumeration.

## Contributing 
Sure! Fork the project, make your changes, and submit a pull request. 
