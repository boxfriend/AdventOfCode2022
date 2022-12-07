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

        var freeSpace = 70000000 - _root.Size;
        var spaceNeeded = 30000000 - freeSpace;

        var sufficientDirectories = new List<int>();
        FindLargeEnoughDirectories(_root, spaceNeeded, sufficientDirectories);

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

            PopulateDir();

            ProcessCommand(command);
        }
        PopulateDir(); //remember kids, don't forget to process *all* instances of an ls command

        void PopulateDir()
        {
            if (contents.Count > 0)
            {
                _workingDirectory.Populate(contents);
                contents.Clear();
            }
        }
    }

    private void ProcessCommand(string command)
    {
        var commandParts = command.Split(' ');
        if (commandParts[1] == "ls")
            return;

        ChangeFolder(commandParts[2]);
    }

    private static int SumDirectories(Directory root, int maxSize)
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

    private static void FindLargeEnoughDirectories(Directory root, int minSize, List<int> sufficientDirectories)
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
        private int _fileSize = 0;
        public bool IsPopulated => Folders.Count + Files.Count > 0;
        public Directory() : this(null) { }
        public Directory(Directory? parent) => ParentFolder = parent;
        public int Size => _fileSize + Folders.Sum(x => x.Value.Size);

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
                    var size = int.Parse(parts[0]);
                    Files.Add(new File(parts[1], size));
                    _fileSize += size;
                }
            }
        }
    }
    private record File(string Name, int Size);
}
