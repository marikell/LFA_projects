using System;
using System.Linq;
using System.Collections.Generic;
using LFA_Project1.Model;

namespace LFA_Project1.Model
{
    public class Derivation : DerivationInput
    {
        public List<Tuple<string, string>> ProductionRules { get; set; }

        #region Constructor
        public Derivation(string[] rulesBfA, string[] rulesAfA, int[] steps, string[] variables, string initialWord)
        {
            Steps = steps.ToList();
            Variables = variables.ToList();
            InitialWord = initialWord;
            ProductionRules = new List<Tuple<string, string>>();

            for (int i = 0; i < rulesBfA.Length; i++)
            {
                ProductionRules.Add(new Tuple<string, string>(rulesBfA[i], rulesAfA[i]));
            }
        }

        #endregion
    }
}