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

        private List<int> stepsProfundidade(string word, int limit)
        {
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
                            var r = stepsProfundidade(wordToList, limit);
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

        private List<int> GetStepsUsingBFS(string word, int size)
        {

            var list = new List<string>() { word };
            var listS = new List<List<int>>() { new List<int>() };

            // verificar lista
            while (list.Count > 0)
            {

                if (list.Contains(_derivation.RulesAfA.ElementAt(0)))
                {
                    var index = list.IndexOf(_derivation.RulesAfA.ElementAt(0));
                    var returnList = new List<int>(listS.ElementAt(index));
                    
                    returnList.Reverse();
                    returnList.Insert(0, 1);

                    return returnList;
                }

                var sliceSize = list.Count;

                for (var i = 0; i < sliceSize; i++)
                {

                    var listWord = list.ElementAt(i);

                    for (var y = 0; y < _derivation.ProductionRules.Count; y++)
                    {

                        var from = _derivation.ProductionRules.ElementAt(y).Item1;
                        var to = _derivation.ProductionRules.ElementAt(y).Item2;

                        // Ignorar a primeira regra de derivação, pois só pode ser a primeira
                        if (from != _derivation.InitialWord)
                        {



                            // Para cada regra de derivação devemos procurar
                            // todas as possiveis ocorrencias da mesma
                            for (var x = 0; x <= listWord.Length; x++)
                            {

                                var wordToList = Replace(to, from, listWord, x);
                                var newListStep = new List<int>(listS.ElementAt(i));
                                newListStep.Add(y + 1);


                                if (wordToList != listWord && !list.Contains(wordToList) && wordToList.Length <= size)
                                {
                                    list.Add(wordToList);
                                    listS.Add(newListStep);
                                }

                            }
                        }

                    }


                }

                list.RemoveRange(0, sliceSize);
                listS.RemoveRange(0, sliceSize);
            }

            return null;
        }

        public List<int> GetStepsByWord(string word)
        {
            int limit = 1;
            List<int> response = null;

            while (response == null)
            {
                response = GetStepsUsingBFS(word, limit++);
            }

            return response;
        }
    }
}
