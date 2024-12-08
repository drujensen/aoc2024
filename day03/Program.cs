using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        var total = 0;
        string content = File.ReadAllText("input.txt");

        var dos = Regex.Matches(content, @"do\(\)", RegexOptions.IgnoreCase).Select(m => m.Index).ToList();
        var donts = Regex.Matches(content, @"don't\(\)", RegexOptions.IgnoreCase).Select(m => m.Index).ToList();

        // Use Regex.Matches to find all matches in the content
        var matches = Regex.Matches(content, @"mul\((\d+),(\d+)\)");
        foreach (Match match in matches)
        {
            if (match.Success)
            {
                int a = int.Parse(match.Groups[1].Value);
                int b = int.Parse(match.Groups[2].Value);
                int position = match.Index;
                Console.WriteLine($"mul {a}, {b} at position {position}");

                //find the closest do() and don't() to the current mul() function
                int closestDo = dos.Where(x => x < position).DefaultIfEmpty(1).Max();
                int closestDont = donts.Where(x => x < position).DefaultIfEmpty(0).Max();
                
                Console.WriteLine($"closest do() at {closestDo}, closest don't() at {closestDont}");
                
                if (closestDo > closestDont)
                {
                    Console.WriteLine($"mul {a}, {b} is on");
                    total += a * b;
                }
                else
                {
                    Console.WriteLine($"mul {a}, {b} is off");
                } 
            }
        }

        Console.WriteLine($"mul instructions: {total}");
    }
}
