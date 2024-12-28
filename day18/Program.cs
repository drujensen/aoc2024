class Memory
{
    public List<List<char>> Data { get; set; }
    public Memory(string[] data)
    {
        Data = new List<List<char>>();
        for( int i = 0; i < 71; i++)
        {
            Data.Add(new List<char>());
            for (int j = 0; j < 71; j++)
            {
                Data[i].Add('.');
            }
        }
        foreach (var line in data)
        {
           var parts = line.Split(",");
           var x = int.Parse(parts[0]);
           var y = int.Parse(parts[1]);
           Data[x][y] = '#';
        }
    }

    public void Print()
    {
        foreach (var row in Data)
        {
            Console.WriteLine(string.Join("", row));
        }
    }

    // Using dykstra's algorithm to find the shortest path
    // starting at (0, 0) and ending at (70, 70)
    public bool FindPath()
    {
        var visited = new bool[71, 71];
        var distance = new int[71, 71];
        var queue = new Queue<(int, int)>();
        queue.Enqueue((0, 0));
        visited[0, 0] = true;
        distance[0, 0] = 0;
        while (queue.Count > 0)
        {
            var (x, y) = queue.Dequeue();
            if (x == 70 && y == 70)
            {
                Console.WriteLine(distance[x, y]);
                return true;
            }
            foreach (var (dx, dy) in new List<(int, int)> { (0, 1), (0, -1), (1, 0), (-1, 0) })
            {
                var nx = x + dx;
                var ny = y + dy;
                if (nx < 0 || nx >= 71 || ny < 0 || ny >= 71 || visited[nx, ny] || Data[nx][ny] == '#')
                {
                    continue;
                }
                visited[nx, ny] = true;
                distance[nx, ny] = distance[x, y] + 1;
                queue.Enqueue((nx, ny));
            }
        }
        return false;
    }
}
class Program
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        var memory = new Memory(lines[0..1023]);
        memory.Print();
        memory.FindPath();

        for (int i =1024; i < lines.Length; i++)
        {
            memory = new Memory(lines[0..i]);
            if (!memory.FindPath())
            {
                Console.WriteLine($"No path found at {i} coordinates at {lines[i]}");
                break;
            }
        }
    }
}
