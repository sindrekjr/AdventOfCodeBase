# AdventOfCodeBase
A basic C# program for Advent of Code, retrieving puzzle inputs on the go and creating instances of solutions as they are created. It runs on .NET Core 3.0 and can be built easily in Visual Studio Code or Visual Studio. 

#### Build & Run
```
> dotnet build
> dotnet run
```

#### Configuration (Config.cs => config.json)
Program.cs assumes that the root folder contains a config.json file and passes this path to Config.cs where it should be deserialized as a Config object. The .json should look more or less like the following. 
```
{
  "cookie": "session=c0nt3nt", 
  // Valid cookie content required to retrieve puzzle input from adventofcode.com with ASolution.LoadInput(). 
  
  "year": 2019,
  // Simple int value representing year to pull puzzles from. Used for folder navigation and puzzle input retrieval. 

  "days": [0] 
  // int[] representing days of which to collect solutions. 0 resolves to "all". 
}
```
If no config.json exists, one is created automatically with an empty string as the cookie value. 
