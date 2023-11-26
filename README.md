# AdventOfCodeBase
Template project for solving Advent of Code in C#, running on [.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).

- [Features](#features)
- [Usage](#usage)
  - [Creating a repository](#creating-a-repository)
  - [Configuring](#configuring)
  - [Running the project](#running-the-project)
  - [Example solution](#example-solution)
- [Notes](#notes)
  - [Generating Solution Files](#generating-solution-files)
  - [Automatic Debugger Break On Exception](#automatic-debugger-break-on-exception)
- [Background](#background)
- [Contributing](#contributing)
- [License](#license)

## Features
* Simple configuration with `config.json`.
* Fetches puzzle input from adventofcode.com and stores it locally.
* Supports easily switching between debug-input and real input.
* Naive benchmarking, showing as millisecond count.

## Usage

### Creating a repository
To get started using this template, click the green "Use this template" button above (or [this link](https://github.com/sindrekjr/AdventOfCodeBase/generate)) to create a new repository of your own from it.

If any solution files that you need are not already included, see **[Generating Previous Year's Solution Files](#generating-previous-years-solution-files)**.

### Configuring
Create a new file named `config.json` at the root of the project.
```json
{
  "cookie": "c0nt3nt",
  "year": 2020,
  "days": [0] 
}
```

If you run the program without adding this file, one will be created for you without a cookie field. The program will not be able to fetch puzzle inputs from adventofcode.com before a valid cookie is added to the configuration. 

#### `cookie` - Note that `c0nt3nt` must be replaced with a valid cookie value that your browser stores when logging in at adventofcode.com. Instructions on locating your session cookie can be found here: https://github.com/wimglenn/advent-of-code-wim/issues/1

#### `year` - Specifies which year you wish to output solutions for when running the project. Defaults to the current year if left unspecified.

#### `days` - Specifies which days you wish to output solutions for when running the project. Defaults to current day if left unspecified and an event is actively running, otherwise defaults to `0`.

The field supports list comprehension syntax and strings, meaning the following notations are valid.
* `"1..4, 10"` - runs day 1, 2, 3, 4, and 10.
* `[1, 3, "5..9", 15]` - runs day 1, 3, 5, 6, 7, 8, 9, and 15.
* `0` - runs all days

### Running the project
Write your advent of code solutions in the appropriate solution classes, `AdventOfCode.Solutions/Year<YYYY>/Day<DD>/Solution.cs`.

Then run the project. From the command line you can use `dotnet run`, and optionally specify a day inline. For example, to run your solution for day 21:
```
dotnet run 21
```

### Example solution
```csharp
class Solution : SolutionBase
{
    // the constructor calls its base class with (day, year, name)
    public Solution() : base(02, 2021, "The Big Bad Sample Santa")
    {
        // you can use the constructor for preparations shared by both part one and two if you wish
    }
    
    protected override string SolvePartOne()
    {
        var lines = Input.SplitByNewline();
        return lines.First();
        // this would return the first line of the input as this part's solution
    }
    
    protected override string SolvePartTwo()
    {
        Debug = true;
        // we choose to use the debug input for this part
        // note that the debug input cannot be fetched automatically; it has to be copied into the solution folder manually
        return "";
    }
}
```

## Notes
### Generating Solution Files

Solution files can be automatically generated via GNU Make or PowerShell, and both methods use the included file `solution.template`, which is customisable. Both options default to current YEAR and all DAYS (1-25).

#### GNU Make

```bash
$ make solution-files [,YEAR] [,DAYS]
```

Requires GNU Make v4 or later.

#### PowerShell

```pwsh
> GenerateSolutionFiles.ps1 [-Year <Int>]
```

Requires PowerShell v3 or later due to the way `$PSScriptRoot` behaves. If you have Windows 8+ you should be set. Upgrades for previous versions, and installs for macOS and Linux can be found in [Microsoft's Powershell Documentation](https://docs.microsoft.com/en-us/powershell/scripting/install/installing-powershell?view=powershell-7.1)

### Automatic Debugger Break On Exception
When running your Solutions with a Debugger attached e.g. [VSCode](https://code.visualstudio.com/docs/editor/debugging) or [Visual Studio](https://docs.microsoft.com/en-us/visualstudio/debugger/quickstart-debug-with-managed?view=vs-2019) the BaseSolution base class will try to pause/break the debugger when there is an uncaught Exception thrown by your solution part. This allows for inspection with the debugger without having to specifically set-up additional exception handling within the debugger or your solution.

## Background
I intended to use Advent of Code 2019 to learn C#, and found that I wanted to try to put together a small solutions framework of my own. In that way this template came about as an introductory project to C# and .NET Core.

## Contributing 
If you wish to contribute to this project, simply fork and the clone the repository, make your changes, and submit a pull request. Contributions are quite welcome!

Please adhere to the [conventional commit format](https://www.conventionalcommits.org/en/v1.0.0/).

## License
[MIT](https://github.com/sindrekjr/AdventOfCodeBase/blob/master/LICENSE.md)
