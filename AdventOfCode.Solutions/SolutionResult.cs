namespace AdventOfCode.Solutions;

public struct SolutionResult
{
    public string Answer { get; set; }
    public TimeSpan Time { get; set; }

    public static SolutionResult Empty => new SolutionResult();
}
