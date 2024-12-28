class NumberKeyPad
{
    private Dictionary<char, Dictionary<char, string>> _shortestPaths;

    public NumberKeyPad()
    {
        // Initialize shortest paths
        _shortestPaths = new Dictionary<char, Dictionary<char, string>>();
        _shortestPaths.Add('A', new Dictionary<char, string> { { 'A', "" },
                                                               { '0', "<" },
                                                               { '1', "^<<" },
                                                               { '2', "^<" },
                                                               { '3', "^" },
                                                               { '4', "^^<<" },
                                                               { '5', "^^<" },
                                                               { '6', "^^" },
                                                               { '7', "^^^<<" },
                                                               { '8', "^^^<" },
                                                               { '9', "^^^" } });

        _shortestPaths.Add('0', new Dictionary<char, string> { { 'A', ">" },
                                                               { '0', "" },
                                                               { '1', "^<" },
                                                               { '2', "^" },
                                                               { '3', "^>" },
                                                               { '4', "^^<" },
                                                               { '5', "^^" },
                                                               { '6', "^^>" },
                                                               { '7', "^^^<" },
                                                               { '8', "^^^" },
                                                               { '9', "^^^>" } });

        _shortestPaths.Add('1', new Dictionary<char, string> { { 'A', ">>v" },
                                                               { '0', ">v" },
                                                               { '1', "" },
                                                               { '2', ">" },
                                                               { '3', ">>" },
                                                               { '4', "^" },
                                                               { '5', "^>" },
                                                               { '6', "^>>" },
                                                               { '7', "^^" },
                                                               { '8', "^^>" },
                                                               { '9', "^^>>" } });
        _shortestPaths.Add('2', new Dictionary<char, string> { { 'A', ">v" },
                                                               { '0', "v" },
                                                               { '1', "<" },
                                                               { '2', "" },
                                                               { '3', ">" },
                                                               { '4', "^<" },
                                                               { '5', "^" },
                                                               { '6', "^>" },
                                                               { '7', "^^<" },
                                                               { '8', "^^" },
                                                               { '9', "^^>" } });
        _shortestPaths.Add('3', new Dictionary<char, string> { { 'A', "v" },
                                                               { '0', "<v" },
                                                               { '1', "<<" },
                                                               { '2', "<" },
                                                               { '3', "" },
                                                               { '4', "^<<" },
                                                               { '5', "^<" },
                                                               { '6', "^" },
                                                               { '7', "^^<<" },
                                                               { '8', "^^<" },
                                                               { '9', "^^" } });
        _shortestPaths.Add('4', new Dictionary<char, string> { { 'A', ">>vv" },
                                                               { '0', ">vv" },
                                                               { '1', "v" },
                                                               { '2', "v>" },
                                                               { '3', "v>>" },
                                                               { '4', "" },
                                                               { '5', ">" },
                                                               { '6', ">>" },
                                                               { '7', "^" },
                                                               { '8', "^>" },
                                                               { '9', "^>>" } });
        _shortestPaths.Add('5', new Dictionary<char, string> { { 'A', ">vv" },
                                                               { '0', "vv" },
                                                               { '1', "v<" },
                                                               { '2', "v" },
                                                               { '3', "v>" },
                                                               { '4', "<" },
                                                               { '5', "" },
                                                               { '6', ">" },
                                                               { '7', "^<" },
                                                               { '8', "^" },
                                                               { '9', "^>" } });
        _shortestPaths.Add('6', new Dictionary<char, string> { { 'A', "vv" },
                                                               { '0', "vv<" },
                                                               { '1', "v<<" },
                                                               { '2', "v<" },
                                                               { '3', "v" },
                                                               { '4', "<<" },
                                                               { '5', "<" },
                                                               { '6', "" },
                                                               { '7', "^<<" },
                                                               { '8', "^<" },
                                                               { '9', "^" } });
        _shortestPaths.Add('7', new Dictionary<char, string> { { 'A', ">>vvv" },
                                                               { '0', ">vvv" },
                                                               { '1', "vv" },
                                                               { '2', "vv>" },
                                                               { '3', "vv>>" },
                                                               { '4', "v" },
                                                               { '5', "v>" },
                                                               { '6', "v>>" },
                                                               { '7', "" },
                                                               { '8', ">" },
                                                               { '9', ">>" } });
        _shortestPaths.Add('8', new Dictionary<char, string> { { 'A', ">vvv" },
                                                               { '0', "vvv" },
                                                               { '1', "vv<" },
                                                               { '2', "vv" },
                                                               { '3', "vv>" },
                                                               { '4', "v<" },
                                                               { '5', "v" },
                                                               { '6', "v>" },
                                                               { '7', "<" },
                                                               { '8', "" },
                                                               { '9', ">" } });
        _shortestPaths.Add('9', new Dictionary<char, string> { { 'A', "vvv" },
                                                               { '0', "vvv<" },
                                                               { '1', "vv<<" },
                                                               { '2', "vv<" },
                                                               { '3', "vv" },
                                                               { '4', "v<<" },
                                                               { '5', "v<" },
                                                               { '6', "v" },
                                                               { '7', "<<" },
                                                               { '8', "<" },
                                                               { '9', "" } });
    }

    public string GetShortestPath(char current, char next)
    {
        return _shortestPaths[current][next] + 'A';
    }

}

class RobotKeyPad
{
    private Dictionary<char, Dictionary<char, string>> _shortestPaths;

    public RobotKeyPad()
    {
        _shortestPaths = new Dictionary<char, Dictionary<char, string>>();
        _shortestPaths.Add('A', new Dictionary<char, string> { { 'A', "" },
                                                               { '^', "<" },
                                                               { '<', "v<<" },
                                                               { 'v', "v<" },
                                                               { '>', "v" } });
        _shortestPaths.Add('^', new Dictionary<char, string> { { 'A', ">" },
                                                               { '^', "" },
                                                               { '<', "v<" },
                                                               { 'v', "v" },
                                                               { '>', "v>" } });
        _shortestPaths.Add('<', new Dictionary<char, string> { { 'A', ">>^" },
                                                               { '^', ">^" },
                                                               { '<', "" },
                                                               { 'v', ">" },
                                                               { '>', ">>" } });
        _shortestPaths.Add('v', new Dictionary<char, string> { { 'A', ">^" },
                                                               { '^', "^" },
                                                               { '<', "<" },
                                                               { 'v', "" },
                                                               { '>', ">" } });
        _shortestPaths.Add('>', new Dictionary<char, string> { { 'A', "^" },
                                                               { '^', "^<" },
                                                               { '<', "<<" },
                                                               { 'v', "<" },
                                                               { '>', "" } });
    }

    public string GetShortestPath(char current, char next)
    {
        return _shortestPaths[current][next] + 'A';
    }
}


class Program
{
    static void Main(string[] args)
    {
        var results = new Dictionary<string, string>();
        var lines = File.ReadAllLines("input.txt");
        var numberKeyPad = new NumberKeyPad();
        var robot1 = new RobotKeyPad();
        var robot2 = new RobotKeyPad();
        foreach (var line in lines)
        {
            var current = 'A';
            var path = "";
            foreach (var next in line)
            {
                path += numberKeyPad.GetShortestPath(current, next);
                current = next;
            }

            current = 'A';
            var robot1Path = "";
            foreach (var next in path)
            {
                robot1Path += robot1.GetShortestPath(current, next);
                current = next;
            }

            current = 'A';
            var robot2Path = "";
            foreach (var next in robot1Path)
            {
                robot2Path += robot2.GetShortestPath(current, next);
                current = next;
            }

            results.Add(line, robot2Path);
        }

        // loop through the dictionary
        // convert the line to a number.  strip out the A's
        // muliply the number by the length of the path
        // add them all together
        var total = 0;
        foreach (var result in results)
        {
            var number = int.Parse(result.Key.Replace("A", ""));
            total += number * result.Value.Length;
            Console.WriteLine($"{result.Key}: {result.Value}");
            Console.WriteLine($"{number} * {result.Value.Length}");
        }

        Console.WriteLine($"Total: {total}");
    }

}
