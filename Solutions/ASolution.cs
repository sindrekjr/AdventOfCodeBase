using System;
using System.IO; 
using System.Net;

namespace AdventOfCode.Solutions {

    abstract class ASolution {

        // Private lazy fields accessed by public properties
        Lazy<string> _input, _part1, _part2;
        
        // Public fields
        public int Day;
        public int Year; 
        public string Title;

        // Public properties 
        public string Input => _input.Value; 
        public string Part1 => string.IsNullOrEmpty(_part1.Value) ? "" : _part1.Value;
        public string Part2 => string.IsNullOrEmpty(_part2.Value) ? "" : _part2.Value;

        // Constructor
        private protected ASolution(int day, int year, string title) {
            Day = day;
            Year = year; 
            Title = title;
            _input = new Lazy<string>(LoadInput());
            _part1 = new Lazy<string>(SolvePartOne());
            _part2 = new Lazy<string>(SolvePartTwo());
        }

        public void Solve(int part = 0) {
            bool doOutput = false; 
            string output = $"--- Day {Day}: {Title} --- \n";

            if(part != 2) {
                if(Part1 != "") {
                    output += $"Part 1: {Part1}\n"; 
                    doOutput= true; 
                } else {
                    output += "Part 1: Unsolved\n"; 
                    if(part == 1) doOutput= true; 
                }
            }
            if(part != 1) {
                if(Part2 != "") {
                    output += $"Part 2: {Part2}\n";
                    doOutput= true; 
                } else {
                    output += "Part 2: Unsolved\n";
                    if(part == 2) doOutput= true; 
                }
            }

            if(doOutput) Console.WriteLine(output); 
        }

        // Method for retrieving the puzzle input for the given day
        string LoadInput() {
            string INPUT_FILEPATH = $"./Solutions/Year{Year}/Day{Day.ToString("D2")}/input";
            string INPUT_URL = $"https://adventofcode.com/{Year}/day/{Day}/input";
            string input = ""; 

            if(File.Exists(INPUT_FILEPATH)) {
                input = File.ReadAllText(INPUT_FILEPATH);
            } else {
                try {
                    using(WebClient client = new WebClient()) {
                        client.Headers.Add(HttpRequestHeader.Cookie, Program.Config.Cookie);
                        input = client.DownloadString(INPUT_URL).Trim();
                        File.WriteAllText(INPUT_FILEPATH, input);
                    }
                } catch(WebException e) {
                    HttpStatusCode statusCode = ((HttpWebResponse) e.Response).StatusCode;
                    if(statusCode == HttpStatusCode.BadRequest) {
                        Console.WriteLine($"Day {Day}: Error code 400 when attempting to retrieve puzzle input through the web client. Your session cookie is most likely not recognized.");
                    } else if(statusCode == HttpStatusCode.NotFound) {
                        Console.WriteLine($"Day {Day}: Error code 404 when attempting to retrieve puzzle input through the web client. The puzzle is most likely not available yet.");
                    } else {
                        Console.WriteLine(e.Status);
                    }
                }
            }
            return input; 
        }

        // Abstract methods that must be defined in each instantiated subclass of ASolution
        protected abstract string SolvePartOne();
        protected abstract string SolvePartTwo();
    }
}
