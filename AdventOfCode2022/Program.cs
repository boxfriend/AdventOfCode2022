using AdventOfCode2022;

const string inputFolder = @"D:\Users\cawil\Documents\C# Stuff\AdventOfCode2022\Input";

await GetSolution<DayOne>("dayone.txt", "Day One: Calorie Counting");
await GetSolution<DayTwo>("daytwo.txt", "Day Two: Rock Paper Scissors");
await GetSolution<DayThree>("daythree.txt", "Day Three: Rucksack Reorganization");
await GetSolution<DayFour>("dayfour.txt", "Day Four: Camp Cleanup");
await GetSolution<DayFive>("dayfive.txt", "Day Five: Supply Stacks");
await GetSolution<DaySix>("daysix.txt", "Day Six: Tuning Trouble");
await GetSolution<DaySeven>("dayseven.txt", "Day Seven: No Space Left On Device");
await GetSolution<DayEight>("dayeight.txt", "Day Eight: Treetop Tree House");
await GetSolution<DayNine>("daynine.txt", "Day Nine: Rope Bridge");
await GetSolution<DayTen>("dayten.txt", "Day Ten: Cathode-Ray Tube");
await GetSolution<DayEleven>("dayeleven.txt", "Day Eleven: Monkey in the Middle");
await GetSolution<DayTwelve>("daytwelve.txt", "Day Twelve: Hill Climbing Algorithm");

async static Task GetSolution<T>(string fileName, string dayTitle) where T : AdventSolution, new()
{
    Console.WriteLine(dayTitle);
    var file = Path.Join(inputFolder, fileName);
    var output = await SolutionGetter.GetSolutions<T>(file);
    foreach (var s in output)
        Console.WriteLine(s);
}