namespace AdventOfCode2022;

internal interface AdventSolution
{
    public IEnumerable<string> GetSolutions(IEnumerable<string> input);
}

internal static class SolutionGetter
{
    public static async Task<IEnumerable<string>> GetSolutions<T>(string inputPath) where T : AdventSolution, new()
    {
        var solution = new T();
        var inputLines = await File.ReadAllLinesAsync(inputPath);
        return solution.GetSolutions(inputLines);
    }
}