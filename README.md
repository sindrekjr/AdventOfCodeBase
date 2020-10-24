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

#### `year`
Specifies which year you wish to output solutions for when running the project.

#### `days`
Specifies which days you wish to output solutions for when running the project. The field supports list comprehension syntax and strings, meaning the following notations are valid.
* `"1..4, 10"` - runs day 1, 2, 3, 4, and 10.
* `[1, 3, "5..9", 15]` - runs day 1, 3, 5, 6, 7, 8, 9, and 15.
* `0` - runs all days


### Code
Write your code solutions to advent of code within the appropriate day classes in the Solutions folder, and run the project. From the command line you may do as follows.
```
> cd AdventOfCode
> dotnet build
> dotnet run
```
Using `dotnet run` from the root of the project will also work as long as you specify which project to run by adding `-p AdventOfCode`. Note that `config.json` must be stored in the folder from where you run the project.

## Contributing 
Sure! Fork the project, make your changes, and submit a pull request. 
