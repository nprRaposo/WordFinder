using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordFinderExercise
{
    public class WordFinder
    {
        private IEnumerable<string> WordDictionary { get; set; }

        public WordFinder(IEnumerable<string> dictionary)
        {
            this.WordDictionary = dictionary.OrderBy(w => w.ToLower());
        }

        public IList<string> Find(IEnumerable<string> src)
        {
            var columns = src.ElementAt(0).Length;
            var sb = new StringBuilder();
            var matrix = this.GetMatrixFromIEnumerable(src);
            var rows = matrix.Count();
            var horizontalString = string.Empty;
            var wordsFound = new List<string>();
            var characters = this.WordDictionary.Select(w => w[0]).Distinct();
            IEnumerable<string> verticalStrings = null;
            IEnumerable<string> wordsStartingWithCharacter = null;
            var wordsToFind = 0;

            foreach (var characterToFind in characters)
            {
                wordsStartingWithCharacter = this.GetWordsStartingWith(characterToFind, this.WordDictionary);
                wordsToFind = wordsStartingWithCharacter.Count();

                for (int r = 0; r < rows; r++)
                {
                    var listOfOcurrences = this.GetOcurrencesOf(characterToFind, matrix[r]);

                    if (listOfOcurrences.Count() == 0)
                    {
                        continue;
                    }

                    horizontalString = sb.Append(matrix[r]).ToString();
                    sb.Clear();
                    verticalStrings = this.GetVerticalString(matrix, r, listOfOcurrences, rows);

                    foreach (var word in wordsStartingWithCharacter)
                    {
                        if (horizontalString.Contains(word) || verticalStrings.Any(vs => vs.Contains(word)))
                        {
                            if (!wordsFound.Contains(word))
                            {
                                wordsFound.Add(word);
                                wordsToFind--;
                            }
                        }
                    }

                    if (wordsToFind == 0)
                    {
                        break;
                    }
                }
            }

            return wordsFound.Distinct().ToList();
        }

        #region Private Helpers
        private char[][] GetMatrixFromIEnumerable(IEnumerable<string> src)
        {
            var rows = src.Count();
            var matrix = new char[rows][];
            var i = 0;

            foreach (var srcRow in src)
            {
                matrix[i] = (char[])srcRow.ToLower().ToArray();
                i++;
            }

            return matrix;
        }

        private IEnumerable<int> GetOcurrencesOf(char findedChar, char[] row)
        {
            var listOfOcurrences = new List<int>();

            for (int i = 0; i < row.Length; i++)
            {
                if (row[i] == findedChar)
                    listOfOcurrences.Add(i);
            }

            return listOfOcurrences;
        }

        private IEnumerable<string> GetVerticalString(char[][] matrix, int startRow, IEnumerable<int> startColumn, int size)
        {
            var resultStringList = new List<string>();
            var qb = new StringBuilder();

            foreach (var colIndex in startColumn)
            {
                for (int i = startRow; i < size; i++)
                {
                    qb.Append(matrix[i][colIndex]);
                }

                resultStringList.Add(qb.ToString());
                qb.Clear();
            }

            return resultStringList;
        }

        private IEnumerable<string> GetWordsStartingWith(char startingChar, IEnumerable<string> words)
        {
            return words.Where(w => w[0] == (startingChar));
        }
        #endregion
    }
}
