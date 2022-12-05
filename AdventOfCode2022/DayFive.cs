using System.Text;

namespace AdventOfCode2022;
internal class DayFive : AdventSolution
{
    public IEnumerable<string> GetSolutions (IEnumerable<string> input)
    {
        var arr = input.ToArray();
        int i;
        for (i = 0; i < arr.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(arr[i]))
                break;
        }
        var stacks = arr[..i];
        var instructions = arr[i..];

        var topCrates = ProcessResults(stacks, instructions, false);
        var preservedOrderCrates = ProcessResults(stacks, instructions, true);

        return new string[]
        {
            $"Top Crates: {topCrates}",
            $"Top Stacks using new process: {preservedOrderCrates}"
        };


        static string ProcessResults(string[] stacks, IEnumerable<string> commands, bool preserveOrder = false)
        {
            var allStacks = GetStacks(stacks);
            ProcessAllInstructions(commands, allStacks, preserveOrder);
            return GetTopCrates(allStacks);
        }

    }

    private static void ProcessAllInstructions(IEnumerable<string> input, Dictionary<int, Stack<char>> stacks, bool preserveOrder)
    {
        foreach(var instruction in input)
        {
            if (string.IsNullOrWhiteSpace(instruction))
                continue;

            ProcessInstruction(instruction, stacks, preserveOrder);
        }
    }

    private static void ProcessInstruction(string instruction, Dictionary<int, Stack<char>> stacks, bool preserveOrder)
    {
        var commands = instruction.Split(' ');
        var count = int.Parse(commands[1]);
        var fromStack = int.Parse(commands[3]);
        var toStack = int.Parse(commands[5]);

        var movedCrates = new StringBuilder();
        for(int i = 0; i < count; i++)
        {
            if (stacks[fromStack].Count <= 0)
                break;

            var crate = stacks[fromStack].Pop();

            if (preserveOrder)
                movedCrates.Insert(0, crate);
            else
                movedCrates.Append(crate);
        }

        foreach (var ch in movedCrates.ToString())
            stacks[toStack].Push(ch);
    }

    private static Dictionary<int, Stack<char>> GetStacks (string[] input)
    {
        var output = new Dictionary<int, Stack<char>>();
        var stacks = input[^1];
        for(var i = 0; i < stacks.Length; i++)
        {
            if(char.IsNumber(stacks[i]))
            {
                var stackNumber = (int)char.GetNumericValue(stacks[i]);
                output.Add(stackNumber, new()); 
                
                //Start from second to last string (last is stack numbers) and work backward to add to stack
                for(var j = input.Length - 2; j >= 0; j--) 
                {
                    var row = input[j];
                    if (row.Length > i && char.IsLetter(row[i]))
                    {
                        output[stackNumber].Push(row[i]);
                    }
                }
            }
        }

        return output;
    }

    private static string GetTopCrates (Dictionary<int, Stack<char>> allStacks)
    {
        var output = new StringBuilder();
        foreach (var stack in allStacks.Values)
            output.Append(stack.Pop());

        return output.ToString();
    }
}
