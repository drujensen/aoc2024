class VisitedField
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Direction { get; set; }
}

class Map
{
    private List<string> _lines;
    private List<List<char>> map = new List<List<char>>();
    private List<VisitedField> visited = new List<VisitedField>();

    private int playerX = 0;
    private int playerY = 0;
    private int playerDirection = 0;  // 0 - up, 1 - right, 2 - down, 3 - left

    public Map(List<string> lines)
    {
        _lines = lines;
        reset();
    }

    private void reset()
    {
        map = new List<List<char>>();
        visited = new List<VisitedField>();
        foreach (var line in _lines)
        {
            addRow(line);
        }
        findPlayerPosition();
    }

    private void addRow(string line)
    {
        var row = new List<char>();
        foreach (var c in line)
        {
            row.Add(c);
        }
        map.Add(row);
    }

    private void findPlayerPosition()
    {
        playerDirection = 0;
        for (int i = 0; i < map.Count; i++)
        {
            for (int j = 0; j < map[i].Count; j++)
            {
                if (map[i][j] == '^')
                {
                    playerX = j;
                    playerY = i;
                    return;
                }
            }
        }
        throw new Exception("Player not found");
    }

    public int CountVisitedFields()
    {
        while (isPlayerOnMap())
        {
            movePlayer();
        }

        int count = 0;
        for (int i = 0; i < map.Count; i++)
        {
            for (int j = 0; j < map[i].Count; j++)
            {
                if (map[i][j] == '^' || map[i][j] == '+' || map[i][j] == '|' || map[i][j] == '-')
                {
                    count++;
                }
            }
        }
        return count;
    }

    public int CountPossibleObstructionsThatProduceEndlessLoops()
    {
        int count = 0;

        for (int i = 0; i < map.Count; i++)
        {
            for (int j = 0; j < map[i].Count; j++)
            {
                reset();
                if (playerY == i && playerX == j)
                {
                    Console.WriteLine($"HIT PLAYER AT {i}, {j}");
                    continue;
                }
                map[i][j] = 'O';
                while (isPlayerOnMap())
                {
                    if (isPlayerInEndlessLoop())
                    {
                        Console.WriteLine($"Endless loop at {i}, {j}");
                        Console.WriteLine($"Player at {playerY}, {playerX}");
                        printMap();
                        count++;
                        break;
                    }

                    movePlayer();
                }


            }
        }
        return count;
    }

    private void printMap()
    {
        for (int i = 0; i < map.Count; i++)
        {
            for (int j = 0; j < map[i].Count; j++)
            {
                Console.Write(map[i][j]);
            }
            Console.WriteLine();
        }
    }

    private bool isPlayerOnMap()
    {
        return playerX >= 0 && playerX < map[0].Count && playerY >= 0 && playerY < map.Count;
    }

    private bool isPlayerInEndlessLoop()
    {
        foreach (var v in visited)
        {
            if (v.X == playerX && v.Y == playerY && v.Direction == playerDirection)
            {
                return true;
            }
        }
        visited.Add(new VisitedField() { X = playerX, Y = playerY, Direction = playerDirection });
        return false;
    }

    private void movePlayer()
    {
        if (rotatePlayerIfObstacle())
        {
            return;
        }

        switch (playerDirection)
        {
            case 0:
                playerY--;
                break;
            case 1:
                playerX++;
                break;
            case 2:
                playerY++;
                break;
            case 3:
                playerX--;
                break;
        }

        if (!isPlayerOnMap())
        {
            return;
        }

        if (map[playerY][playerX] != '+')
        {
            if (map[playerY][playerX] == '#' || map[playerY][playerX] == 'O')
            {
                throw new Exception("Player is in a wall");
            }
            map[playerY][playerX] = playerDirection % 2 == 0 ? '|' : '-';
        }
    }

    private bool rotatePlayerIfObstacle()
    {
        if (playerDirection == 0 && isPositionOnMap(playerY - 1, playerX) && (map[playerY - 1][playerX] == '#' || map[playerY - 1][playerX] == 'O'))
        {
            map[playerY][playerX] = '+';
            playerDirection = 1;
            return true;
        }
        else if (playerDirection == 1 && isPositionOnMap(playerY, playerX + 1) && (map[playerY][playerX + 1] == '#' || map[playerY][playerX + 1] == 'O'))
        {
            map[playerY][playerX] = '+';
            playerDirection = 2;
            return true;
        }
        else if (playerDirection == 2 && isPositionOnMap(playerY + 1, playerX) && (map[playerY + 1][playerX] == '#' || map[playerY + 1][playerX] == 'O'))
        {
            map[playerY][playerX] = '+';
            playerDirection = 3;
            return true;
        }
        else if (playerDirection == 3 && isPositionOnMap(playerY, playerX - 1) && (map[playerY][playerX - 1] == '#' || map[playerY][playerX - 1] == 'O'))
        {
            map[playerY][playerX] = '+';
            playerDirection = 0;
            return true;
        }
        return false;
    }

    private bool isPositionOnMap(int y, int x)
    {
        return y >= 0 && y < map.Count && x >= 0 && x < map[0].Count;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");

        var map = new Map(lines.ToArray().ToList());

        int count = map.CountVisitedFields();

        Console.WriteLine($"Visited fields: {count}");

        count = map.CountPossibleObstructionsThatProduceEndlessLoops();

        Console.WriteLine($"Possible Obstructions: {count}");

    }
}
