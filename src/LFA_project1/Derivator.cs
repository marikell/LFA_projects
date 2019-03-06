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

        public static string Replace(string wba, string waa, string word)
        {
            return (new Regex(Regex.Escape(wba)).Replace(word, waa, 1));              
        }
        public string Derive()
        {  
            StringBuilder strBuilder = new StringBuilder(_derivation.InitialWord);

            foreach(int step in _derivation.Steps)
            {
                var wba = _derivation.ProductionRules.ElementAt(step-1).Item1;
                var waa = _derivation.ProductionRules.ElementAt(step-1).Item2;

                var word = strBuilder.ToString();
                
                if(!word.Contains(wba))
                {
                   throw new Exception(String.Format("Palavra {0} não encontrada na {1}", wba, word));          
                }

                strBuilder = new StringBuilder(Replace(wba, waa, word));           
            }            
            return strBuilder.ToString();
        }
    }
}
