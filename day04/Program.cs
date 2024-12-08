using System.Security.Cryptography.X509Certificates;

class Matrix
{
    public List<List<char>> matrix;
    public Matrix()
    {
        matrix = new List<List<char>>();
    }

    public void Add(string line)
    {
        matrix.Add(line.ToList());
    }

    public int findWordCount(string word)
    {
        int count = 0;
        for (int i = 0; i < matrix.Count; i++)
        {
            for (int j = 0; j < matrix[i].Count; j++)
            {
                if (matrix[i][j] == word[0])
                {
                    if (CheckWord(i + 1, j, word.Substring(1), "down"))
                    {
                        Console.WriteLine($"Word found down at {i + 1}, {j}");
                        count++;
                    }
                    if (CheckWord(i - 1, j, word.Substring(1), "up"))
                    {
                        Console.WriteLine($"Word found up at {i - 1}, {j}");
                        count++;
                    }
                    if (CheckWord(i, j + 1, word.Substring(1), "right"))
                    {
                        Console.WriteLine($"Word found right at {i}, {j + 1}");
                        count++;
                    }
                    if ( CheckWord(i, j - 1, word.Substring(1), "left"))
                    {
                        Console.WriteLine($"Word found left at {i}, {j + 1}");
                        count++;
                    }
                    if (CheckWord(i + 1, j + 1, word.Substring(1), "downright"))
                    {
                        Console.WriteLine($"Word found downright at {i + 1}, {j + 1}");
                        count++;
                    }
                    if (CheckWord(i - 1, j + 1, word.Substring(1), "upright"))
                    {
                        Console.WriteLine($"Word found upright at {i - 1}, {j + 1}");
                        count++;
                    }
                    if (CheckWord(i + 1, j - 1, word.Substring(1), "downleft"))
                    {
                        Console.WriteLine($"Word found downleft at {i + 1}, {j - 1}");
                        count++;
                    }
                    if (CheckWord(i - 1, j - 1, word.Substring(1), "upleft"))
                    {
                        Console.WriteLine($"Word found upleft at {i - 1}, {j - 1}");
                        count++;
                    }
                }
            }
        }
        return count;
    }

    public bool CheckWord(int i, int j, string word, string direction)
    {
        if (i < 0 || i >= matrix.Count || j < 0 || j >= matrix[i].Count)
        {
            return false;
        }
        char c = matrix[i][j];
        if (c != word[0])
        {
            return false;
        }

        if (word.Length == 1)
        {
            return true;
        }

        switch (direction)
        {
            case "down":
                if (CheckWord(i + 1, j, word.Substring(1), "down"))
                {
                    return true;
                }
                break;
            case "up":
                if (CheckWord(i - 1, j, word.Substring(1), "up"))
                {
                    return true;
                }
                break;
            case "right":
                if (CheckWord(i, j + 1, word.Substring(1), "right"))
                {
                    return true;
                }
                break;
            case "left":
                if (CheckWord(i, j - 1, word.Substring(1), "left"))
                {
                    return true;
                }
                break;
            case "downright":
                if (CheckWord(i + 1, j + 1, word.Substring(1), "downright"))
                {
                    return true;
                }
                break;
            case "upright":
                if (CheckWord(i - 1, j + 1, word.Substring(1), "upright"))
                {
                    return true;
                }
                break;
            case "downleft":
                if (CheckWord(i + 1, j - 1, word.Substring(1), "downleft"))
                {
                    return true;
                }
                break;
            case "upleft":
                if (CheckWord(i - 1, j - 1, word.Substring(1), "upleft"))
                {
                    return true;
                }
                break;
        }

        return false;
    }

    public int findMASasX()
    {
        int count = 0;

        for (int i = 0; i < matrix.Count; i++)
        {
            for (int j = 0; j < matrix[i].Count; j++)
            {
                if (matrix[i][j] == 'A')
                {
                    var atLeastTwo = 0;
                    
                    if (CheckMAS(i - 1, j - 1, "M") &&
                        CheckMAS(i + 1, j + 1, "S"))
                    {
                        Console.WriteLine($"MAS found upleft to downright at {i}, {j}");
                        atLeastTwo++;
                    }
                    if (CheckMAS(i - 1, j + 1, "M") &&
                        CheckMAS(i + 1, j - 1, "S"))
                    {
                        Console.WriteLine($"MAS found upright to downleft at {i}, {j}");
                        atLeastTwo++;
                    }
                    if (CheckMAS(i - 1, j - 1, "S") &&
                        CheckMAS(i + 1, j + 1, "M"))
                    {
                        Console.WriteLine($"MAS found downright to upleft at {i}, {j}");
                        atLeastTwo++;
                    }
                    if (CheckMAS(i - 1, j + 1, "S") &&
                        CheckMAS(i + 1, j - 1, "M"))
                    {
                        Console.WriteLine($"MAS found downleft to upright at {i}, {j}");
                        atLeastTwo++;
                    }
                    
                    if (atLeastTwo >= 2)
                    {
                        count++;
                    }
                }
            }
        }
        return count;
    }

    public bool CheckMAS(int i, int j, string letter)
    {
        if (i < 0 || i >= matrix.Count || j < 0 || j >= matrix[i].Count)
        {
            return false;
        }
        char c = matrix[i][j];
        if (c != letter[0])
        {
            return false;
        }
        return true;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var matrix = new Matrix();

        var lines = File.ReadAllLines("input.txt");
        foreach (var line in lines)
        {
            matrix.Add(line);
        }

        int count = matrix.findWordCount("XMAS");
        Console.WriteLine($"Word found {count} times");

        int count2 = matrix.findMASasX();
        Console.WriteLine($"Word found {count2} times");
    }
}
