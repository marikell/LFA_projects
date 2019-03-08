using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using LFA_Project1.Model;

namespace LFA_Project1
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

        private List<int> steps(string word, int limit)
        {

            // condições de parada
            if (word.Length > limit)
            {
                return null;
            }
            else if (word == _derivation.RulesAfA.ElementAt(0))
            {
                return new List<int>() { 1 };
            }

            for (var y = 0; y < _derivation.ProductionRules.Count; y++)
            {

                var from = _derivation.ProductionRules.ElementAt(y).Item1;
                var to = _derivation.ProductionRules.ElementAt(y).Item2;

                // Ignorar a primeira regra de derivação, pois só pode ser a primeira
                if (from != _derivation.InitialWord)
                {

                    // Para cada regra de derivação devemos procurar
                    // todas as possiveis ocorrencias da mesma
                    for (var x = 0; x <= word.Length; x++)
                    {

                        var wordToList = Replace(to, from, word, x);

                        if (wordToList != word)
                        {
                            var r = steps(wordToList, limit);
                            if (r == null)
                            {
                                continue;
                            }
                            else
                            {
                                r.Add(y + 1);
                                return r;
                            }
                        }

                    }
                }



            }

            return null;

        }

        public List<int> GetStepsByWord(string word)
        {
            int limit = 1;
            List<int> response = null;

            while (response == null)
            {
                response = steps(word, limit++);
            }

            return response;
        }
    }
}
