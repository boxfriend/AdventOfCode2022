namespace AdventOfCode2022;
internal class DayNine : AdventSolution
{
    public IEnumerable<string> GetSolutions (IEnumerable<string> input)
    {
        var visitedPositionCount = GetVisitedPositions(input, 2);
        var visitedLength10 = GetVisitedPositions(input, 10);
        
        return new string[]
        {
            $"Number of positions tail visited: {visitedPositionCount}",
            $"Number of positions with length 10: {visitedLength10}"
        };
    }

    private static int GetVisitedPositions(IEnumerable<string> input, int ropeLength)
    {
        var visitedPositions = new HashSet<Point>
        {
            Point.Zero
        };
        var ropePositions = new Point[ropeLength];
        Array.Fill(ropePositions, new(0, 0));
        foreach (var command in input)
        {
            var targetPosition = ropePositions[0] + GetDirection(command);

            while (ropePositions[0] != targetPosition)
            {
                ropePositions[0] = ropePositions[0].MoveTowards(targetPosition);
                for (var i = 1; i < ropePositions.Length; i++)
                {
                    var prevPosition = ropePositions[i-1];
                    if (ropePositions[i].IsAdjacent(prevPosition))
                        break;

                    ropePositions[i] = ropePositions[i].MoveTowards(prevPosition);
                }
                visitedPositions.Add(ropePositions[^1]);
            }
        }
        return visitedPositions.Count;
    }

    private static Point GetDirection(string command)
    {
        var commandParts = command.Split(' ');
        var distance = int.Parse(commandParts[1]);

        return commandParts[0] switch
        {
            "R" => Point.Right,
            "L" => Point.Left,
            "U" => Point.Up,
            "D" => Point.Down,
            _ => Point.Zero
        } * distance;
    }

    private record struct Point(int X, int Y)
    {
        public static Point Zero => new(0, 0);
        public static Point Up => new(0, 1);
        public static Point Down => new(0, -1);
        public static Point Right => new(1, 0);
        public static Point Left => new(-1, 0);

        public static Point operator + (Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
        public static Point operator - (Point a, Point b) => new(a.X - b.X, a.Y - b.Y);
        public static Point operator * (Point a, int b) => new(a.X * b, a.Y * b);

        public bool IsAdjacent(Point toCheck)
        {
            var distance = Direction(this, toCheck);

            return Math.Abs(distance.X) <= 1 && Math.Abs(distance.Y) <= 1;
        }

        public static Point Direction (Point from, Point to) => to - from;

        public Point MoveTowards(Point to)
        {
            var direction = Direction(this, to);

            return this + new Point(Math.Sign(direction.X), Math.Sign(direction.Y));
        }
    }
}
