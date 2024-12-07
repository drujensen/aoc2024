class Program
{
    static void Main(string[] args)
    {
        string path = Path.Combine(AppContext.BaseDirectory, "input.txt");
        string[] lines = File.ReadAllLines(path);
        
        List<int> lista = [];
        List<int> listb = [];

        foreach (string line in lines)
        {
            string[] values = line.Split("   ");
            lista.Add(int.Parse(values[0].Trim()));
            listb.Add(int.Parse(values[1].Trim()));
        }

        lista.Sort();
        listb.Sort();

        int sum = 0;
        for (int i = 0; i < lista.Count; i++)
        {
            sum += Math.Abs(lista[i] - listb[i]);
        }

        Console.WriteLine($"Total: {sum}");

        // we need to loop through lista and the count of similar values in listb
        // then we multiple the number by the count and add that to a total sum
        // then we output the total sum
        sum = 0;
        for (int i = 0; i < lista.Count; i++)
        {
            int count = listb.Count(x => x == lista[i]);
            sum += lista[i] * count;
        }
        
        Console.WriteLine($"Total: {sum}");
    }
}
