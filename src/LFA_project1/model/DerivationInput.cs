using System;
using System.Linq;
using System.Collections.Generic;

namespace LFA_Project1.Model
{
    public class DerivationInput
    {
        #region Properties
        public List<int> Steps { get; set; }
        public List<string> Variables { get; set; }
        public string InitialWord { get; set; }
        public string[] RulesBfA { get; set; }
        public string[] RulesAfA { get; set; }         

        #endregion   
    }
}