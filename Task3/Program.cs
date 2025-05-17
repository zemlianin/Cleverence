#define USE_REFLECTION

using System.Reflection;
using Task3.LineParsers;

namespace Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputPath;
            string outputPath;
            string problemPath;

            if (args.Length != 3)
            {
                inputPath = Path.Combine("..", "..", "..", "Logs", "input.txt");
                outputPath = Path.Combine("..", "..", "..", "Logs", "output.txt");
                problemPath = Path.Combine("..", "..", "..", "Logs", "problems.txt");
            }
            else
            {
                inputPath = args[0];
                outputPath = args[1];
                problemPath = args[2];
            }

#if USE_REFLECTION
            var parsers = DiscoverParsers();
#else
            var parsers = new List<ILineParser>
            {
                new LineParserFormatWithMethod(),
                new LineParserFormatWithoutMethod()
            };
#endif

            var fileParser = new LogFileParser(parsers);
            fileParser.LogFileParse(inputPath, outputPath, problemPath);
        }

#if USE_REFLECTION
        private static List<ILineParser> DiscoverParsers()
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => !t.IsAbstract && typeof(ILineParser).IsAssignableFrom(t))
                .Select(t => (ILineParser)Activator.CreateInstance(t)!)
                .ToList();
        }
#endif
    }
}
