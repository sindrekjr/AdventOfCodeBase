global using System;
global using System.Collections;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;

using System.Diagnostics;
using System.Net;
using AdventOfCode.Services;

namespace AdventOfCode.Solutions
{
    public abstract class SolutionBase
    {
        public int Day { get; }
        public int Year { get; }
        public string Title { get; }
        public bool Debug { get; set; }
        public string? Input => LoadInput().Result;
        public string? DebugInput => LoadDebugInput();

        public SolutionResult Part1 => SolveSafely(SolvePartOne);
        public SolutionResult Part2 => SolveSafely(SolvePartTwo);


        private protected SolutionBase(int day, int year, string title, bool useDebugInput = false)
        {
            Day = day;
            Year = year;
            Title = title;
            Debug = useDebugInput;
        }

        public IEnumerable<SolutionResult> Solve(int part = 0)
        {
            if (part != 2 && (part == 1 || !string.IsNullOrEmpty(Part1.Answer)))
            {
                yield return Part1;
            }

            if (part != 1 && (part == 2 || !string.IsNullOrEmpty(Part2.Answer)))
            {
                yield return Part2;
            }
        }

        SolutionResult SolveSafely(Func<string> SolverFunction)
        {
            if (Debug)
            {
                if (string.IsNullOrEmpty(DebugInput))
                {
                    throw new Exception("DebugInput is null or empty");
                }
            }
            else if (string.IsNullOrEmpty(Input))
            {
                throw new Exception("Input is null or empty");
            }

            try
            {
                var then = DateTime.Now;
                var result = SolverFunction();
                var now = DateTime.Now;
                return string.IsNullOrEmpty(result) ? SolutionResult.Empty : new SolutionResult { Answer = result, Time = now - then };
            }
            catch (Exception)
            {
                if (Debugger.IsAttached)
                {
                    Debugger.Break();
                    return SolutionResult.Empty;
                }
                else
                {
                    throw;
                }
            }
        }

        static HttpClientHandler handler = new HttpClientHandler { UseCookies = true };
        static HttpClient client = new HttpClient(handler);

        async Task<string> LoadInput()
        {
            var inputFilepath = $"./AdventOfCode.Solutions/Year{Year}/Day{Day:D2}/input";
            var inputUri = new Uri($"https://adventofcode.com/{Year}/day/{Day}/input");

            if (File.Exists(inputFilepath) && new FileInfo(inputFilepath).Length > 0)
            {
                return File.ReadAllText(inputFilepath);
            }

            try
            {
                var currentEst = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc).AddHours(-5);
                if (currentEst < new DateTime(Year, 12, Day)) throw new InvalidOperationException("Too early.");

                handler.CookieContainer.Add(new Cookie { Name = "session", Domain = ".adventofcode.com", Value = ConfigService.GetCookie() });
                var request = new HttpRequestMessage { RequestUri = inputUri, Method = HttpMethod.Get };

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var input = await response.Content.ReadAsStringAsync();
                File.WriteAllText(inputFilepath, input);

                return input;
            }
            catch (HttpRequestException e)
            {
                var code = e.StatusCode;
                var colour = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                if (code == HttpStatusCode.BadRequest)
                {
                    Console.WriteLine($"Day {Day}: Received 400 when attempting to retrieve puzzle input. Your session cookie is probably not recognized.");

                }
                else if (code == HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"Day {Day}: Received 404 when attempting to retrieve puzzle input. The puzzle is probably not available yet.");
                }
                else
                {
                    Console.ForegroundColor = colour;
                    Console.WriteLine(e.ToString());
                }
                Console.ForegroundColor = colour;
            }
            catch (InvalidOperationException)
            {
                var colour = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Day {Day}: Cannot fetch puzzle input before given date (Eastern Standard Time).");
                Console.ForegroundColor = colour;
            }

            return "";
        }

        string LoadDebugInput()
        {
            string inputFilepath = $"./AdventOfCode.Solutions/Year{Year}/Day{Day:D2}/debug";
            return (File.Exists(inputFilepath) && new FileInfo(inputFilepath).Length > 0)
                ? File.ReadAllText(inputFilepath)
                : "";
        }

        public override string ToString() =>
            $"--- Day {Day}: {Title} --- {(Debug ? "!! Debug mode active, using DebugInput !!" : "")}\n"
            + $"{ResultToString(1, Part1)}\n"
            + $"{ResultToString(2, Part2)}\n";

        string ResultToString(int part, SolutionResult result) =>
            $"  - Part{part} => " + (string.IsNullOrEmpty(result.Answer) 
                ? "Unsolved"
                : $"{result.Answer} ({result.Time.TotalMilliseconds}ms)");

        protected abstract string SolvePartOne();
        protected abstract string SolvePartTwo();
    }
}