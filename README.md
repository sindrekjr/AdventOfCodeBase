# AdventOfCodeBase
A basic C# program for Advent of Code, retrieving puzzle inputs on the go and creating instances of solutions as they are created. It runs on .NET Core 3.0 and can be built easily in Visual Studio Code or Visual Studio. 

## Requirements
* .NET Core 3.1

## Features
* Fetches puzzle input from adventofcode.com and stores it locally.
* Dynamically outputs current date's puzzle solution, depending on configuration.
* Includes various useful utilities for typical puzzle problems.
* Easy and intuitive configuration with config.json.

## Usage
### Create Project
Create a new project of your own from this template repository, through the button shown below.
![usetemplate](https://user-images.githubusercontent.com/23259585/95107477-3e522300-073a-11eb-8c80-c0cd4e1b5c11.png)

### Cleanup
Make any file additions/modifications you want, such as removing solution files for previous years if you've no interest in completing those. You probably do not want to remove any files outside of `AdventOfCode/Solutions/` unless you know what you're doing.

### Configure
Create `config.json` in the project root and add to it the following key/value pairs.
```json
{
  "cookie": "session=c0nt3nt",
  "year": 2020,
  "days": [0] 
}
```
If you run the program without adding a `config.json` file, one will be created for you without a cookie field. The program will not be able to fetch puzzle inputs from the web before a valid cookie is added to the configuration.

### Code
Write your code solutions to advent of code within the appropriate day classes in the Solutions folder, and run the project as follows.
```
> dotnet build
> dotnet run -p AdventOfCode
```
If you move into the `AdventOfCode` folder, `dotnet run` will be sufficient for running your code.

## Contributing 
Sure! Fork the project, make your changes, and submit a pull request. 
