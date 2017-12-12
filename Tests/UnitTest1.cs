using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab6;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }
        [TestMethod]
        public void TestScaling()
        {
            var m1 = new Marker(4, 4);
            var m2 = new Marker(8, 8);
            var m3 = new Marker(2, 8);

            var center = new Marker((m1.RenderedX + m2.RenderedX + m3.RenderedX) / 3, (m1.RenderedY + m2.RenderedY + m3.RenderedY) / 3);

            TestContext.WriteLine(center.ToString());
        }
    }
}
