using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using LFA_project1.Model;

namespace LFA_project1
{
    public class Derivator
    {
        private readonly Derivation _derivation;
        public Derivator(Derivation derivation)
        {
            _derivation = derivation;
        }

        public static string Replace(string stringToSearch, string stringToReplace, string word, int startIndex = 0)
        {
            return (new Regex(Regex.Escape(stringToSearch)).Replace(word, stringToReplace, 1, startIndex));
        }
        public string Derive()
        {
            StringBuilder strBuilder = new StringBuilder(_derivation.InitialWord);

            foreach (int step in _derivation.Steps)
            {
                var wba = _derivation.ProductionRules.ElementAt(step - 1).Item1;
                var waa = _derivation.ProductionRules.ElementAt(step - 1).Item2;

                var word = strBuilder.ToString();

                if (!word.Contains(wba))
                {
                    throw new Exception(String.Format("Palavra {0} não encontrada na {1}", wba, word));
                }

                strBuilder = new StringBuilder(Replace(wba, waa, word));
            }
            return strBuilder.ToString();
        }

        private List<int> GetSteps(string word, int size)
        {


            List<Word> list = new List<Word>() { new Word(_derivation.RulesAfA.ElementAt(0)) };

            while (list.Count > 0)
            {

                Word finalWord = list.FirstOrDefault(q => q.Text == word);

                if (finalWord != null)
                {

                    List<int> returnList = finalWord.Steps;
                    return returnList;
                }

                int sliceSize = list.Count;

                for (int i = 0; i < sliceSize; i++)
                {

                    Word listWord = list.ElementAt(i);

                    for (var y = 0; y < _derivation.ProductionRules.Count; y++)
                    {

                        var from = _derivation.ProductionRules.ElementAt(y).Item1;
                        var to = _derivation.ProductionRules.ElementAt(y).Item2;

                        var wordToList = Replace(from, to, listWord.Text);

                        if (wordToList != listWord.Text && list.FirstOrDefault(q => q.Text == wordToList) == null && wordToList.Length <= size)
                        {
                            list.Add(new Word(wordToList, listWord.Steps, y + 1));
                        }

                    }


                }

                list.RemoveRange(0, sliceSize);
            }

            return null;
        }

        public List<int> GetStepsByWord(string word)
        {
            int limit = 1;
            List<int> response = null;

            while (response == null)
            {
                response = GetSteps(word, limit++);
            }

            return response;
        }
    }
}
