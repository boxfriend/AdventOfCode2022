namespace AdventOfCode2022;
internal class DayOne : AdventSolution
{
    public IEnumerable<string> GetSolutions(IEnumerable<string> input)
    {
        const int numberOfEntries = 3; //Top three elves carrying most calories

        var calories = GetCalorieCounts(input);
        var topCombined = calories.OrderByDescending(x => x).Take(numberOfEntries).Sum();
        var output = new string[]
        {
            $"Top Calorie Count: {calories.Max()}",
            $"Top {numberOfEntries} combined: {topCombined}"
        };

        return output;
    }

    private static List<int> GetCalorieCounts(IEnumerable<string> input)
    {
        var calories = new List<int>();
        var count = 0;
        foreach (var entry in input)
        {
            if (string.IsNullOrWhiteSpace(entry))
            {
                calories.Add(count);
                count = 0;
                continue;
            }

            var cal = int.Parse(entry);
            count += cal;
        }
        return calories;
    }
}
