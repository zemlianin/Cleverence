using Task1;

class Program
{
    static void Main()
    {
        string original = "aaabbcccdde";
        var archiver = new Archiver();
        var fastArchiver = new FastArchiver();

        Console.WriteLine("===Ordinary Archiver===");
        RunExample(archiver, original);

        Console.WriteLine("===Fast Archiver===");
        RunExample(fastArchiver, original);
    }

    static void RunExample(IArchiver archiver, String original)
    {
        string compressed = archiver.Compress(original);
        string decompressed = archiver.Decompress(compressed);

        Console.WriteLine($"Исходная строка: {original}");
        Console.WriteLine($"Сжатая строка:   {compressed}");
        Console.WriteLine($"Восстановленная: {decompressed}");
    }
}
