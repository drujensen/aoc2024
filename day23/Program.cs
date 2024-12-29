class ComputerNetwork
{
    private Dictionary<string, List<string>> routeTable = new Dictionary<string, List<string>>();

    public ComputerNetwork(string[] lines)
    {
        foreach (var line in lines)
        {
            var parts = line.Split('-');
            AddRoute(parts[0], parts[1]);
            AddRoute(parts[1], parts[0]);
        }
    }

    public void AddRoute(string from, string to)
    {
        if (!routeTable.ContainsKey(from))
        {
            routeTable[from] = new List<string>();
        }
        routeTable[from].Add(to);
    }

    public void FindNetworkOf3()
    {
        HashSet<string> networkOf3 = new HashSet<string>();

        foreach (var (key, value) in routeTable)
        {
            foreach (var v in value)
            {
                if (routeTable.ContainsKey(v))
                {
                    foreach (var v2 in routeTable[v])
                    {
                        if (routeTable.ContainsKey(v2))
                        {
                            foreach (var v3 in routeTable[v2])
                            {
                                if (v3 == key)
                                {
                                    List<string> networkOf3List = new List<string> { key, v, v2 };
                                    networkOf3List.Sort();
                                    var str = string.Join("-", networkOf3List);
                                    networkOf3.Add(str);
                                }
                            }
                        }
                    }
                }
            }
        }

        var filtered = networkOf3.Where(x => x.StartsWith("t") || x.Contains("-t")).ToList();
        foreach (var network in filtered)
        {
            Console.WriteLine(network);
        }
        Console.WriteLine($"Total: {filtered.Count}");
    }

    public string FindPassword()
    {
        List<string> networkList = new List<string>();
        foreach (var (key, value) in routeTable)
        {
            networkList.Add(key);
        }

        networkList.Sort();
        var password = string.Join(",", networkList);
        return password;
    }

    public void Print()
    {
        foreach (var (key, value) in routeTable)
        {
            Console.WriteLine($"{key} -> {string.Join(", ", value)}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        var computerNetwork = new ComputerNetwork(lines);
        computerNetwork.FindNetworkOf3();
        Console.WriteLine(computerNetwork.FindPassword());
    }
}
