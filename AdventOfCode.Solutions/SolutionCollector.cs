using AdventOfCode.Services;

namespace AdventOfCode.Solutions
{
    public class SolutionCollector : IEnumerable<SolutionBase>
    {
        IEnumerable<SolutionBase> Solutions;

        public SolutionCollector() => Solutions = LoadSolutions(ConfigService.GetYear(), ConfigService.GetDays()).ToArray();
        public SolutionCollector(int year, int[] days) => Solutions = LoadSolutions(year, days).ToArray();

        public SolutionBase GetSolution(int day)
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

        public IEnumerator<SolutionBase> GetEnumerator() => Solutions.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IEnumerable<SolutionBase> LoadSolutions(int year, int[] days)
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
                    yield return (SolutionBase)Activator.CreateInstance(solution);
                }
            }
        }
    }
}
