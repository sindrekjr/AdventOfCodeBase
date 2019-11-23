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
        public string Part1 => string.IsNullOrEmpty(_part1.Value) ? "Unsolved" : _part1.Value;
        public string Part2 => string.IsNullOrEmpty(_part2.Value) ? "Unsolved" : _part2.Value;

        // Constructor
        private protected ASolution(int day, int year, string title) {
            Day = day;
            Year = year; 
            Title = title;
            _input = new Lazy<string>(LoadInput());
            _part1 = new Lazy<string>(SolvePartOne());
            _part2 = new Lazy<string>(SolvePartTwo());
        }

        public string Solve(int part = 0) {
            string output = $"--- Day {Day}: {Title} --- \n";
            if(part == 0 || part == 1) {
                output += $"Part 1: {Part1}\n"; 
            }
            if(part == 0 || part == 2) {
                output += $"Part 2: {Part2}\n";
            }
            return output; 
        }

        // Method for retrieving the puzzle input for the given day
        string LoadInput() {
            string INPUT_FILEPATH = $"./Solutions/Year{Year}/Day{Day.ToString("D2")}/input";
            string INPUT_URL = $"https://adventofcode.com/{Year}/day/{Day}/input";

            if(!File.Exists(INPUT_FILEPATH)) {
                try {
                    using (WebClient client = new WebClient()) {
                        client.Headers.Add(HttpRequestHeader.Cookie, Program.Config.Cookie);
                        File.WriteAllText(INPUT_FILEPATH, client.DownloadString(INPUT_URL).Trim());
                    }
                } catch (WebException e) {
                    if (e.Status.Equals(400)) {
                        Console.WriteLine("Error code 400 when requesting input through web client. Make sure your cookie is configured correctly.");
                    } else if (e.Status.Equals(404)) {
                        Console.WriteLine("Error code 404 when requesting input through web client. It might not be available yet.");
                    } else {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
            return File.ReadAllText(INPUT_FILEPATH);
        }

        // Abstract methods that must be defined in each instantiated subclass of ASolution
        protected abstract string SolvePartOne();
        protected abstract string SolvePartTwo();
    }
}
