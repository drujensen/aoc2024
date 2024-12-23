class Map
{
    private List<List<char>> map = new List<List<char>>();
    private int X;
    private int Y;

    public Map(string input)
    {
        var lines = input.Split("\n");
        foreach (var line in lines)
        {
            map.Add(line.ToCharArray().ToList());
        }
        FindSubmarine();
    }

    private void FindSubmarine()
    {
        for (int y = 0; y < map.Count; y++)
        {
            for (int x = 0; x < map[y].Count; x++)
            {
                if (map[y][x] == '@')
                {
                    X = x;
                    Y = y;
                }
            }
        }
    }

    public void Move(char move)
    {
        switch (move)
        {
            case '^':
                MoveUp();
                break;
            case 'v':
                MoveDown();
                break;
            case '<':
                MoveLeft();
                break;
            case '>':
                MoveRight();
                break;
        }
    }

    private void MoveLeft()
    {
        if (map[Y][X - 1] == '#')
        {
            return;
        }
        if (map[Y][X - 1] == '.')
        {
            map[Y][X] = '.';
            map[Y][X - 1] = '@';
            X--;
            return;
        }
        if (map[Y][X - 1] == 'O')
        {
            for (int x = X - 1; x >= 0; x--)
            {
                if (map[Y][x] == '#')
                {
                    return;
                }
                if (map[Y][x] == 'O')
                {
                    continue;
                }
                if (map[Y][x] == '.')
                {
                    map[Y][X] = '.';
                    map[Y][X - 1] = '@';
                    X--;
                    map[Y][x] = 'O';
                    return;
                }
            }

        }
    }

    private void MoveRight()
    {
        if (map[Y][X + 1] == '#')
        {
            return;
        }
        if (map[Y][X + 1] == '.')
        {
            map[Y][X] = '.';
            map[Y][X + 1] = '@';
            X++;
            return;
        }
        if (map[Y][X + 1] == 'O')
        {
            for (int x = X + 1; x < map[Y].Count; x++)
            {
                if (map[Y][x] == '#')
                {
                    return;
                }
                if (map[Y][x] == 'O')
                {
                    continue;
                }
                if (map[Y][x] == '.')
                {
                    map[Y][X] = '.';
                    map[Y][X + 1] = '@';
                    X++;
                    map[Y][x] = 'O';
                    return;
                }
            }
        }
    }

    private void MoveUp()
    {
        if (map[Y - 1][X] == '#')
        {
            return;
        }
        if (map[Y - 1][X] == '.')
        {
            map[Y][X] = '.';
            map[Y - 1][X] = '@';
            Y--;
            return;
        }
        if (map[Y - 1][X] == 'O')
        {
            for (int y = Y - 1; y >= 0; y--)
            {
                if (map[y][X] == '#')
                {
                    return;
                }
                if (map[y][X] == 'O')
                {
                    continue;
                }
                if (map[y][X] == '.')
                {
                    map[Y][X] = '.';
                    map[Y - 1][X] = '@';
                    Y--;
                    map[y][X] = 'O';
                    return;
                }
            }
        }
    }

    private void MoveDown()
    {
        if (map[Y + 1][X] == '#')
        {
            return;
        }
        if (map[Y + 1][X] == '.')
        {
            map[Y][X] = '.';
            map[Y + 1][X] = '@';
            Y++;
            return;
        }
        if (map[Y + 1][X] == 'O')
        {
            for (int y = Y + 1; y < map.Count; y++)
            {
                if (map[y][X] == '#')
                {
                    return;
                }
                if (map[y][X] == 'O')
                {
                    continue;
                }
                if (map[y][X] == '.')
                {
                    map[Y][X] = '.';
                    map[Y + 1][X] = '@';
                    Y++;
                    map[y][X] = 'O';
                    return;
                }
            }
        }
    }

    public void Print()
    {
        var total = 0;
        for (int y = 0; y < map.Count; y++)
        {
            for (int x = 0; x < map[y].Count; x++)
            {
                if (map[y][x] == 'O')
                {
                    total += y * 100 + x;
                }
            }
        }
        Console.WriteLine($"Total: {total}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var text = File.ReadAllText("input.txt");
        var parts = text.Split("\n\n");

        var map = new Map(parts[0]);
        var moves = parts[1].Replace("\n", "").ToCharArray();
        foreach (var move in moves)
        {
            map.Move(move);
        }
        map.Print();
    }
}
