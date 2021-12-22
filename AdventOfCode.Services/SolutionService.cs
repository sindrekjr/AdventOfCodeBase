using AdventOfCode.Services.Models;

namespace AdventOfCode.Services
{
    public static class SolutionService
    {
        public static IEnumerable<ISolution> FetchSolutions() => FetchSolutions(ConfigService.GetYear(), ConfigService.GetDays());

        public static IEnumerable<ISolution> FetchSolutions(int year) => FetchSolutions(year, ConfigService.GetDays());

        public static IEnumerable<ISolution> FetchSolutions(int year, int[] days)
        {
            if (days.Sum() == 0)
            {
                days = Enumerable.Range(1, 25).ToArray();
            }

            foreach (int day in days)
            {
                var solution = Type.GetType($"AdventOfCode.Solutions.Year{year}.Day{day:D2}");

                if (solution != null)
                {
                    var iSolution = Activator.CreateInstance(solution) as ISolution;
                    if (iSolution != null) yield return iSolution;
                }
            }
        }
    }
}
