namespace AdventOfCode2022;
internal class DayTwo : AdventSolution
{
    public IEnumerable<string> GetSolutions (IEnumerable<string> input)
    {
        var matches = new List<GameRound>();
        var strategyOutcome = new List<GameRound>();
        foreach(var round in input)
        {
            if(string.IsNullOrWhiteSpace(round))
                continue;

            var player1Play = GetShape(round[0]);
            var player2Play = GetShape(round[2]);
            matches.Add(new GameRound(player1Play, player2Play));
            player2Play = StrategyPlay(player1Play, round[2]);
            strategyOutcome.Add(new GameRound(player1Play, player2Play));
        }

        var player1Score = matches.Sum(x => x.PlayerOne.Points);
        var player2Score = matches.Sum(x => x.PlayerTwo.Points);
        var strategyScore = strategyOutcome.Sum(x => x.PlayerTwo.Points);

        return new string[]
        {
            $"Your Score: {player2Score}, Opponent Score: {player1Score}",
            $"Using the strategy you would get {strategyScore} points"
        };

    }

    private static RPS StrategyPlay(RPS player1, char player2)
    {
        var desiredState = player2 switch
        { 
            'X' => WinState.Loss,
            'Y' => WinState.Tie,
            'Z' => WinState.Win,
            _ => throw new ArgumentException($"{player2} is not a valid argument. {nameof(StrategyPlay)} parameter {nameof(player2)} takes characters A,B,C,X,Y,Z")
        };

        return desiredState switch
        {
            WinState.Win => GetWin(player1),
            WinState.Loss => GetLoss(player1),
            WinState.Tie => player1
        };
    }

    private static RPS GetWin (RPS opponent) => opponent switch
    {
        RPS.Paper => RPS.Scissors,
        RPS.Rock => RPS.Paper,
        RPS.Scissors => RPS.Rock
    };

    private static RPS GetLoss (RPS opponent) => opponent switch
    {
        RPS.Paper => RPS.Rock,
        RPS.Rock => RPS.Scissors,
        RPS.Scissors => RPS.Paper
    };

    private static RPS GetShape (char play) => play switch
    {
        'A' or 'X' => RPS.Rock,
        'B' or 'Y' => RPS.Paper,
        'C' or 'Z' => RPS.Scissors,
        _ => throw new ArgumentException($"{play} is not a valid argument. {nameof(GetShape)} takes characters A,B,C,X,Y,Z")
    };

    private enum WinState //Define enum values equal to number of points each state is worth
    {
        Win = 6,
        Loss = 0,
        Tie = 3
    }

    private enum RPS
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }

    private struct GameRound
    { 
        public Player PlayerOne { get; init; }
        public Player PlayerTwo { get; init; }


        public GameRound(RPS playerOne, RPS playerTwo)
        {
            PlayerOne = new Player(playerOne, Evaluate(playerOne, playerTwo));
            PlayerTwo = new Player(playerTwo, Evaluate(playerTwo, playerOne));
        }

        private static WinState Evaluate(RPS player, RPS opponent)
        {
            if (player == opponent)
                return WinState.Tie;

            return (player, opponent) switch
            {
                (RPS.Paper, RPS.Scissors) => WinState.Loss,
                (RPS.Scissors, RPS.Rock) => WinState.Loss,
                (RPS.Rock, RPS.Paper) => WinState.Loss,
                _ => WinState.Win
            };
        }
    }
    private record Player (RPS Shape, WinState State)
    {
        public int Points = (int)Shape + (int)State;
    }
}