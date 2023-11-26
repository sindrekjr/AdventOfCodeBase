namespace AdventOfCode.Solutions;

public static class SolutionCollector
{
    public static IEnumerable<SolutionBase> FetchSolutions(int year, IEnumerable<int> days)
    {
        if (days.Sum() == 0) days = Enumerable.Range(1, 25).ToArray();

        foreach (int day in days)
        {
            var type = Type.GetType($"AdventOfCode.Solutions.Year{year}.Day{day:D2}.Solution");
            if (type != null)
            {
                if (Activator.CreateInstance(type) is SolutionBase solution)
                {
                    yield return solution;
                }
            }
        }
    }
}
