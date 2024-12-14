class CalibrationEquation
{
    public Int64 Value { get; set; }
    public List<Int64> Operands { get; set; }

    public CalibrationEquation(string line)
    {
        Operands = new List<Int64>();
        var parts = line.Split(": ");
        Value = Int64.Parse(parts[0]);
        Operands = parts[1].Split(' ').Select(Int64.Parse).ToList();
    }

    // Try the combination of + and * operators for all the operands
    // and check if the result is equal to the value
    // return true if it is
    public bool IsValid()
    {
        List<Int64> totals = new List<Int64>();
        for (int i = 0; i < Operands.Count; i++)
        {
            if (i == 0)
            {
                totals.Add(Operands[i]);
            }
            else
            {
                List<Int64> newTotals = new List<Int64>();
                foreach (var total in totals)
                {
                    string concatenated = $"{total}{Operands[i]}";
                    if (Int64.TryParse(concatenated, out Int64 result))
                    {
                        newTotals.Add(result);
                    }
                    else
                    {
                        // Handle the error case
                        throw new ArgumentException($"The concatenated string '{concatenated}' is not a valid Int64.");
                    }
                    newTotals.Add(total + Operands[i]);
                    newTotals.Add(total * Operands[i]);
                }
                totals = newTotals.ToList();
            }
        }

        foreach (var total in totals)
        {
            if (total == Value)
            {
                Console.WriteLine($"Valid: Value: {Value}, Operands: {string.Join(", ", Operands)}");
                return true;
            }
        }

        return false;
    }
}


class Program
{
    static void Main(string[] args)
    {
        var calibrations = new List<CalibrationEquation>();
        var lines = File.ReadAllLines("input.txt");

        foreach (var line in lines)
        {
            calibrations.Add(new CalibrationEquation(line));
        }

        Int64 result = 0;
        foreach (var calibration in calibrations)
        {
            if (calibration.IsValid())
            {
                result += calibration.Value;
            }
        }
        Console.WriteLine($"Total: {result}");
    }
}
