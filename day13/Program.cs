class Prize
{
    public Int64 X { get; }
    public Int64 Y { get; }

    public Prize(string data)
    {
        var parts = data.Split(':');
        var tmp = parts[1].Split(',');
        X = Int64.Parse(tmp[0].Split('=')[1]) + 10000000000000;
        Y = Int64.Parse(tmp[1].Split('=')[1]) + 10000000000000;
    }

}

class Button
{
    public Int64 X { get; }
    public Int64 Y { get; }

    public Button(string data)
    {
        var parts = data.Split(':');
        var tmp = parts[1].Split(',');
        X = int.Parse(tmp[0].Split('+')[1]);
        Y = int.Parse(tmp[1].Split('+')[1]);
    }

}

class ComboPresses
{
    public Int64 APresses { get; }
    public Int64 BPresses { get; }

    public ComboPresses(Int64 aPresses, Int64 bPresses)
    {
        APresses = aPresses;
        BPresses = bPresses;
    }

    public Int64 Cost()
    {
        return (APresses * 3) + (BPresses * 1);
    }
}

class ClawMachine
{
    private readonly Button aButton;
    private readonly Button bButton;
    private readonly Prize prize;
    private List<ComboPresses> solutions = new List<ComboPresses>();

    public ClawMachine(string[] lines)
    {
        aButton = new Button(lines[0]);
        bButton = new Button(lines[1]);
        prize = new Prize(lines[2]);
        FindAllPathsToPrize();
    }

    public void FindAllPathsToPrize()
    {
        Int64 aPresses = 0;
        Int64 bPresses = 0;
        while (aButton.X * aPresses <= prize.X && aButton.Y * aPresses <= prize.Y)
        {
            var modx = (prize.X - (aButton.X * aPresses)) % bButton.X;
            var mody = (prize.Y - (aButton.Y * aPresses)) % bButton.Y;
            if (modx == 0 && mody == 0)
            {
                var divX = (prize.X - (aButton.X * aPresses)) / bButton.X;
                var divY = (prize.Y - (aButton.Y * aPresses)) / bButton.Y;
                if (divX == divY)
                {
                    bPresses = divX;
                    Console.WriteLine($"Found A: {aPresses}, B: {bPresses}");
                    solutions.Add(new ComboPresses(aPresses, bPresses));
                }
            }
            aPresses++;
        }
    }

    public ComboPresses? GetBestSolution()
    {
        if (solutions.Any() == false)
        {
            return null;
        }
        return solutions.MinBy(x => x.Cost());
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

        Int64 tokens = 0;
        foreach (var claw in clawMachines)
        {
            var bestSolution = claw.GetBestSolution();
            if (bestSolution != null)
            {
                tokens += bestSolution.Cost();
            }
        }
        Console.WriteLine($"Tokens: {tokens}");
    }
}
