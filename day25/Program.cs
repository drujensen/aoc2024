class Key
{
    List<int> pattern = new List<int>() { 0, 0, 0, 0, 0 };

    public Key(string[] key)
    {
        for(int row = 1; row <= 5; row++)
        {
            for(int col = 0; col <= 4; col++)
            {
                if(key[row][col] == '#')
                {
                    pattern[col]++;
                }
            }
        }
    }

    public List<int> Pattern
    {
        get
        {
            return pattern;
        }
    }
}

class Door
{
    List<int> pattern = new List<int>() { 0, 0, 0, 0, 0 };

    public Door(string[] door)
    {
        for(int row = 5; row >= 1; row--)
        {
            for(int col = 0; col <= 4; col++)
            {
                if(door[row][col] == '#')
                {
                    pattern[col]++;
                }
            }
        }
    }

    public bool CanOpen(Key key)
    {
        bool canOpen = true;
        for(int i = 0; i < 5; i++)
        {
            if(key.Pattern[i] + pattern[i] > 5)
            {
                canOpen = false;
                break;
            }
        }
        return canOpen;
    }
}

class Room
{
    List<Key> keys = new List<Key>();
    List<Door> doors = new List<Door>();

    public Room(string[] data)
    {
        foreach(var item in data)
        {
            var parts = item.Split("\n");
            if (parts[0] == "#####")
            {
                var door = new Door(parts);
                doors.Add(door);
            }
            else
            {
                var key = new Key(parts);
                keys.Add(key);
            }
        }
    }

    public int CountKeysThatCanOpenDoor()
    {
        var count = 0;
        foreach(var door in doors)
        {
            foreach(var key in keys)
            {
                if(door.CanOpen(key))
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
        var data = File.ReadAllText("input.txt");
        var parts = data.Split("\n\n");
        var room = new Room(parts);
        Console.WriteLine(room.CountKeysThatCanOpenDoor());
    }
}
