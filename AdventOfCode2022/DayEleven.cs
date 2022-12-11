namespace AdventOfCode2022;
internal class DayEleven : AdventSolution
{
    public IEnumerable<string> GetSolutions (IEnumerable<string> input)
    {
        var topX = 2;
        var monkeysPart1 = ProcessMonkeys(input, 20, true);
        var mostActiveP1 = monkeysPart1.OrderByDescending(x => x.InspectCount).Take(topX).ToArray();
        var monkeysPart2 = ProcessMonkeys(input, 10000, false);
        var mostActiveP2 = monkeysPart2.OrderByDescending(x => x.InspectCount).Take(topX).ToArray();

        var monkeyBusinessPart1 = 1;
        var monkeyBusinessPart2 = 1L;
        
        for(var i = 0; i < topX; i++)
        {
            monkeyBusinessPart1 *= mostActiveP1[i].InspectCount;
            monkeyBusinessPart2 *= mostActiveP2[i].InspectCount;
        }

        return new string[]
        {
            $"Monkey Business after 20 rounds for top {topX} monkeys is {monkeyBusinessPart1}",
            $"Monkey Business after 10000 rounds for top {topX} monkeys is {monkeyBusinessPart2}"
        };
    }

    private static List<Monkey> ProcessMonkeys(IEnumerable<string> input, int roundsCount, bool worryDecrease)
    {
        var monkeyInfo = new List<string>();
        var monkeys = new Dictionary<int, Monkey>();
        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                ProcessMonkey();
                continue;
            }
            monkeyInfo.Add(line);
        }
        ProcessMonkey();

        var mod = 1;
        foreach (var m in monkeys.Values)
            mod *= m.TestValue;

        for (var i = 0; i < roundsCount; i++)
        {
            foreach (var monkey in monkeys.Values)
            {
                monkey.InspectItems(worryDecrease, mod);
                monkey.ThrowItems(monkeys);
            }
        }

        return monkeys.Values.ToList();

        void ProcessMonkey ()
        {
            var monkey = CreateMonkey(monkeyInfo);
            monkeys.Add(monkey.ID, monkey);
            monkeyInfo.Clear();
        }
    }

    private static Monkey CreateMonkey(IEnumerable<string> input)
    {
        var monkey = new Monkey();
        foreach(var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
                throw new InvalidOperationException("somehow the monkey input was wrong");

            var parts = line.Split(' ',StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            switch (parts[0].ToLower().Replace(":",""))
            {
                case "monkey":
                    monkey.ID = int.Parse(parts[1].Replace(":",""));
                    break;
                case "starting":
                    monkey.AssignStartItems(parts);
                    break;
                case "operation":
                    monkey.ParseOperation(parts[^3..]);
                    break;
                case "test":
                    monkey.TestValue = int.Parse(parts[^1]);
                    break;
                case "if":
                    var trueTest = bool.Parse(parts[1].Replace(":",""));
                    monkey.AssignThrowTarget(trueTest, int.Parse(parts[^1]));
                    break;
                default:
                    throw new NotImplementedException($"First word of input is not valid to create a monkey: {parts[0]}");
            }
        }
        return monkey;
    }

    private class Monkey 
    {

        public int ID { get; set; } = 0;
        public int TestValue { get; set; } = 0;
        public int TrueMonkey { get; private set; } = 0;
        public int FalseMonkey { get; private set; } = 0;
        private Func<long,long>? Operation { get; set; } = null;
        public List<Item> Items = new();
        public int InspectCount { get; private set; } = 0;

        public void InspectItems(bool worryLevelDecrease, int worryMod)
        {
            InspectCount += Items.Count;
            for(var i = 0; i < Items.Count; i++)
            {
                Items[i].WorryLevel = Operation.Invoke(Items[i].WorryLevel);

                if (worryLevelDecrease)
                    Items[i].WorryLevel /= 3;
                else
                    Items[i].WorryLevel %= worryMod;
            }
        }

        public void ParseOperation (string[] parts)
        {
            if (parts.Length != 3)
                throw new ArgumentException("number of parts for the operation is wrong");

            if (parts[1] == "*")
            {
                Operation = (parts[0] == parts[^1]) ? (long x) => x * x : (long x) => x * int.Parse(parts[^1]);
            }else
            {
                Operation = (parts[0] == parts[^1]) ? (long x) => x * 2 : (long x) => x + int.Parse(parts[^1]);
            }

        }

        public void ThrowItems(Dictionary<int, Monkey> monkeys)
        {
            for (var i = Items.Count - 1; i >= 0; i--)
            {
                var item = Items[i];
                if (item.WorryLevel % TestValue == 0)
                {
                    monkeys[TrueMonkey].Items.Add(item);
                }else
                {
                    monkeys[FalseMonkey].Items.Add(item);
                }
                Items.RemoveAt(i);
            }
        }

        public void AssignStartItems (string[] info)
        {
            foreach(var item in info)
            {
                if (string.IsNullOrWhiteSpace(item))
                    throw new InvalidOperationException("somehow the item was empty?");

                if(int.TryParse(item.Replace(",",""), out var itemNumber))
                {
                    Items.Add(new Item(itemNumber));
                }
            }
        }

        public void AssignThrowTarget(bool testIsTrue, int targetMonkeyID)
        {
            if (testIsTrue)
                TrueMonkey = targetMonkeyID;
            else
                FalseMonkey = targetMonkeyID;
        }
    }
    private class Item 
    { 
        public long WorryLevel { get; set; }
        public Item(long worryLevel) => WorryLevel = worryLevel;
            
    }
}
