class Prize
{
    public int X { get; }
    public int Y { get; }

    public Prize(string data)
    {
        var parts = data.Split(':');
        var tmp = parts[1].Split(',');
        X = int.Parse(tmp[0].Split('=')[1]);
        Y = int.Parse(tmp[1].Split('=')[1]);
    }

}

class Button
{
    public int X { get; }
    public int Y { get; }

    public Button(string data)
    {
        var parts = data.Split(':');
        var tmp = parts[1].Split(',');
        X = int.Parse(tmp[0].Split('+')[1]);
        Y = int.Parse(tmp[1].Split('+')[1]);
    }

}

class ClawMachine
{
    private readonly Button aButton;
    private readonly Button bButton;
    private readonly Prize prize;
    private List<Tuple<int, int>> solutions = new List<Tuple<int, int>>();

    public ClawMachine(string[] lines)
    {
        aButton = new Button(lines[0]);
        bButton = new Button(lines[1]);
        prize = new Prize(lines[2]);
        FindAllPathsToPrize();
    }

    public void FindAllPathsToPrize()
    {
        int aPresses = 0;
        int bPresses = 0;
        while (aButton.X * aPresses < prize.X && aButton.Y * aPresses < prize.Y)
        {
            if (prize.X - (aButton.X * aPresses) % bButton.X == 0 && 
                prize.Y - (aButton.Y * aPresses) % bButton.Y == 0)
            {
                bPresses = prize.X - (aButton.X * aPresses) / bButton.X;
                Console.WriteLine($"Found A: {aPresses}, B: {bPresses}");
                solutions.Add(new Tuple<int, int>(aPresses, bPresses));
            }
            aPresses++;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<ClawMachine> clawMachines = new List<ClawMachine>();
        var data = File.ReadAllText("input.txt");
        var lines = data.Split("\n\n");
        foreach (var clawData in lines)
        {
            var claw = new ClawMachine(clawData.Split('\n'));
            clawMachines.Add(claw);
        }
    }
}
