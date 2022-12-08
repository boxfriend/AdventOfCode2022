using AdventOfCode2022;

const string inputFolder = @"D:\Users\cawil\Documents\C# Stuff\AdventOfCode2022\Input";

Console.WriteLine("Day One: Calorie Counting");
string dayOneFile = Path.Join(inputFolder, "dayone.txt");
var dayOneOutput = await SolutionGetter.GetSolutions<DayOne>(dayOneFile);
foreach (var s in dayOneOutput)
    Console.WriteLine(s);

Console.WriteLine("Day Two: Rock Paper Scissors");
string dayTwoFile = Path.Join(inputFolder, "daytwo.txt");
var dayTwoOutput = await SolutionGetter.GetSolutions<DayTwo>(dayTwoFile);
foreach (var s in dayTwoOutput)
    Console.WriteLine(s);

Console.WriteLine("Day Three: Rucksack Reorganization");
string dayThreeFile = Path.Join(inputFolder, "daythree.txt");
var dayThreeOutput = await SolutionGetter.GetSolutions<DayThree>(dayThreeFile);
foreach (var s in dayThreeOutput)
    Console.WriteLine(s);

Console.WriteLine("Day Four: Camp Cleanup");
string dayFourFile = Path.Join(inputFolder, "dayfour.txt");
var dayFourOutput = await SolutionGetter.GetSolutions<DayFour>(dayFourFile);
foreach (var s in dayFourOutput)
    Console.WriteLine(s);

Console.WriteLine("Day Five: Supply Stacks");
string dayFiveFile = Path.Join(inputFolder, "dayfive.txt");
var dayFiveOutput = await SolutionGetter.GetSolutions<DayFive>(dayFiveFile);
foreach (var s in dayFiveOutput)
    Console.WriteLine(s);

Console.WriteLine("Day Six: Tuning Trouble");
string daySixFile = Path.Join(inputFolder, "daysix.txt");
var daySixOutput = await SolutionGetter.GetSolutions<DaySix>(daySixFile);
foreach (var s in daySixOutput)
    Console.WriteLine(s);

Console.WriteLine("Day Seven: No Space Left On Device");
string daySevenFile = Path.Join(inputFolder, "dayseven.txt");
var daySevenOutput = await SolutionGetter.GetSolutions<DaySeven>(daySevenFile);
foreach (var s in daySevenOutput)
    Console.WriteLine(s);

Console.WriteLine("Day Eight: Treetop Tree House");
string dayEightFile = Path.Join(inputFolder, "dayeight.txt");
var dayEightOutput = await SolutionGetter.GetSolutions<DayEight>(dayEightFile);
foreach (var s in dayEightOutput)
    Console.WriteLine(s);
