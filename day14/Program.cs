class Robot
{
    public int PX { get; set; }
    public int PY { get; set; }
    public int VX { get; set; }
    public int VY { get; set; }

    public Robot(string data)
    {
        var parts = data.Split(' ');
        var position = parts[0].Split('=')[1].Split(',');
        var velocity = parts[1].Split('=')[1].Split(',');
        PX = int.Parse(position[0]);
        PY = int.Parse(position[1]);
        VX = int.Parse(velocity[0]);
        VY = int.Parse(velocity[1]);
    }

    public void Move(int width, int height)
    {
        PX += VX;
        PY += VY;
        if (PX < 0)
            PX = PX + width;
        if (PX >= width)
            PX = PX - width;
        if (PY < 0)
            PY = PY + height;
        if (PY >= height)
            PY = PY - height;
    }
    
}

class HeadQuarters
{
    private List<Robot> robots = new List<Robot>();
    private int width = 101;
    private int height = 103;

    public HeadQuarters(string[] lines)
    {
        foreach (var line in lines)
        {
            robots.Add(new Robot(line));
        }
    }

    public void Move(int count)
    {
        for (int i = 0; i < count; i++)
        {
            foreach (var robot in robots)
            {
                robot.Move(width, height);
            }
        }
    }

    public void Print()
    {
        Move(100);

        var quadrant1 = 0;
        var quadrant2 = 0;
        var quadrant3 = 0;
        var quadrant4 = 0;
        foreach (var robot in robots)
        {
            if (robot.PX < width / 2 && robot.PY < height / 2)
                quadrant1++;
            else if (robot.PX >= width / 2 && robot.PY < height / 2)
                quadrant2++;
            else if (robot.PX < width / 2 && robot.PY >= height / 2)
                quadrant3++;
            else if (robot.PX >= width / 2 && robot.PY >= height / 2)
                quadrant4++;
        }
        Console.WriteLine($"Q1: {quadrant1}, Q2: {quadrant2}, Q3: {quadrant3}, Q4: {quadrant4}");
        Console.WriteLine($"Total: {quadrant1 * quadrant2 * quadrant3 * quadrant4}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        var hq = new HeadQuarters(lines);
        hq.Print();
    }
}
