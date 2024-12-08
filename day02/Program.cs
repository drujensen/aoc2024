
using System.Net.Mail;
using System.Xml.XPath;

class Report(int[] levels)
{
    public List<int> Levels { get; set; } = levels.ToList();

    public bool IsSafe()
    {
        var isSafe = IsInRange() && (IsIncreasing() || IsDecreasing());
        // return isSafe;
        if (isSafe) {
            return true;
        }

        var tmpLevels = Levels.ToList() ;

        for (int i = 0; i < Levels.Count; i++)
        {
            Levels.RemoveAt(i);
            if (IsInRange() && (IsIncreasing() || IsDecreasing())) {
                return true;
            }
            Levels = tmpLevels.ToList();
        }
        return false;
    }

    public bool IsInRange()
    {
        for (int i = 0; i < Levels.Count-1; i++)
        {
            var diff = Math.Abs(Levels[i] - Levels[i + 1]);
            if (diff <= 0 || diff > 3)
            {
                return false;
            }
        }
        return true;
    }

    public bool IsIncreasing()
    {
        for (int i = 0; i < Levels.Count - 1; i++)
        {
            if (Levels[i] > Levels[i + 1])
            {
                return false;
            }
        }
        return true;
    }

    public bool IsDecreasing()
    {
        for (int i = 0; i < Levels.Count - 1; i++)
        {
            if (Levels[i] < Levels[i + 1])
            {
                return false;
            }
        }
        return true;
    }
}


class Program
{
    static void Main(string[] args)
    {
        var reports = new List<Report>();
        var lines = File.ReadAllLines("input.txt");

        foreach (var line in lines)
        {
            var report = new Report(line.Split(' ').Select(int.Parse).ToArray());
            reports.Add(report);
        }

        var count = 0;
        foreach (var report in reports)
        {
            if (report.IsSafe())
            {
                count++;
            }
        }

        Console.WriteLine($"Safe reports: {count}");

    }
}
