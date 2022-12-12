namespace AdventOfCode2022;
internal class DayTwelve : AdventSolution
{
    private static readonly Point[] Directions = { Point.Up, Point.Right, Point.Down, Point.Left };
    public IEnumerable<string> GetSolutions (IEnumerable<string> input)
    {
        var positions = new char[input.Count(),input.First().Length];
        var row = 0;
        foreach(var line in input)
        {
            for(var i = 0; i < line.Length; i++)
                positions[row,i] = line[i];
            row++;
        }
        var startPosition = new Point(0, 0);
        var endPosition = new Point(0, 0);

        for(var i = 0; i < positions.GetLength(0); i++)
        {
            for(var j = 0; j < positions.GetLength(1); j++)
            {
                if (positions[i, j] == 'E')
                    endPosition = new(i, j);
                if (positions[i, j] == 'S')
                    startPosition = new(i, j);
            }
        }
        var count = MinimumStepsToDestination(startPosition, endPosition, positions);

        return new string[]
        {
            $"Minimum Steps to end: {count}"
        };
    }

    //This is a pretty basic version of the A* algorithm
    private int MinimumStepsToDestination (Point origin, Point destination, char[,] positions)
    {
        var openPositions = new Stack<(Point point,Node parent)>();
        var originNode = new Node(null, 0, 0);
        openPositions.Push((origin,originNode));
        var closedPositions = new Dictionary<Point, Node>
        {
            { origin, originNode }
        };

        while (openPositions.Count > 0)
        {
            var pos = openPositions.Pop();
            EvaluateSuccessors(pos.point, pos.parent);
        }


        var count = 0;
        var nextPosition = closedPositions[destination].Parent;
        while(nextPosition != null)
        {
            nextPosition = nextPosition.Parent;
            count++;
        }


        return count;
        
        void EvaluateSuccessors(Point point, Node parent)
        {
            var hPoint = point - destination;
            var h = Math.Abs(hPoint.X) + Math.Abs(hPoint.Y);

            var node = (point != origin) ? new Node(parent, parent.MoveCost + 1, h) : parent;

            if (!closedPositions.TryAdd(point,node))
            {
                var oldNode = closedPositions[point];
                if (oldNode!.F <= node.F && node != originNode)
                    return;

                closedPositions[point] = node;
            }

            foreach(var dir in Directions)
            {
                var nextPosition = point + dir;
                if(!ValidPosition(nextPosition, positions.GetLength(0), positions.GetLength(1)))
                    continue;

                var thisSpot = positions[point.X, point.Y];
                var nextSpot = positions[nextPosition.X,nextPosition.Y];
                if (point != origin && point != destination && nextSpot - thisSpot > 1)
                    continue;

                openPositions.Push((nextPosition, node));
            }
        }
    }

    private bool ValidPosition (Point position, int maxRow, int maxColumn) =>
        InRangeMaxExclusive(position.X, 0, maxRow) && InRangeMaxExclusive(position.Y, 0, maxColumn);

    private bool InRangeMaxExclusive(int toCheck, int min, int max) => toCheck >= min && toCheck < max;

    private record Node(Node? Parent, float MoveCost, float HeuristicCost)
    {
        public float F => MoveCost + HeuristicCost;
    }
}
