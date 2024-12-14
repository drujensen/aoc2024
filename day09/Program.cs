class Block
{
    public int FileId { get; set; }
    public bool IsUsed { get; set; }
    public int Length { get; set; }
}

class DiskMap
{
    private List<Block> blocks = new List<Block>();

    public DiskMap(string data)
    {
        var fileId = 0;
        for(int i = 0; i< data.Length; i++)
        {
            bool used = i % 2 == 0;
            int count;
            int.TryParse(data[i].ToString(), out count);
            for(int j = 0; j < count; j++)
            {
                if (used)
                {
                    blocks.Add(new Block { FileId = fileId, IsUsed = used, Length = count });
                }
                else
                {
                    blocks.Add(new Block { FileId = -1, IsUsed = used, Length = count });
                }
            }
            if (used)
            {
                fileId++;
            }
        }
        Console.WriteLine($"FileId: {fileId}");
    }

    public void Defragment()
    {
        int i = 0;
        int j = blocks.Count - 1;
        while(i < j)
        {
            if(blocks[i].IsUsed)
            {
                i++;
            }
            else if(!blocks[j].IsUsed)
            {
                j--;
            }
            else
            {
                var temp = blocks[i];
                blocks[i] = blocks[j];
                blocks[j] = temp;
                i++;
                j--;
            }
        }
    }
    
    public void DefragmentFullFiles()
    {
        int i = 0;
        int j = blocks.Count - 1;
        while(i < j)
        {
            if(!blocks[j].IsUsed)
            {
                j -= blocks[j].Length;
                continue;
            }

            int j_length = blocks[j].Length;
            while(i < j)
            {
                if(blocks[i].IsUsed ||  blocks[i].Length < blocks[j].Length)
                {
                    i += 1;
                    continue;
                }
                swapFiles(i, j, blocks[i].Length - blocks[j].Length);
                break;
            }
            j -= j_length;
        }
    }

    private void swapFiles(int i, int j, int newSize)
    {
        while(j > 0 && blocks[j].IsUsed)
        {
            var temp = blocks[i];
            blocks[i] = blocks[j];
            blocks[j] = temp;
            i += 1;
            j -= 1;
        }
        for (int k = 0; k < newSize; k++)
        {
            blocks[i].Length = newSize;
            i += 1;
        }
    }

    public void Print()
    {
        for(int i = 0; i < blocks.Count; i++)
        {
            Console.Write(blocks[i].IsUsed ? "*" : ".");
        }
        Console.WriteLine();
    }   

    public Int64 CheckSum()
    {
        Int64 sum = 0;
        for(int i = 0; i < blocks.Count; i++)
        {
            if(blocks[i].IsUsed)
            {
                sum += i * blocks[i].FileId;
            }
        }
        return sum;
    }
}


class Program
{
    static void Main(string[] args)
    {
        var data = File.ReadAllText("input.txt");
        var diskMap = new DiskMap(data);
        diskMap.DefragmentFullFiles();
        diskMap.Print();
        Console.WriteLine($"Checksum: {diskMap.CheckSum()}");
    }
}
