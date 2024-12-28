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
        HashSet<HashSet<string>> networkOf3 = new HashSet<HashSet<string>>();

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
                                    HashSet<string> networkOf3Set = new HashSet<string> { v3, v2, v };
                                    networkOf3.Add(networkOf3Set);
                                }
                            }
                        }
                    }
                }
            }
        }

        var filteredNetworkOf3WhereAnyCommputerNameStartsWithTheLettert = networkOf3.Where(x => x.Any(y => y.StartsWith("t"))).ToList();
        foreach (var network in filteredNetworkOf3WhereAnyCommputerNameStartsWithTheLettert)
        {
            Console.WriteLine(string.Join(", ", network));
        }
        Console.WriteLine($"Total: {filteredNetworkOf3WhereAnyCommputerNameStartsWithTheLettert.Count}");
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
    }
}
