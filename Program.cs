using AdventOfCode.Services;

foreach (var solution in SolutionService.FetchSolutions())
{
    Console.WriteLine();
    Console.WriteLine(solution.ToString());
}
