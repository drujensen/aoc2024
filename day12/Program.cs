using System.Security.Cryptography.X509Certificates;

class Plot
{
    public char Type { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Sides { get; set; }
    public bool inRegion { get; set; }

    public Plot(char type, int x, int y, int sides)
    {
        this.Type = type;
        this.X = x;
        this.Y = y;
        this.Sides = sides;
        inRegion = false;
       
    }
        // Override hashcode and equals to use hashset
        public override int GetHashCode()
        {
            return X ^ Y;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Plot;
            if (other == null)
            {
                return false;
            }
            return X == other.X && Y == other.Y;
        }
}

class Region
{
    public HashSet<Plot> plots = new HashSet<Plot>();

    public void AddPlot(Plot plot)
    {
        plot.inRegion = true;
        plots.Add(plot);
    }

    public void Populate(Plot plot, List<List<Plot>> map)
    {
        Stack<Plot> stack = new Stack<Plot>();
        stack.Push(plot);
        while (stack.Count > 0)
        {
            var current = stack.Pop();
            AddPlot(current);

            if (current.X > 0 && map[current.Y][current.X - 1].Type == current.Type && !map[current.Y][current.X - 1].inRegion)
            {
                stack.Push(map[current.Y][current.X - 1]);
            }
            if (current.Y > 0 && map[current.Y - 1][current.X].Type == current.Type && !map[current.Y - 1][current.X].inRegion)
            {
                stack.Push(map[current.Y - 1][current.X]);
            }
            if (current.X < map[0].Count - 1 && map[current.Y][current.X + 1].Type == current.Type && !map[current.Y][current.X + 1].inRegion)
            {
                stack.Push(map[current.Y][current.X + 1]);
            }
            if (current.Y < map.Count - 1 && map[current.Y + 1][current.X].Type == current.Type && !map[current.Y + 1][current.X].inRegion)
            {
                stack.Push(map[current.Y + 1][current.X]);
            }
        }
    }

    public int Parameter()
    {
        var sum = 0;
        foreach (var plot in plots)
        {
            sum += plot.Sides;
        }
        return sum;
    }

    public int Area()
    {
        return plots.Count;
    }
}

class Map
{
    public List<List<Plot>> plots = new List<List<Plot>>();
    public List<Region> regions = new List<Region>();

    public Map(string[] lines)
    {
        for(int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var row = new List<Plot>();
            for(int j = 0; j < line.Length; j++)
            {
                var c = line[j];
                var sides = 0;
                if (j == 0 || (j > 0 && lines[i][j - 1] != c))
                {
                    sides++;
                }
                if (i == 0 || (i > 0 && lines[i - 1][j] != c))
                {
                    sides++;
                }
                if (j == line.Length - 1 || (j < line.Length - 1 && lines[i][j + 1] != c))
                {
                    sides++;
                }
                if (i == lines.Length - 1 || (i < lines.Length - 1 && lines[i + 1][j] != c))
                {
                    sides++;
                }
                var plot = new Plot(c, j, i, sides);
                row.Add(plot);
            }
            plots.Add(row);
        }
        FindRegions();
    }

    public void FindRegions()
    {
        foreach (var row in plots)
        {
            foreach (var plot in row)
            {
                if (plot.inRegion)
                {
                    continue;
                }

                var region = new Region();
                region.Populate(plot, plots);
                regions.Add(region);
            }
        }
    }

    public void PrintMap()
    {
        foreach (var row in plots)
        {
            foreach (var plot in row)
            {
                Console.Write(plot.Sides);
            }
            Console.WriteLine();
        }
    }

    public void Print()
    {
        var total = 0;
        foreach (var region in regions)
        {
            Console.WriteLine($"Area: {region.Area()}, Parameter: {region.Parameter()}");
            total += region.Area() * region.Parameter();
        }
        Console.WriteLine($"Total: {total}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        var map = new Map(lines);
        map.PrintMap();
        map.Print();
    }
}
