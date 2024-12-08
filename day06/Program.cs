class Map
{
    private List<List<char>> map = new List<List<char>>();

    private int playerX = 0;
    private int playerY = 0;
    private int playerDirection = 0;  // 0 - up, 1 - right, 2 - down, 3 - left

    public void AddRow(string line)
    {
        var row = new List<char>();
        foreach (var c in line)
        {
            row.Add(c);
        }
        map.Add(row);
    }

    public void FindPlayerPosition()
    {
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
    }
    
    public bool MovePlayer()
    {
        map[playerY][playerX] = 'X';
        switch (playerDirection)
        {
            case 0:
                playerY--;
                if (!isPositionOnMap(playerY, playerX))
                {
                    return false;
                }
                map[playerY][playerX] = '^';
                break;
            case 1:
                playerX++;
                if (!isPositionOnMap(playerY, playerX))
                {
                    return false;
                }
                map[playerY][playerX] = '>';
                break;
            case 2:
                playerY++;
                if (!isPositionOnMap(playerY, playerX))
                {
                    return false;
                }
                map[playerY][playerX] = '+';
                break;
            case 3:
                playerX--;
                if (!isPositionOnMap(playerX, playerY))
                {
                    return false;
                }
                map[playerY][playerX] = '<';
                break;
        }
        
        RotatePlayerIfObstacle();

        return true;
    }

    public void RotatePlayerIfObstacle()
    {
        if (playerDirection == 0)
        {
            if (isPositionOnMap(playerY - 1, playerX) && map[playerY - 1][playerX] == '#')
            {
                playerDirection = 1;
            }
        }
        else if (playerDirection == 1)
        {
            if (isPositionOnMap(playerY, playerX + 1) && map[playerY][playerX + 1] == '#')
            {
                playerDirection = 2;
            }
        }
        else if (playerDirection == 2)
        {
            if (isPositionOnMap(playerY + 1, playerX) && map[playerY + 1][playerX] == '#')
            {
                playerDirection = 3;
            }
        }
        else if (playerDirection == 3)
        {
            if (isPositionOnMap(playerY, playerX - 1) && map[playerY][playerX - 1] == '#')
            {
                playerDirection = 0;
            }
        }
    }
    
    private bool isPositionOnMap(int x, int y)
    {
        return x >= 0 && x < map[0].Count && y >= 0 && y < map.Count;
    }

    public int CountVisitedFields()
    {
        int count = 0;
        for (int i = 0; i < map.Count; i++)
        {
            for (int j = 0; j < map[i].Count; j++)
            {
                if (map[i][j] == 'X')
                {
                    count++;
                }
            }
        }
        return count;
    }
}


class Program
{
    static void Main(string[] args)
    {
        var map = new Map();
        var lines = File.ReadAllLines("input.txt");

        foreach (var line in lines)
        {
            map.AddRow(line);
        }

        map.FindPlayerPosition();
        while (map.MovePlayer())
        {
        }
        int count = map.CountVisitedFields();

        Console.WriteLine($"Visited fields: {count}");
    }
}
