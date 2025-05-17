using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Task1.Tests
{
    [TestClass]
    public class ArchiverPerformanceTests
    {
        public TestContext TestContext { get; set; }

        private readonly IArchiver slow = new Archiver();
        private readonly IArchiver fast = new FastArchiver();

        [DataTestMethod]
        [DataRow("aaabbcccdde")]
        [DataRow("aabbccddeeffgggghhhh")]
        [DataRow("a", DisplayName = "SingleCharacter")]
        [DataRow("abcde", DisplayName = "UniqueCharacters")]
        [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", DisplayName = "LongRepeating")]
        [DataRow("aabbaaabbbbccccddddaa", DisplayName = "MixedRepeats")]
        public void Compress_Performance(string input)
        {
            var sw1 = Stopwatch.StartNew();
            var result1 = slow.Compress(input);
            sw1.Stop();

            var sw2 = Stopwatch.StartNew();
            var result2 = fast.Compress(input);
            sw2.Stop();

            TestContext.WriteLine($"Input: {input}");
            TestContext.WriteLine($"Archiver    : {sw1.ElapsedTicks} ticks");
            TestContext.WriteLine($"FastArchiver: {sw2.ElapsedTicks} ticks");

            Assert.AreEqual(slow.Decompress(result1), input, "Slow decompression mismatch");
            Assert.AreEqual(fast.Decompress(result2), input, "Fast decompression mismatch");
        }

        [DataTestMethod]
        [DataRow("a3b2c3d2e")]
        [DataRow("a10")]
        [DataRow("a2b2c2d2e2f2")]
        [DataRow("a")]
        [DataRow("a100")]
        [DataRow("x1y2z3")]
        public void Decompress_Performance(string input)
        {
            var sw1 = Stopwatch.StartNew();
            var result1 = slow.Decompress(input);
            sw1.Stop();

            var sw2 = Stopwatch.StartNew();
            var result2 = fast.Decompress(input);
            sw2.Stop();

            TestContext.WriteLine($"Input: {input}");
            TestContext.WriteLine($"Archiver    : {sw1.ElapsedTicks} ticks");
            TestContext.WriteLine($"FastArchiver: {sw2.ElapsedTicks} ticks");

            Assert.AreEqual(result1, result2, "Decompression outputs differ");
        }
    }
}
