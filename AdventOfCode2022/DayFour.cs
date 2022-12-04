namespace AdventOfCode2022;
internal class DayFour : AdventSolution
{
    public IEnumerable<string> GetSolutions (IEnumerable<string> input)
    {
        var fullyContained = 0;
        var overlaps = 0;

        foreach(var pair in input)
        {
            var ranges = GetRangePair(pair);
            if (DoesOverlap(ranges))
                overlaps++;
            if(IsFullyContained(ranges))
                fullyContained++;
        }

        return new string[]
        {
            $"Number of fully contained pairs: {fullyContained}",
            $"Total overlaps: {overlaps}"
        };
    }

    private static Pair GetRangePair(string input)
    {
        var ranges = input.Split(',');
        var firstRange = GetRangeFromInput(ranges[0]);
        var secondRange = GetRangeFromInput(ranges[1]);

        return new(firstRange, secondRange);
        
        
        static Range GetRangeFromInput(string input)
        {
            var numbers = input.Split('-');

            var min = int.Parse(numbers[0]);
            var max = int.Parse(numbers[1]);

            return new(min, max);
        }
    }

    private static bool IsFullyContained (Pair pair) => IsFullyContained(pair.First, pair.Second);
    private static bool IsFullyContained(Range first, Range second) => 
        (InRange(first, second.Min) && InRange(first, second.Max)) ||
        (InRange(second, first.Min) && InRange(second, first.Max));

    private static bool DoesOverlap (Pair pair) => DoesOverlap(pair.First, pair.Second);
    private static bool DoesOverlap(Range first, Range second)
    {
        if (InRange(first, second.Min) || InRange(first, second.Max))
            return true;

        if (InRange(second, first.Min) || InRange(second, first.Max))
            return true;

        return false;
    }

    private static bool InRange(Range range, int toCheck) => range.Min <= toCheck && range.Max >= toCheck;

    private record struct Range(int Min, int Max);
    private record struct Pair(Range First, Range Second);
}
