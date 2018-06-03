using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordFinderExercise.Test
{
    [TestClass]
    public class WordFinderTest
    {
        [TestMethod]
        public void UsingTheDictionaryAndSourceOfTheExerciseTest()
        {
            var dictionary = new string[] { "chill", "wind", "snow", "cold" };
            var src = new string[] { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" };

            var wordFinder = new WordFinder(dictionary);
            var result = wordFinder.Find(src);

            Assert.AreEqual(result.Count, 3);
        }

        [TestMethod]
        public void UsingAMatrixOf19x19CharsTest()
        {
            var dictionary = new string[] { "mariposa", "lanza", "celtina", "lete", "juanita", "lechiguana", "yuyera", "caronte", "mosca", "cochinilla", "agavo", "marmorea", "coma" };
            var src = new string[] {"AECYMCCOCHINILLACMN",
                                    "AOLUVLAUENCCÓLGLCAR",
                                    "ADERNDRIATLOAIORORM",
                                    "ANCCHALZIIRSMTGIOIR",
                                    "RMHIREJAPETRSUSOAPA",
                                    "UMICARONTEOILELTPOE",
                                    "CNGOAJLIYHMIVDAALSA",
                                    "EAUUMUTJTOCOALNMVAS",
                                    "ELAOBAIENAEPEEZCOSA",
                                    "EANEMNPSRELNROANDTO",
                                    "DTAADIRIHMTASETOSAA",
                                    "NLNIATECCAINDROOAUM",
                                    "GMYGRANAAUNTERGTULO",
                                    "RRAEEIIEIAACANALGRS",
                                    "OPATHUDAGAVOAAUDTAC",
                                    "DINCIOUAHASLUACAOBA",
                                    "TSRMICOMAMARMOREARA",
                                    "AOTDNLRLLMILETEENEA",
                                    "BESYUYERANVEEHASERL"};

            var wordFinder = new WordFinder(dictionary);
            var result = wordFinder.Find(src);

            Assert.AreEqual(result.Count, dictionary.Count());
        }
    }
}
