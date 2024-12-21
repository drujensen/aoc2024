class Stones
{
    private List<Int64> stones;

    public Stones(string data)
    {
        stones = data.Split(' ').Select(Int64.Parse).ToList();
    }

    public void Blink()
    {
        List<Int64> newStones = new List<Int64>();

        for (int i = 0; i < stones.Count; i++)
        {
            var stone = stones[i];
            var stoneString = stone.ToString();
            var stoneLength = stoneString.Length;

            if (stone  == 0)
            {
               newStones.Add(1);
            }
            else if (stoneLength % 2 == 0)
            {
                // split stoneString in half and convert to Int64
                var half = stoneLength / 2;
                var firstHalf = Int64.Parse(stoneString.Substring(0, half));
                var secondHalf = Int64.Parse(stoneString.Substring(half, half));
                newStones.Add(firstHalf);
                newStones.Add(secondHalf);
            }
            else
            {
                newStones.Add(stone * 2024);
            }
        }
        stones = newStones;
    }

    public Int64 RecursiveBlink(Int64 stone, int count = 0)
    {
        var stoneString = stone.ToString();
        var stoneLength = stoneString.Length;

        if (count == 75)
        {
            return 1;
        }

        if (stone == 0)
        {
            return RecursiveBlink(1, count + 1);
        }
        else if (stoneLength % 2 == 0)
        {
            // split stoneString in half and convert to Int64
            var half = stoneLength / 2;
            var firstHalf = Int64.Parse(stoneString.Substring(0, half));
            var secondHalf = Int64.Parse(stoneString.Substring(half, half));
            return RecursiveBlink(firstHalf, count + 1) + RecursiveBlink(secondHalf, count + 1);
        }
        else
        {
            return RecursiveBlink(stone * 2024, count + 1);
        }
    }

    public void PrintRecursive()
    {
        Int64 sum = 0;
        foreach (var stone in stones)
        {
            sum += RecursiveBlink(stone);
        }
        Console.WriteLine($"Count: {sum}");
    }

    public void Print()
    {
        // Blink 25 times
        for (Int64 i = 0; i < 25; i++)
        {
            Blink();
        }
        Console.WriteLine($"Count: {stones.Count}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var data = File.ReadAllText("input.txt");
        var stones = new Stones(data);
        stones.Print();
        
        stones = new Stones(data);
        stones.PrintRecursive();
    }
}
