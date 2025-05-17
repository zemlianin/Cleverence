using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using Task3.FormatParser;
using Task3.LineParsers;

namespace Task3.Tests
{
    [TestClass]
    public class LogFileParserTests
    {
        private string _tempInputFile = "test_input.txt";
        private string _tempOutputFile = "test_output.txt";
        private string _tempErrorFile = "test_errors.txt";

        private readonly List<ILineParser> parsers = new()
        {
            new LineParserFormatWithMethod(),
            new LineParserFormatWithoutMethod()
        };

        [TestInitialize]
        public void Setup()
        {
            var lines = new[]
            {
                "2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'",

                "10.03.2025 15:14:49.523 INFORMATION  Версия программы: '3.4.0.48729'",

                "This line does not match anything"
            };

            File.WriteAllLines(_tempInputFile, lines, Encoding.UTF8);
        }

        [TestCleanup]
        public void Cleanup()
        {
            File.Delete(_tempInputFile);
            File.Delete(_tempOutputFile);
            File.Delete(_tempErrorFile);
        }

        [TestMethod]
        public void LogFileParse_ParsesValidLinesAndSavesInvalidToErrorFile()
        {
            var parser = new LogFileParser(parsers);
            parser.LogFileParse(_tempInputFile, _tempOutputFile, _tempErrorFile);

            var outputLines = File.ReadAllLines(_tempOutputFile, Encoding.UTF8);
            var errorLines = File.ReadAllLines(_tempErrorFile, Encoding.UTF8);

            Assert.AreEqual(2, outputLines.Length);
            Assert.AreEqual(1, errorLines.Length);

            StringAssert.Contains(outputLines[0], "MobileComputer.GetDeviceId");
            StringAssert.Contains(outputLines[1], "DEFAULT");
            StringAssert.Contains(errorLines[0], "does not match");
        }
    }
}
