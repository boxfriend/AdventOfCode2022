namespace AdventOfCode2022;
internal class DaySix : AdventSolution
{
    public IEnumerable<string> GetSolutions (IEnumerable<string> input)
    {
        var markerAfterChar = FindMarkerCharacter(input.First(),4);
        var messageMarker = FindMarkerCharacter(input.First(), 14);

        return new string[]
        {
            $"Start of Packet Marker: {markerAfterChar}",
            $"Start of Message Marker: {messageMarker}"
        };
    }

    private static int FindMarkerCharacter(string input, int numberDistinct)
    {
        var uniqueChars = new HashSet<char>();
        var backPointer = 0;
        for(var i = 0; i < input.Length; i++)
        {
            if(!uniqueChars.Add(input[i]))
            {
                var charToFind = input[i];
                for(; backPointer < input.Length; backPointer++)
                {
                    if (input[backPointer] == charToFind)
                    {
                        backPointer++;
                        break;
                    }
                    uniqueChars.Remove(input[backPointer]);
                }    
            }

            if (uniqueChars.Count >= numberDistinct)
                return i+1; //characters are 1-indexed so we must add 1 to get the character number rather than string index
        }

        return -1; //Should only reach here if no marker is found
    }
}
