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

                int indexWba = word.IndexOf(wba);

                string builder = string.Empty;

                List<string> arrays = new List<string>();

                for(int i = 0; i < word.Length; i++)
                {
                    if(indexWba == i)
                    {
                        string beforeFoundWord = (i == 0) ? string.Empty : word.Substring(0, i);
                        if(!string.IsNullOrEmpty(beforeFoundWord))
                        {
                            arrays.Add(beforeFoundWord);
                        }
                        arrays.Add(wba);                                   
                        builder = string.Empty;             
                        i += wba.Length - 1;
                    }
                    else
                    {
                        builder += word[i];
                    }
                }

                if(!string.IsNullOrEmpty(builder))
                {
                    arrays.Add(builder);    
                }
                

                for(int i = 0; i<arrays.Count; i++)
                {
                    if(arrays[i].Equals(wba))
                    {
                        arrays[i] = waa;
                    }
                }

                strBuilder = new StringBuilder(string.Join("", arrays));               
            }

            return strBuilder.ToString();
        }


        #endregion


    }
}
