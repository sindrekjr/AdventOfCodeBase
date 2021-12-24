global using System;
global using System.Collections;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;

using System.Diagnostics;

using AdventOfCode.Services;

namespace AdventOfCode.Solutions
{
    public abstract class SolutionBase
    {
        public int Day { get; }
        public int Year { get; }
        public string Title { get; }
        public bool Debug { get; set; }
        public string? Input => InputService.FetchInput(Day, Year);
        public string? DebugInput => InputService.FetchDebugInput(Day, Year);

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