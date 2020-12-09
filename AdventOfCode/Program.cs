using System;
using AdventOfCode.Infrastructure;
using AdventOfCode.Infrastructure.Helpers;
using AdventOfCode.Infrastructure.Models;
using AdventOfCode.Solutions;

namespace AdventOfCode
{
    class Program
    {
        public static Config Config = Config.Get("config.json");
        static SolutionCollector Solutions = new SolutionCollector(Config.Year, Config.Days);

        static void Main(string[] args)
        {
            foreach (ASolution solution in Solutions)
            {
                Console.WriteLine();
                Console.WriteLine(FormatHelper.FormatDay(solution));
            }
        }
    }
}
