using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task3.FormatParser;

namespace Task3.Tests
{
    [TestClass]
    public class LineParserFormatWithoutMethodTests
    {
        private readonly LineParserFormatWithoutMethod parser = new();

        [DataTestMethod]
        [DataRow(
            "10.03.2025 15:14:49.523 INFORMATION  Версия программы: '3.4.0.48729'",
            true,
            "10-03-2025\t15:14:49.523\tINFO\tDEFAULT\tВерсия программы: '3.4.0.48729'")]
        [DataRow(
            "10.99.2025 15:14:49.523 INFORMATION  Версия программы: '3.4.0.48729'",
            false,
            "")]
        [DataRow(
            "10.03.2025 15:14:49.523 UNKNOWNLEVEL  Some message",
            false,
            "")]
        [DataRow(
            "some completely invalid text that doesn't match the pattern",
            false,
            "")]
        public void TryParse_VariousInputs_ParsesAccordingToExpectations(string input, bool expectedResult, string expectedOutput)
        {
            var result = parser.TryParse(input, out var formatted);

            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(expectedOutput, formatted);
        }
    }
}
