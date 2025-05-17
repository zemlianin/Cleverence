using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task3.FormatParser;

namespace Task3.Tests
{
    [TestClass]
    public class LineParserFormatWithMethodTests
    {
        private readonly LineParserFormatWithMethod parser = new();

        [DataTestMethod]
        [DataRow(
            "2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'",
            true,
            "10-03-2025\t15:14:51.5882\tINFO\tMobileComputer.GetDeviceId\tКод устройства: '@MINDEO-M40-D-410244015546'")]
        [DataRow(
            "2025-99-99 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'",
            false,
            "")]
        [DataRow(
            "2025-03-10 15:14:51.5882| UNRECOGNIZED|11|MobileComputer.GetDeviceId| Some message",
            false,
            "")]
        [DataRow(
            "this is not a valid log line at all",
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
