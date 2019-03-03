using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LFA_Project1
{
    public class Derivation
    {
        #region Fields
        private ICollection<Tuple<string,string>> _productionRules;
        private ICollection<int> _steps;
        private ICollection<string> _variables; 
        private string _initialWord;

        #endregion


        #region Properties
        
        protected ICollection<Tuple<string,string>> ProductionRules
        {
            get
            {
                return _productionRules;
            }
        }

        protected ICollection<int> Steps
        {
            get
            {
                return _steps;
            }
        }

        protected ICollection<string> Variables
        {
            get
            {
                return _variables;
            }
        }

        protected string InitialWord
        {
            get
            {
                return _initialWord;
            }
        }

        #endregion

        #region Constructor
        public Derivation()
        {
            _productionRules = new List<Tuple<string,string>>();
            _steps = new List<int>();      
            _variables = new List<string>();      
        }
        #endregion

        #region Methods

        public void Init(string[] rulesBfA, string[] rulesAfA, int[] steps, string[] variables, string initialWord)
        {
            _steps = steps.ToList();  
            _variables = variables.ToList();
            _initialWord = initialWord;

            for(int i = 0; i<rulesBfA.Length; i++)
            {
                _productionRules.Add(new Tuple<string,string>(rulesBfA[i],rulesAfA[i]));
            }
        }
        public string Derive()
        {               
            StringBuilder strBuilder = new StringBuilder(InitialWord);

            foreach(int step in Steps)
            {
                var wba = ProductionRules.ElementAt(step-1).Item1;
                var waa = ProductionRules.ElementAt(step-1).Item2;

                string word = strBuilder.ToString();
                strBuilder = new StringBuilder();

                var splited = Validate(word, wba,word.Split(wba, StringSplitOptions.RemoveEmptyEntries)).ToArray();

                for(int i = 0; i<splited.Length; i++)
                {
                    if(splited[i].Equals(wba))
                    {
                        splited[i] = waa;
                    }
                }
                strBuilder = new StringBuilder(string.Join("", splited));               
            }

            return strBuilder.ToString();
        }


        #endregion

        public ICollection<string> Validate(string word, string wba,string[] wrds)
        {
            List<string> lst = new List<string>();

            string w = string.Empty;

            foreach(char c in word)
            {             
                w += c;

                if(w.Equals(wba))
                {
                    lst.Add(w);
                    w = string.Empty;
                    continue;
                }

                foreach(string wrd in wrds)
                {
                    if(w.Equals(wrd))
                    {
                        lst.Add(w);
                        w = string.Empty;
                    }                  
                }
            }
            
            return lst;
        }

    }
}
