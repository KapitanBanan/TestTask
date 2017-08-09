using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;

namespace TestTask.Tests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestFileTreeMethod()
        {
            //arrange
            string sourse = @"E:\Test";
            Queue<string> expected = new Queue<string>();
            expected.Enqueue(@"E:\Test\Number.jpg");
            expected.Enqueue(@"E:\Test\Test2\Eagle.JPG");

            //act
            ThreadFileTree testThread = new ThreadFileTree(sourse);

            //assert
            Assert.AreEqual(expected, testThread.Roll);
        }
        [TestMethod]
        public void TestFileHashMethod()
        {
            //arrange
            Queue<string> test = new Queue<string>();
            test.Enqueue(@"E:\Test3\Number.jpg");
            List<string> expected = new List<string>();
            expected[0] = "AccessError";

            //act
            ThreadFileHash testThread = new ThreadFileHash(test);

            //assert
            Assert.AreEqual(expected, testThread.Roll);
        }
    }
}
