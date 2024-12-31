class Computer
{
    public List<int> Instructions { get; set; } = new List<int>();
    public List<int> Output { get; set; }
    public int IP { get; set; }
    public int RA { get; set; }
    public int RB { get; set; }
    public int RC { get; set; }

    public Computer(String data)
    {
        var parts = data.Split("\n\n");
        var registers = parts[0].Split("\n");
        var instructions = parts[1].Split(": ");
        IP = 0;
        RA = int.Parse(registers[0].Split(": ")[1]);
        RB = int.Parse(registers[1].Split(": ")[1]);
        RC = int.Parse(registers[2].Split(": ")[1]);
        instructions[1].Split(",").ToList().ForEach(i => Instructions.Add(int.Parse(i)));
    }

    public void Run()
    {
        IP = 0;
        Output = new List<int>();
        while (IP < Instructions.Count - 1)
        {
            var opcode = Instructions[IP];
            IP += 1;
            var operand = Instructions[IP];
            int value = 0;

            switch (opcode)
            {
                case 0: // adv
                    value = (int)Math.Pow(2, GetOperandValue(operand));
                    RA = RA / value;
                    break;
                case 1: // bxl
                    RB ^= operand;
                    break;
                case 2: // bst
                    RB = GetOperandValue(operand) % 8;
                    break;
                case 3: // jnz
                    if (RA != 0)
                    {
                        IP = operand;
                        continue;
                    }
                    break;
                case 4: // bxc
                    RB ^= RC;
                    break;
                case 5: // out
                    value = GetOperandValue(operand) % 8;
                    Output.Add(value);
                    break;
                case 6: // bdv
                    value = (int)Math.Pow(2, GetOperandValue(operand));
                    RB = RA / value;
                    break;
                case 7: // cdv
                    value = (int)Math.Pow(2, GetOperandValue(operand));
                    RC = RA / value;
                    break;
            }

            // If not a jump instruction, move the instruction pointer
            if (opcode != 3)
            {
                IP += 1;
            }
            PrintRegisters();
        }
    }

    private int GetOperandValue(int operand)
    {
        switch (operand)
        {
            case 4: return RA;
            case 5: return RB;
            case 6: return RC;
            default: return operand; // Literal value for 0-3
        }
    }

    public void PrintRegisters()
    {
        Console.WriteLine($"IP: {IP}");
        Console.WriteLine($"RA: {RA}");
        Console.WriteLine($"RB: {RB}");
        Console.WriteLine($"RC: {RC}");
        for (int i = 0; i < Instructions.Count; i++)
        {
            Console.Write($"{Instructions[i]} ");
        }
        Console.WriteLine();
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        var data = File.ReadAllText("input.txt");
        var computer = new Computer(data);
        computer.Run();
    }
}
