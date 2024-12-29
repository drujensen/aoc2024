class Wire
{
    public string Name { get; set; }
    public int? Value { get; set; }

    public Wire()
    {
    }

    public Wire(string data)
    {
        var parts = data.Split(": ");
        Name = parts[0];
        if (int.TryParse(parts[1], out int value))
        {
            Value = value;
        }
    }
}

class Gate
{
    public string Operation { get; set; }
    public Wire Input1 { get; set; }
    public Wire Input2 { get; set; }
    public Wire Output { get; set; }

    public Gate(List<Wire> wires, string data)
    {
        var parts = data.Split(" ");

        // Helper method to get or create a wire
        Input1 = GetOrCreateWire(wires, parts[0]);
        Operation = parts[1];
        Input2 = GetOrCreateWire(wires, parts[2]);
        Output = GetOrCreateWire(wires, parts[4]);
    }

    private Wire GetOrCreateWire(List<Wire> wires, string wireName)
    {
        // Check if the wire already exists
        var existingWire = wires.FirstOrDefault(w => w.Name == wireName);
        if (existingWire != null)
        {
            return existingWire;
        }
        // If not, create a new wire and add it to the list
        var newWire = new Wire { Name = wireName };
        wires.Add(newWire);
        return newWire;
    }
}



class BreadBoard
{
    public List<Wire> Wires { get; set; }
    public List<Gate> Gates { get; set; }

    public BreadBoard(string data)
    {
        var parts = data.Split("\n\n");
        var inputs = parts[0].Split("\n");
        var gates = parts[1].Split("\n");

        Wires = inputs.Select(i => new Wire(i)).ToList();
        Gates = gates.Where(f => f != null && f.Length > 3).Select(g => new Gate(Wires, g)).ToList();
    }

    public void Cycle()
    {
        while (true)
        {
            var changed = false;
            foreach (var gate in Gates)
            {
                if (gate.Operation == "AND")
                {
                    if (gate.Input1.Value.HasValue && gate.Input2.Value.HasValue && !gate.Output.Value.HasValue)
                    {
                        gate.Output.Value = gate.Input1.Value & gate.Input2.Value;
                        changed = true;
                    }
                }
                else if (gate.Operation == "OR")
                {
                    if (gate.Input1.Value.HasValue && gate.Input2.Value.HasValue && !gate.Output.Value.HasValue)
                    {
                        gate.Output.Value = gate.Input1.Value | gate.Input2.Value;
                        changed = true;
                    }
                }
                else if (gate.Operation == "XOR")
                {
                    if (gate.Input1.Value.HasValue && gate.Input2.Value.HasValue && !gate.Output.Value.HasValue)
                    {
                        gate.Output.Value = gate.Input1.Value ^ gate.Input2.Value;
                        changed = true;
                    }
                }
                else
                {
                    throw new Exception("Unknown operation");
                }
            }
            if (!changed)
            {
                break;
            }
        }
    }

    // Combining the bits from all wires starting with z produces the binary number.
    // Converting this number to decimal gives the final answer.
    // Least significant bit is Z0 to Most significant bitcd i Z45.
    public Int64 GetZValue()
    {
        List<int> bits = new List<int>();
        foreach (var wire in Wires.OrderByDescending(w => w.Name))
        {
            if (wire.Name.StartsWith("z"))
            {
                Console.WriteLine($"{wire.Name}: {wire.Value}");
                bits.Add(wire.Value.Value);
            }
        }
        Console.WriteLine(string.Join("", bits));
        return Convert.ToInt64(string.Join("", bits), 2);
    }
}


class Program
{
    static void Main(string[] args)
    {
        var data = File.ReadAllText("input.txt");
        var breadBoard = new BreadBoard(data);
        breadBoard.Cycle();
        Console.WriteLine($"Z value: {breadBoard.GetZValue()}");
    }
}
