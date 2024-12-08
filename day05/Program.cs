using System.Globalization;

class Rule
{
    public int Key { get; set; }
    public int Value { get; set; }

    // The key page must print before the value page
    // Return true if the key doens't match the current page, ignore the rule
    // Return false if the value is before the key
    public bool CheckRule(List<int> pages, int position)
    {
        if (pages[position] == Key)
        {
            //Check all the pages before the current page
            //If the value page is before the key page, return false
            for (int i = 0; i < position; i++)
            {
                if (pages[i] == Value)
                {
                    return false;
                }
            }
        }
        
        return true;
    }
}

class Book
{
    private List<int> pages = new List<int>();

    public void Add(string page)
    {
        pages = page.Split(",").Select(int.Parse).ToList();
    }

    public int FindMiddlePageThatPassRules(List<Rule> rules)
    {
        var (_, passed) = PassedRules(rules); 
        if (passed)
        {
            return GetMiddlePage();
        }
        
        return 0;
    }

    public int FindMiddlePageThatFailedRulesButWereFixed(List<Rule> rules)
    {
        var (failedRule, passed) = PassedRules(rules); 
        if (!passed)
        {
            while (!passed)
            {
                var key = rules[failedRule].Key;
                var value = rules[failedRule].Value;
                SwapPages(key, value);
                (failedRule, passed) = PassedRules(rules);
            }
            return GetMiddlePage();
        }
        
        return 0;
    }

    private void SwapPages(int key, int value)
    {
        var keyIndex = pages.IndexOf(key);
        var valueIndex = pages.IndexOf(value);
        pages[keyIndex] = value;
        pages[valueIndex] = key;
    }
    
    private (int, bool) PassedRules(List<Rule> rules)
    {
        for (int i = 0; i < pages.Count; i++)
        {
            for (int j = 0; j < rules.Count; j++)
            {
                if (!rules[j].CheckRule(pages, i))
                {
                    return (j, false);
                }
            }
        }
        
        return (-1, true);
    }


    private int GetMiddlePage()
    {
        if (pages.Count % 2 == 1)
        {
            return pages[pages.Count / 2];
        }
        return (pages[pages.Count / 2] + pages[pages.Count / 2 - 1]) / 2;
    }
}

class Books
{
    private List<Book> books = new List<Book>();

    public void Add(string pages)
    {
        var book = new Book();
        book.Add(pages);
        books.Add(book);
    }

    public int SumMiddlePagesThatPassRules(List<Rule> rules)
    {
        var sum = 0;
        foreach (var book in books)
        {
            sum += book.FindMiddlePageThatPassRules(rules);
        }
        return sum;
    }

    public int SumMiddlePagesThatFaileAfterFixingPages(List<Rule> rules)
    {
        var sum = 0;
        foreach (var book in books)
        {
            sum += book.FindMiddlePageThatFailedRulesButWereFixed(rules);
        }
        return sum;
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Rule> rules = new List<Rule>();
        var books = new Books();

        var lines = File.ReadAllLines("input.txt");

        var lineIsBlank = false;
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                lineIsBlank = true;
                continue;
            }
            if (!lineIsBlank)
            {
                var parts = line.Split("|");
                var ruleKey = int.Parse(parts[0]);
                var ruleValue = int.Parse(parts[1]);
                rules.Add(new Rule { Key = ruleKey, Value = ruleValue });
            }
            else
            {
                books.Add(line);
            }
        }

        var result = books.SumMiddlePagesThatPassRules(rules);
        Console.WriteLine($"Result: {result}");

        var result2 = books.SumMiddlePagesThatFaileAfterFixingPages(rules);
        Console.WriteLine($"Result: {result2}");
    }
}
