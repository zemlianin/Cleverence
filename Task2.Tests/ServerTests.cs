using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Task1.Tests
{
    [TestClass]
    public class ServerTests
    {
        [TestInitialize]
        public void ResetCount()
        {
            Server.AddToCount(-Server.GetCount());
        }

        [TestMethod]
        public void TestSingleThreadedAddAndGet()
        {
            Server.AddToCount(5);
            Server.AddToCount(3);

            var result = Server.GetCount();

            Assert.AreEqual(8, result);
        }

        [TestMethod]
        public void TestParallelReads()
        {
            Server.AddToCount(42);

            var results = new int[10];
            Parallel.For(0, 10, i =>
            {
                results[i] = Server.GetCount();
            });

            foreach (var val in results)
            {
                Assert.AreEqual(42, val);
            }
        }

        [TestMethod]
        public void TestConcurrentWritesAreSummedCorrectly()
        {
            var tasks = 100;
            Parallel.For(0, tasks, i =>
            {
                Server.AddToCount(1);
            });

            var final = Server.GetCount();

            Assert.AreEqual(tasks, final);
        }
    }
}
