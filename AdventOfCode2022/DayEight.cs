namespace AdventOfCode2022;
internal class DayEight : AdventSolution
{
    private int[,]? _treeGrid;
    public IEnumerable<string> GetSolutions (IEnumerable<string> input)
    {
        var rowLength = input.First().Length;
        var rowCount = input.Count();
        _treeGrid = new int[rowCount, rowLength];

        var row = 0;
        foreach(var treeRow in input)
        {
            var column = 0;
            foreach(var tree in treeRow)
            {
                _treeGrid[row, column] = (int)char.GetNumericValue(tree);
                column++;
            }
            row++;
        }

        var visibleTrees = NumberVisibleFromEdge();
        var bestScenicScore = HighestScenicScore();
        return new string[]
        {
            $"Trees visible from the edge: {visibleTrees}",
            $"Highest Scenic Score is: {bestScenicScore}"
        };
    }

    private int NumberVisibleFromEdge()
    {
        var visibleTrees = new HashSet<(int, int)>();
        for(var i = 0; i < _treeGrid.GetLength(0); i++)
        {
            for(var j = 0; j < _treeGrid.GetLength(1); j++)
            {
                if (IsVisible(i, j))
                    visibleTrees.Add((i, j));
            }
        }
        return visibleTrees.Count;
    }

    private int HighestScenicScore()
    {
        var scenicScores = new int[_treeGrid.GetLength(0), _treeGrid.GetLength(1)];

        for(var i = 0; i < _treeGrid.GetLength(0); i++)
        {
            for(var j = 0; j < _treeGrid.GetLength(1); j++)
            {
                scenicScores[i, j] = ScenicScore(i,j);
            }
        }
        return scenicScores.OfType<int>().Max();
    }

    private int ScenicScore(int row, int column)
    {
        var topCount = VisibleCheck(row, column, 0, 1).numberVisible;
        var bottomCount = VisibleCheck(row, column, 0, -1).numberVisible;
        var rightCount = VisibleCheck(row, column, -1, 0).numberVisible;
        var leftCount = VisibleCheck(row, column, 1, 0).numberVisible;

        return topCount * bottomCount * rightCount * leftCount;
    }

    private bool IsVisible(int row, int column)
    {
        var topVisible = VisibleCheck(row,column,0,1).visible;
        var bottomVisible = VisibleCheck(row, column, 0, -1).visible;
        var leftVisible = VisibleCheck(row, column, 1, 0).visible;
        var rightVisible = VisibleCheck(row, column, -1, 0).visible;

        return rightVisible || topVisible || leftVisible || bottomVisible;
    }

    private (bool visible, int numberVisible) VisibleCheck(int row, int column, int directionRow, int directionColumn)
    {
        var tree = _treeGrid[row, column];
        row += directionRow;
        column += directionColumn;
        var count = 0;
        while(row < _treeGrid.GetLength(0) && row >= 0 && column < _treeGrid.GetLength(1) && column >= 0)
        {
            count++;
            if (_treeGrid[row, column] >= tree)
                return (false, count);

            row += directionRow;
            column += directionColumn;
        }
        return (true, count);
    }
}
