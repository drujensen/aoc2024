class Maze
{
    private char[,] _maze;
    private int _startX;
    private int _startY;
    private int _endX;
    private int _endY;

    public Maze(string[] lines)
    {
        _maze = new char[lines.Length, lines[0].Length];
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                _maze[i, j] = lines[i][j];
            }
        }
        findStartEnd();
    }

    private void findStartEnd()
    {
        for (int i = 0; i < _maze.GetLength(0); i++)
        {
            for (int j = 0; j < _maze.GetLength(1); j++)
            {
                if (_maze[i, j] == 'S')
                {
                    _startX = i;
                    _startY = j;
                }
                if (_maze[i, j] == 'E')
                {
                    _endX = i;
                    _endY = j;
                }
            }
        }
    }

    public int FindAllCheatsSave100Picos()
    {
        var visited = new HashSet<(int, int)>();
        var solutions = new List<List<string>>();
        var path = FindPath();
        var longestPath = path.Count;
        var originalMaze = (char[,])_maze.Clone();
        for (int i = 0; i < _maze.GetLength(0); i++)
        {
            for (int j = 0; j < _maze.GetLength(1); j++)
            {
                foreach (var (dx, dy) in new[] { (0, 1), (0, -1), (1, 0), (-1, 0) })
                {
                    var newX = i + dx;
                    var newY = j + dy;
                    if (newX >= 0 && newX < _maze.GetLength(0) && newY >= 0 && newY < _maze.GetLength(1))
                    {
                        if (visited.Contains((newX, newY)))
                            continue;
                        visited.Add((newX, newY));
                        _maze = (char[,])originalMaze.Clone();
                        _maze[i, j] = '.';
                        _maze[newX, newY] = '.';
                        var newPath = FindPath();
                        if (newPath.Count < longestPath - 100)
                        {
                            solutions.Add(newPath);
                        }
                    }
                }
            }
        }

        return solutions.Count;
    }

    public List<string> FindPath()
    {
        var queue = new Queue<(int, int, List<string>)>();
        var visited = new HashSet<(int, int)>();

        queue.Enqueue((_startX, _startY, new List<string>()));

        while (queue.Count > 0)
        {
            var (x, y, path) = queue.Dequeue();

            if (visited.Contains((x, y)))
                continue;

            visited.Add((x, y));

            if (x == _endX && y == _endY)
                return path;

            foreach (var (dx, dy, direction) in new[] { (0, 1, "R"), (0, -1, "L"), (1, 0, "D"), (-1, 0, "U") })
            {
                var newX = x + dx;
                var newY = y + dy;
                if (newX >= 0 && newX < _maze.GetLength(0) && newY >= 0 && newY < _maze.GetLength(1) && _maze[newX, newY] != '#')
                {
                    var newPath = new List<string>(path) { direction };
                    queue.Enqueue((newX, newY, newPath));
                }
            }
        }

        return new List<string>();
    }

    public void Print()
    {
        for (int i = 0; i < _maze.GetLength(0); i++)
        {
            for (int j = 0; j < _maze.GetLength(1); j++)
            {
                Console.Write(_maze[i, j]);
            }
            Console.WriteLine();
        }
        Console.WriteLine($"Start: ({_startX}, {_startY})");
        Console.WriteLine($"End: ({_endX}, {_endY})");

        var path = FindPath();
        Console.WriteLine($"Length: {path.Count}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        var maze = new Maze(lines);
        var result = maze.FindAllCheatsSave100Picos();
        Console.WriteLine($"Result: {result}");
    }
}
