class Point
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }
}

class Trail
{
    public List<Point>? Points { get; set; }
}

class TrailPoint : Point
{
    public List<Trail>? Trails { get; set; }

    public void findTrails(int[,] map)
    {
        Trails = new List<Trail>();
        var stack = new Stack<Point>();
        for (var i=0; i <= 9; i++)
        {
            if (i == 0)
            {
                stack.Push(new Point { X = X, Y = Y, Z = Z });
            } else
            {
                var newStack = new Stack<Point>();
               // Find all points north, east, south and west of the current point
               // where the Z value is equal to i
               // If found, add to a new stack
                while (stack.Count > 0)
                {
                    var point = stack.Pop();
                    // North
                    if (point.Y > 0 && map[point.Y - 1, point.X] == i)
                    {
                        newStack.Push(new Point { X = point.X, Y = point.Y - 1, Z = i });
                    }
                    // East
                    if (point.X < map.GetLength(1) - 1 && map[point.Y, point.X + 1] == i)
                    {
                        newStack.Push(new Point { X = point.X + 1, Y = point.Y, Z = i });
                    }
                    // South
                    if (point.Y < map.GetLength(0) - 1 && map[point.Y + 1, point.X] == i)
                    {
                        newStack.Push(new Point { X = point.X, Y = point.Y + 1, Z = i });
                    }
                    // West
                    if (point.X > 0 && map[point.Y, point.X - 1] == i)
                    {
                        newStack.Push(new Point { X = point.X - 1, Y = point.Y, Z = i });
                    }
                }
                // Swap the stacks
                stack = newStack;
            }
        }

        // Create trails
        while (stack.Count > 0)
        {
            var point = stack.Pop();
            var trail = new Trail { Points = new List<Point> { point } };
            Trails.Add(trail);
        }
    }
}

class TopographicalMap
{
    private List<TrailPoint> trailPoints;
    private int[,] map;
    private int width;
    private int length;

    public TopographicalMap(string[] lines)
    {
        width = lines[0].Length;
        length = lines.Length;
        map = new int[length, width];
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                map[i, j] = lines[i][j] - '0';
            }
        }

        // Find all trail points
        trailPoints = new List<TrailPoint>();
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == 0)
                {
                    var trailPoint = new TrailPoint { X = j, Y = i, Z = 0 };
                    trailPoints.Add(trailPoint);
                }
            }
        }

        // Find all trails
        foreach (var trailPoint in trailPoints)
        {
            trailPoint.findTrails(map);
        }
    }

    public void PrintTotals()
    {
        var sum = 0;
        foreach (var trailPoint in trailPoints)
        {
            sum += trailPoint.Trails.Count;
        }
        Console.WriteLine($"Sum of trails: {sum}");
    }
}


class Program
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        var map = new TopographicalMap(lines);
        map.PrintTotals();
    }
}
