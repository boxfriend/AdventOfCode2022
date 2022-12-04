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
