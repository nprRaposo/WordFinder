using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordFinderExercise.Test
{
    [TestClass]
    public class WordFinderTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var dictionary = new string[] { "chill", "wind", "snow", "cold" };
            var src = new string[] { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" };

            var wordFinder = new WordFinder(dictionary);
            var result = wordFinder.Find(src);

            Assert.AreEqual(1, 1);
        }
    }
}
