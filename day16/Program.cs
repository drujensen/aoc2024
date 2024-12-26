using Microsoft.VisualBasic;

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

    public bool Equals(Position obj)
    {
        return obj.X == X && obj.Y == Y && obj.Direction == Direction;
    }

    public int HashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode() ^ Direction.GetHashCode();
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
    public bool Add(Position position)
    {
        if (Positions.Any(pos => pos.Equals(position)))
        {
            return false;
        }
        Positions.Add(position);
        return true;
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
    public Stack<Path> Paths { get; set; }
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
        var solutions = new List<Path>();

        var Paths = new Stack<Path>();
        var path = new Path();
        path.Add(Start);
        Paths.Push(path);

        while (Paths.Any())
        {
            var currentPath = Paths.Pop();
            var currentPosition = currentPath.Positions.Last();

            if (currentPosition.X == End.X && currentPosition.Y == End.Y)
            {
                Console.WriteLine("Found the end");
                Console.WriteLine("Cost: " + currentPath.Positions.Sum(p => p.Cost));
                solutions.Add(currentPath);
                continue;
            }

            var nextPositions = GetNextPositions(currentPosition);

            foreach (var nextPosition in nextPositions)
            {
                var newPath = new Path();
                newPath.AddRange(currentPath.Positions);
                // Avoid going back to the previous position
                if (newPath.Add(nextPosition)) 
                {
                    Paths.Push(newPath);
                }
            }
        }
        if (!solutions.Any())
        {
            throw new Exception("No solution found");
        }
        return solutions.OrderBy(s => s.Positions.Sum(p => p.Cost)).First();
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
