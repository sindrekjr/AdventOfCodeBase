using System;
using AdventOfCode.Infrastructure.Models;
using AdventOfCode.Solutions;

namespace AdventOfCode.Infrastructure.Helpers
{
    static class FormatHelper
    {
        public static string SimpleFormat(ASolution solution)
            => $"--- {FormatTitle(solution.Day, solution.Title)} --- \n"
                + (solution.Debug ? FormatDebug(solution.DebugInput) + "\n" : "")
                + $"Part 1: {solution.Part1.Answer}\n"
                + $"Part 2: {solution.Part2.Answer}\n";

        public static string FunctionFormat(ASolution solution)
            => $"{FormatTitle(solution.Day, solution.Title)}\n"
                + (solution.Debug ? FormatDebug(solution.DebugInput) + "\n" : "")
                + $"{FormatPart(1, solution.Part1)}\n"
                + $"{FormatPart(2, solution.Part2)}\n";

        public static string FormatTitle(int day, string title) => $"Day {day}: {title}";

        public static string FormatDebug(string debugInput) => "!! Debug mode active, using DebugInput";

        public static string FormatPart(int part, SolutionResult result)
            => $"  - Part{part} => " + (string.IsNullOrEmpty(result.Answer) ? "Unsolved" : $"{result.Answer} ({result.Time.TotalMilliseconds}ms)");
    }
}