using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Infrastructure.Models;
using AdventOfCode.Solutions;

namespace AdventOfCode.Infrastructure
{
    class SolutionCollector : IEnumerable<ASolution>
    {
        readonly static Config config = Config.Get("config.json");

        IEnumerable<ASolution> Solutions;

        public SolutionCollector() => Solutions = LoadSolutions(config.Year, config.Days).ToArray();
        public SolutionCollector(int year, int[] days) => Solutions = LoadSolutions(year, days).ToArray();

        public ASolution GetSolution(int day)
        {
            try
            {
                return Solutions.Single(s => s.Day == day);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        public IEnumerator<ASolution> GetEnumerator() => Solutions.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IEnumerable<ASolution> LoadSolutions(int year, int[] days)
        {
            if (days.Sum() == 0)
            {
                days = Enumerable.Range(1, 25).ToArray();
            }

            foreach (int day in days)
            {
                var solution = Type.GetType($"AdventOfCode.Solutions.Year{year}.Day{day.ToString("D2")}");
                if (solution != null)
                {
                    yield return (ASolution)Activator.CreateInstance(solution);
                }
            }
        }
    }
}
