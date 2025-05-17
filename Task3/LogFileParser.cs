using System.Text;
using Task3.LineParsers;

namespace Task3
{
    internal class LogFileParser
    {
        internal IEnumerable<ILineParser> Parsers { get; }

        internal LogFileParser(IEnumerable<ILineParser> parsers)
        {
            Parsers = parsers;
        }

        internal void LogFileParse(
            string inputFilePath,
            string outputFilePath,
            string errorFilePath)
        {
            using var reader = new StreamReader(inputFilePath, Encoding.GetEncoding("utf-8"));
            using var writer = new StreamWriter(outputFilePath);
            using var problemWriter = new StreamWriter(errorFilePath);

            string? line;

            while ((line = reader.ReadLine()) != null)
            {
                bool parsed = false;

                foreach (var parser in Parsers)
                {
                    if (parser.TryParse(line, out var output))
                    {
                        writer.WriteLine(output);
                        parsed = true;
                        break;
                    }
                }

                if (!parsed)
                {
                    problemWriter.WriteLine(line);
                }
            }

            Console.WriteLine("Обработка завершена.");
        }
    }
}
