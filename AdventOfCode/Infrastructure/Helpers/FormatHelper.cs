using System;
using AdventOfCode.Infrastructure.Models;
using AdventOfCode.Solutions;

namespace AdventOfCode.Infrastructure.Helpers
{
    static class FormatHelper
    {
        public static string FormatDay(ASolution solution) => solution.ToString();

        public static string FormatTitle(int day, string title) => $"Day {day}: {title}";

        public static string FormatDebug(string debugInput) => "!! Debug mode active, using DebugInput";

        public static string FormatPart(int part, SolutionResult result)
            => $"  - Part{part} => " + (string.IsNullOrEmpty(result.Answer) ? "Unsolved" : $"{result.Answer} ({result.Time.TotalMilliseconds}ms)");
    }
}