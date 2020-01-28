# AdventOfCodeBase
A basic C# program for Advent of Code, retrieving puzzle inputs on the go and creating instances of solutions as they are created. It runs on .NET Core 3.0 and can be built easily in Visual Studio Code or Visual Studio. 

## Requirements
* .NET Core 3.0

## Features
* Fetches puzzle input from adventofcode.com and stores it locally
* Dynamically outputs current date's puzzle solution, depending on configuration
* Includes various useful utilities for typical puzzle problems
* Easy and intuitive configuration with config.json

## Usage
### Configure
Create config.json in the project root and add to it the following key/value pairs.
```
{
  // Valid cookie content required to retrieve puzzle input from adventofcode.com with ASolution.LoadInput(). 
  "cookie": "session=c0nt3nt", 
  
  // int value representing year to pull puzzles from.
  //  Used for folder navigation and puzzle input retrieval
  "year": 2019,

  // int[] representing days of which to collect solutions. 
  //  [0] resolves to "all" 
  //  [] resolves to current date, if applicable
  "days": [0] 
}
```
If you run the program without adding a config.json file, one will be created for you with an empty string as the value for "cookie". The program will not be able to fetch puzzle inputs from the web before a valid cookie is added to the configuration. 

### Build & Run
```
> dotnet build
> dotnet run
```

## Contributing 
Sure! Fork the project, make your changes, and submit a pull request. 
