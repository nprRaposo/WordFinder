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
            this.WordDictionary = dictionary.OrderBy(w => w); 
        }

        public IList<string> Find(IEnumerable<string> src)
        {
            var rows = src.Count();
            var columns = src.ElementAt(0).Length;

            var matrix = new char[rows][];

            #region Getting the Matrix
            // Formamos la matrix.
            var i = 0;
            foreach (var srcRow in src)
            {
                matrix[i] = (char[])srcRow.ToArray();
                i++;
            }
            #endregion

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
                    horizontalString = new string(matrix[r]);
                    verticalStrings = this.GetVerticalString(matrix, r, listOfOcurrences, rows);

                    foreach (var word in wordsStartingWithCharacter)
                    {
                        if (horizontalString.Contains(word))
                        {
                            wordsFound.Add(word);
                            wordsToFind--;
                        }

                        if (verticalStrings.Any(vs => vs.Contains(word)))
                        {
                            wordsFound.Add(word);
                            wordsToFind--;
                        }
                    }

                    if (wordsToFind == 0)
                    {
                        break;
                    }
                }
            }

            return wordsFound;
        }

        #region Private Helpers
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
