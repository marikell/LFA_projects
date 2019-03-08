using System;
using System.Linq;
using System.Collections.Generic;
using LFA_Project1.Model;

namespace LFA_Project1.Model
{

    public class Derivation : DerivationInput
    {
        #region Properties
        public List<Tuple<string, string>> ProductionRules { get; set; }
        public List<string> RulesAfA { get; set; }
        public List<string> RulesABA { get; set; }

        #endregion

        #region Constructor
        public Derivation(string[] rulesBfA, string[] rulesAfA, int[] step, string[] variables, string initialWord)
        {

            if (step != null)
            {
                Steps = step.ToList();
            }

            Variables = variables.ToList();
            InitialWord = initialWord;
            ProductionRules = new List<Tuple<string, string>>();
            RulesAfA = rulesAfA.ToList();
            RulesABA = rulesBfA.ToList();

            for (int i = 0; i < rulesBfA.Length; i++)
            {
                ProductionRules.Add(new Tuple<string, string>(rulesBfA[i], rulesAfA[i]));
            }
        }

        #endregion
    }
}