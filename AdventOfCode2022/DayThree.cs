namespace AdventOfCode2022;
internal class DayThree : AdventSolution
{
    public IEnumerable<string> GetSolutions (IEnumerable<string> input)
    {
        var prioritySum = GetSharedItems(input).Sum(x => GetPriority(x));
        var badgeItemSum = GetBadges(input,3).Sum(x => GetPriority(x));
        
        return new string[]
        {
            $"Sum of items in both compartments: {prioritySum}",
            $"Sum of badge items is {badgeItemSum}"
        };
    }

    private static IEnumerable<char> GetSharedItems(IEnumerable<string> input)
    {
        var sharedItems = new List<char>();
        foreach (var sack in input)
        {
            if (string.IsNullOrWhiteSpace(sack))
                continue;

            var halfLength = sack.Length / 2;
            var firstCompartment = sack[..halfLength];
            var secondCompartment = sack[halfLength..];

            sharedItems.AddRange(GetShared(firstCompartment, secondCompartment));
        }
        return sharedItems;
    }

    private static IEnumerable<char> GetBadges(IEnumerable<string> input, int badgeGroupCount)
    {
        var badges = new List<char>();
        var counter = 0;
        var badgeGroup = new string[badgeGroupCount];
        foreach(var sack in input)
        {
            if(counter >= badgeGroupCount)
            {
                counter = 0;
                badges.AddRange(GetShared(badgeGroup));
            }

            badgeGroup[counter++] = sack;
        }
        badges.AddRange(GetShared(badgeGroup));
        return badges;
    }

    private static IEnumerable<char> GetShared (params string[] inputs)
    {
        IEnumerable<char>? i = null;
        foreach(var input in inputs)
        {
            if(i is null)
            {
                i = new HashSet<char>(input);
                continue;
            }

            i = i.Intersect(new HashSet<char>(input));
        }
        return i!;
    }

    private static int GetPriority(char character)
    {
        if (!char.IsLetter(character))
            throw new ArgumentOutOfRangeException($"{nameof(character)} must be a letter");

        if (char.IsLower(character))
            return character - 'a' + 1; //priority is 1-indexed so must increase value by 1
        else
            return character - 'A' + 27;
    }
}
