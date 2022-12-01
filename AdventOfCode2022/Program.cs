using AdventOfCode2022;

Console.WriteLine("Day One: Calorie Counting");
const string dayOneFile = @"D:\Users\cawil\Documents\C# Stuff\AdventOfCode2022\Input\dayone.txt";
var dayOneOutput = await SolutionGetter.GetSolutions<DayOne>(dayOneFile);
foreach (var s in dayOneOutput)
    Console.WriteLine(s);
