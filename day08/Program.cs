public class Antinode
{
    public int X { get; }
    public int Y { get; }
    public char Frequency { get; }

    public Antinode(int x, int y, char frequency)
    {
        X = x;
        Y = y;
        Frequency = frequency;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Antinode other)
        {
            return X == other.X && Y == other.Y;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public Antinode Clone()
    {
        return new Antinode(X, Y, Frequency);
    }

    public Antinode? CreateMatchingAntinode(Antinode antenna, int width, int height)
    {
        // Calculate the distance between the two antennas
        // create antinode in the opposite direction at the 
        // same distance as the distance between the two antennas
        //
        // For example, if the distance between the two antennas is 3
        // and this antanna is at (1, 1) and the antenna being passed in
        // is at (4, 4) then the antinode should be at (7, 7)
        // the reverse direction would be (1 - 4, 1 - 4) = (-3, -3) 
        int x = antenna.X + (antenna.X - X);
        int y = antenna.Y + (antenna.Y - Y);
        if (x < 0 || x >= width || y < 0 || y >= height)
        {
            return null;
        }
        return new Antinode(x, y, Frequency);
    }

    public List<Antinode> CreateAllMatchingAntinodes(Antinode antenna, int width, int height)
    {
        var antinodes = new List<Antinode>();
        var previous = Clone();
        
        // The antenna becomes an antinode in this scenario
        antinodes.Add(previous);
        antinodes.Add(antenna);
       
        var antinode = previous.CreateMatchingAntinode(antenna, width, height);
        while (antinode != null)
        {
            antinodes.Add(antinode);
            previous = antenna.Clone();
            antenna = antinode.Clone();
            antinode = previous.CreateMatchingAntinode(antenna, width, height);
        }
        return antinodes;
    }
}

class Antenna : Antinode
{
    public Antenna(int x, int y, char frequency) : base(x, y, frequency)
    {
    }
}

class Program
{
    static void Main(string[] args)
    {
        var antennas = new List<Antenna>();

        var lines = File.ReadAllLines("input.txt");
        var width = lines[0].Length;
        var height = lines.Length;

        for (var y = 0; y < height; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (lines[y][x] == '.')
                {
                    continue;
                }
                antennas.Add(new Antenna(x, y, lines[y][x]));
            }
        }

        var antinodes = new HashSet<Antinode>();
        foreach (var antenna in antennas)
        {
            foreach (var otherAntenna in antennas)
            {
                if (antenna == otherAntenna)
                {
                    continue;
                }

                if (antenna.Frequency != otherAntenna.Frequency)
                {
                    continue;
                }

                var antinode = antenna.CreateMatchingAntinode(otherAntenna, width, height);
                if (antinode != null)
                {
                    antinodes.Add(antinode);
                }
            }
        }

        Console.WriteLine($"Total: {antinodes.Count}");
        
        var antinodes2 = new HashSet<Antinode>();
        foreach (var antenna in antennas)
        {
            foreach (var otherAntenna in antennas)
            {
                if (antenna == otherAntenna)
                {
                    continue;
                }

                if (antenna.Frequency != otherAntenna.Frequency)
                {
                    continue;
                }

                var matchingAntinodes = antenna.CreateAllMatchingAntinodes(otherAntenna, width, height);
                foreach (var antinode in matchingAntinodes)
                {
                    antinodes2.Add(antinode);
                }
            }
        }

        Console.WriteLine($"Total: {antinodes2.Count}");
    }
}