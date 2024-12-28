class SecretCalculator
{
    /*
    In particular, each buyer's secret number evolves into the next secret number in the sequence via the following process:

    Calculate the result of multiplying the secret number by 64. Then, mix this result into the secret number. Finally, prune the secret number.
    Calculate the result of dividing the secret number by 32. Round the result down to the nearest integer. Then, mix this result into the secret number. Finally, prune the secret number.
    Calculate the result of multiplying the secret number by 2048. Then, mix this result into the secret number. Finally, prune the secret number.
    Each step of the above process involves mixing and pruning:

    To mix a value into the secret number, calculate the bitwise XOR of the given value and the secret number. Then, the secret number becomes the result of that operation. (If the secret number is 42 and you were to mix 15 into the secret number, the secret number would become 37.)
    To prune the secret number, calculate the value of the secret number modulo 16777216. Then, the secret number becomes the result of that operation. (If the secret number is 100000000 and you were to prune the secret number, the secret number would become 16113920.)

    Run the above process 2000 times.
    */
    public Int64 Calculate(string secretString)
    {
        var secret = Int64.Parse(secretString);
        for (int i = 0; i < 2000; i++)
        {
            secret = prune(mix((secret * 64), secret));
            secret = prune(mix((secret / 32), secret));
            secret = prune(mix((secret * 2048), secret));
        }
        return secret;
    }

    private Int64 mix(Int64 a, Int64 b)
    {
        return a ^ b;
    }

    private Int64 prune(Int64 a)
    {
        return a % 16777216;
    }
}
class Program
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("input.txt");
        var secretCalculator = new SecretCalculator();
        
        Int64 total = 0;
        foreach (var line in lines)
        {
            total += secretCalculator.Calculate(line);
        }
        Console.WriteLine($"Total: {total}");
    }
}
