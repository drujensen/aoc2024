class DesignMatcher
{
    public HashSet<string> Patterns { get; set; }
    public List<string> Designs { get; set; }

    public DesignMatcher(string data)
    {
        Patterns = new HashSet<string>();
        Designs = new List<string>();
        
        var parts = data.Split("\n\n");
        foreach (var pattern in parts[0].Split(", "))
        {
            Patterns.Add(pattern);
        }

        foreach (var design in parts[1].Split("\n"))
        {
            if(design.Length == 0)
            {
                continue;
            }
            Designs.Add(design);
        }
    }

    public int FindMatches()
    {
        var count = 0;
        foreach (var design in Designs)
        {
            var (found, path) = FindMatch(design);
            if (found)
            {
                count++;
                Console.WriteLine($"Match found for {design}: {string.Join(", ", path)}");
            }
        }
        return count;
    }

    public (bool, List<string>) FindMatch(string target)
    {
        var queue = new Queue<(string, List<string>)>();
        var visited = new HashSet<string>();
        
        queue.Enqueue(("", new List<string>()));

        while (queue.Count > 0)
        {
            var (current, path) = queue.Dequeue();

            if (visited.Contains(current))
                continue;

            visited.Add(current);

            if (current == target)
                return (true, path);

            foreach (var pattern in Patterns)
            {
                if (target.StartsWith(current + pattern))
                {
                    string newString = current + pattern;
                    if (newString.Length <= target.Length)
                    {
                        var newPath = new List<string>(path) { pattern };
                        queue.Enqueue((newString, newPath));
                    }
                }
            }
        }

        return (false, new List<string>());
    }

    public void Print()
    {
        foreach (var pattern in Patterns)
        {
            Console.WriteLine(pattern);
        }
        foreach (var design in Designs)
        {
            Console.WriteLine(design);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var data = File.ReadAllText("input.txt");
        var matcher = new DesignMatcher(data);
        var count = matcher.FindMatches();
        Console.WriteLine($"Number of matches: {count}");
    }
}
