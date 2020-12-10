using System;
using AdventOfCode.Infrastructure;
using AdventOfCode.Infrastructure.Helpers;
using AdventOfCode.Solutions;

namespace AdventOfCode
{
    class Program
    {
        static SolutionCollector Solutions = new SolutionCollector();

        static void Main(string[] args)
        {
            foreach (ASolution solution in Solutions)
            {
                Console.WriteLine();
                Console.WriteLine(FormatHelper.FunctionFormat(solution));
            }
        }
    }
}
