using System.Text;

namespace AdventOfCode2022;
internal class DayTen : AdventSolution
{
    public IEnumerable<string> GetSolutions (IEnumerable<string> input)
    {
        var signalStrengths = new List<int>();
        var builder = new StringBuilder();
        var rows = new List<string>();
        var callback = (int cycles, int register) => 
        {
            var drawPosition = (cycles % 40) - 1;
            var inRange = InRange(drawPosition, register - 1, register + 1);
            var charToAdd = inRange ? '#' : '.';
            builder.Append(charToAdd);
            if (cycles % 40 == 0)
            {
                rows.Add(builder.ToString());
                builder.Clear();
            }

            if ((cycles - 20) % 40 == 0)
            {
                signalStrengths.Add(cycles * register);
            }
        };
        var cpu = new CPU(callback);
        foreach(var command in input)
            ProcessCommand(cpu, command);

        var summedSignals = signalStrengths.Sum();

        var results = new List<string>() { summedSignals.ToString() };
        results.AddRange(rows);
        return results;
    }

    private static bool InRange (int toCheck, int min, int max) => toCheck >= min && toCheck <= max;

    private static void ProcessCommand(CPU cpu, string command)
    {
        if (string.IsNullOrWhiteSpace(command))
            return;

        var args = command.Split(' ');

        if (args[0] == "noop")
            cpu.NoOp();
        else if (args[0] == "addx")
            cpu.AddX(int.Parse(args[1]));

    }

    private record CPU(Action<int,int> CyclesCallback)
    {
        private int _register = 1;
        public int Register { get => _register; private set => _register = value; }

        private int _cycles = 0;
        public int Cycles
        {
            get => _cycles;
            private set
            {
                _cycles = value;
                CyclesCallback?.Invoke(_cycles, _register);
            }
        }

        public void AddX(int x)
        {
            Cycles++;
            Cycles++;
            Register += x;
        }
        public void NoOp () => Cycles++;
    }
}
