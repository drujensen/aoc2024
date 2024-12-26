enum Direction
{
    North,
    East,
    South,
    West
}


class Position
{
    public int X { get; set; }
    public int Y { get; set; }
    public Direction Direction { get; set; }
    public int Cost { get; set; }

   public override bool Equals(object obj)
   {
       if (obj is Position other)
       {
           return X == other.X && Y == other.Y && Direction == other.Direction;
       }
       return false;
   }

   public override int GetHashCode()
   {
       return HashCode.Combine(X, Y, Direction);
   }
}


class Path
{
    public List<Position> Positions { get; set; }

    public Path()
    {
        Positions = new List<Position>();
    }

    public void AddRange(IEnumerable<Position> positions)
    {
        foreach (var position in positions)
        {
            Add(position);
        }
    }
    public void Add(Position position)
    {
        Positions.Add(position);
    }

    public Position CurrentPosition()
    {
        return Positions.Last();
    }

    public int Cost()
    {
        return Positions.Sum(p => p.Cost);
    }
}

// # is a wall
// . is a path
// S is the start
// E is the end
// 90 degree turns are allowed but cost 1000 units
// movement in the same direction costs 1 unit
// find the shortest path from S to E
class Maze
{
    public List<List<char>> Map { get; set; }
    public Position? Start { get; set; }
    public Position? End { get; set; }

    public Maze(string[] lines)
    {
        Map = new List<List<char>>();

        foreach (var line in lines)
        {
            Map.Add(line.ToList());
        }

        findStart();
        findEnd();
    }

    private void findStart()
    {
        for (int y = 0; y < Map.Count; y++)
        {
            for (int x = 0; x < Map[y].Count; x++)
            {
                if (Map[y][x] == 'S')
                {
                    Start = new Position { X = x, Y = y, Direction = Direction.East };
                    return;
                }
            }
        }
    }

    private void findEnd()
    {
        for (int y = 0; y < Map.Count; y++)
        {
            for (int x = 0; x < Map[y].Count; x++)
            {
                if (Map[y][x] == 'E')
                {
                    End = new Position { X = x, Y = y, Direction = Direction.North };
                    return;
                }
            }
        }
    }

    public Path CheapestPath()
    {
        if (Start == null || End == null)
        {
            throw new Exception("Start or End not found");
        }

        var priorityQueue = new PriorityQueue<Path, int>();
        var path = new Path();
        path.Add(Start);
        priorityQueue.Enqueue(path, 0);

        var bestCosts = new Dictionary<Position, int>();

        while (priorityQueue.Count > 0)
        {
            var currentPath = priorityQueue.Dequeue();
            var currentPosition = currentPath.CurrentPosition();

            if (currentPosition.X == End.X && currentPosition.Y == End.Y)
            {
                return currentPath;
            }

            foreach (var nextPosition in GetNextPositions(currentPosition))
            {
                if (!currentPath.Positions.Any(p => p.Equals(nextPosition)))
                {
                    var newPath = new Path();
                    newPath.AddRange(currentPath.Positions);
                    newPath.Add(nextPosition);
                    int newCost = currentPath.Cost() + nextPosition.Cost;
                    if (!bestCosts.ContainsKey(nextPosition) || newCost < bestCosts[nextPosition])
                    {
                        bestCosts[nextPosition] = newCost;
                        if (!currentPath.Positions.Any(p => p.Equals(nextPosition)))
                        {
                           priorityQueue.Enqueue(newPath, newCost);
                        }
                    }
                }
            }
        }
        throw new Exception("No solution found");
    }

    private List<Position> GetNextPositions(Position currentPosition)
    {
        var nextPositions = new List<Position>();

        if (currentPosition.Direction == Direction.North)
        {
            if (currentPosition.Y - 1 >= 0 && Map[currentPosition.Y - 1][currentPosition.X] != '#')
            {
                nextPositions.Add(new Position { X = currentPosition.X, Y = currentPosition.Y - 1, Direction = Direction.North, Cost = 1 });
            }
            // 90 degree clockwise turn
            if (currentPosition.X + 1 < Map[currentPosition.Y].Count && Map[currentPosition.Y][currentPosition.X + 1] != '#')
            {
                nextPositions.Add(new Position { X = currentPosition.X, Y = currentPosition.Y, Direction = Direction.East, Cost = 1000 });
            }
            // 90 degree counter clockwise turn
            if (currentPosition.X - 1 >= 0 && Map[currentPosition.Y][currentPosition.X - 1] != '#')
            {
                nextPositions.Add(new Position { X = currentPosition.X, Y = currentPosition.Y, Direction = Direction.West, Cost = 1000 });
            }
        }
        else if (currentPosition.Direction == Direction.East)
        {
            if (currentPosition.X + 1 < Map[currentPosition.Y].Count && Map[currentPosition.Y][currentPosition.X + 1] != '#')
            {
                nextPositions.Add(new Position { X = currentPosition.X + 1, Y = currentPosition.Y, Direction = Direction.East, Cost = 1 });
            }

            if (currentPosition.Y + 1 < Map.Count && Map[currentPosition.Y + 1][currentPosition.X] != '#')
            {
                nextPositions.Add(new Position { X = currentPosition.X, Y = currentPosition.Y, Direction = Direction.South, Cost = 1000 });
            }

            if (currentPosition.Y - 1 >= 0 && Map[currentPosition.Y - 1][currentPosition.X] != '#')
            {
                nextPositions.Add(new Position { X = currentPosition.X, Y = currentPosition.Y, Direction = Direction.North, Cost = 1000 });
            }
        }
        else if (currentPosition.Direction == Direction.South)
        {
            if (currentPosition.Y + 1 < Map.Count && Map[currentPosition.Y + 1][currentPosition.X] != '#')
            {
                nextPositions.Add(new Position { X = currentPosition.X, Y = currentPosition.Y + 1, Direction = Direction.South, Cost = 1 });
            }
            if (currentPosition.X - 1 >= 0 && Map[currentPosition.Y][currentPosition.X - 1] != '#')
            {
                nextPositions.Add(new Position { X = currentPosition.X, Y = currentPosition.Y, Direction = Direction.West, Cost = 1000 });
            }
            if (currentPosition.X + 1 < Map[currentPosition.Y].Count && Map[currentPosition.Y][currentPosition.X + 1] != '#')
            {
                nextPositions.Add(new Position { X = currentPosition.X, Y = currentPosition.Y, Direction = Direction.East, Cost = 1000 });
            }
        }
        else if (currentPosition.Direction == Direction.West)
        {
            if (currentPosition.X - 1 >= 0 && Map[currentPosition.Y][currentPosition.X - 1] != '#')
            {
                nextPositions.Add(new Position { X = currentPosition.X - 1, Y = currentPosition.Y, Direction = Direction.West, Cost = 1 });
            }
            if (currentPosition.Y - 1 >= 0 && Map[currentPosition.Y - 1][currentPosition.X] != '#')
            {
                nextPositions.Add(new Position { X = currentPosition.X, Y = currentPosition.Y, Direction = Direction.North, Cost = 1000 });
            }
            if (currentPosition.Y + 1 < Map.Count && Map[currentPosition.Y + 1][currentPosition.X] != '#')
            {
                nextPositions.Add(new Position { X = currentPosition.X, Y = currentPosition.Y, Direction = Direction.South, Cost = 1000 });
            }
        }

        return nextPositions;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        var maze = new Maze(lines);
        var path = maze.CheapestPath();
        Console.WriteLine("Cheapest path cost: " + path.Positions.Sum(p => p.Cost));
    }
}
