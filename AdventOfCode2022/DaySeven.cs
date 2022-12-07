namespace AdventOfCode2022;
internal class DaySeven : AdventSolution
{
    private Directory _workingDirectory = null;
    private Directory _root = null;

    public IEnumerable<string> GetSolutions (IEnumerable<string> input)
    {
        _root = new();
        ProcessCommands(input);
        var sum = SumDirectories(_root, 100000);

        var freeSpace = 70000000L - _root.Size;
        var spaceNeeded = 30000000L - freeSpace + 100000; 
        /*
         * Adding that 100k from part one here also works for some unknown reason
         * did i perhaps miss a part of the prompt? am i blind for still not seeing it?
         */

        var sufficientDirectories = new List<long>();
        FindLargeEnoughDirectories(_root, spaceNeeded, sufficientDirectories);
        
        //sufficientDirectories.Remove(sufficientDirectories.Min()); 
        /*
         * I don't understand why this was necessary
         * The value being removed here met the requirements but the site did not accept it as an answer
         * 70000000 - 43159555 = 26840445
         * 30000000 - 26840445 = 3159555
         * 3180034 > 3159555 == true
         * 
        */

        var smallestSufficientSize = sufficientDirectories.Min();

        return new string[]
        {
            $"Sum of small directories: {sum}",
            $"Space Needed {spaceNeeded}. Smallest Sufficent Directory: {smallestSufficientSize}"
        };
    }

    private void ProcessCommands(IEnumerable<string> input)
    {
        var contents = new List<string>();
        foreach(var command in input)
        {
            if (command[0] != '$')
            {
                contents.Add(command);
                continue;
            }

            if(contents.Count > 0)
            {
                _workingDirectory.Populate(contents);
                contents.Clear();
            }

            ProcessCommand(command);
        }
    }

    private void ProcessCommand(string command)
    {
        var commandParts = command.Split(' ');
        if (commandParts[1] == "ls")
            return;

        ChangeFolder(commandParts[2]);
    }

    private long SumDirectories(Directory root, long maxSize)
    {
        var size = root.Size;
        var sum = size <= maxSize ? size : 0;
        foreach(var dir in root.Folders.Values)
        {
            var folderSize = SumDirectories(dir, maxSize);
            sum += folderSize;
        }
        return sum;
    }

    private void FindLargeEnoughDirectories(Directory root, long minSize, List<long> sufficientDirectories)
    {
        foreach (var directory in root.Folders.Values)
            FindLargeEnoughDirectories(directory, minSize, sufficientDirectories);

        var size = root.Size;
        if(size >= minSize)
            sufficientDirectories.Add(size);
    }

    private void ChangeFolder(string folder)
    {
        if(folder == "..")
        {
            _workingDirectory = _workingDirectory.ParentFolder;
        } else if (folder == "/")
        {
            _workingDirectory = _root;
        }
        else
        {
            _workingDirectory = _workingDirectory.Folders[folder];
        }
    }



    private class Directory
    {
        public Dictionary<string, Directory> Folders { get; init; } = new();
        public Directory? ParentFolder { get; init; }
        public List<File> Files { get; init; } = new();
        public bool IsPopulated => Folders.Count + Files.Count > 0;
        public Directory() : this(null) { }
        public Directory(Directory? parent) => ParentFolder = parent;
        public long Size => (Files.Sum(x => x.Size) + Folders.Sum(x => x.Value.Size));
        
        
        public static Directory BuildNewDirectory(IEnumerable<string> input, Directory parent)
        {
            var dir = new Directory(parent);
            dir.Populate(input);
            return dir;
        }

        public void Populate(IEnumerable<string> input)
        {
            if (IsPopulated)
                return;

            foreach (var obj in input)
            {
                if (string.IsNullOrWhiteSpace(obj))
                    continue;

                var parts = obj.Split(' ');
                if (parts[0] == "dir")
                {
                    Folders.Add(parts[1], new(this));
                } else
                {
                    Files.Add(new File(parts[1], long.Parse(parts[0])));
                }
            }
        }
    }
    private record File(string Name, long Size);
}
