using System.Linq;
using System.Collections.Generic;
namespace LFA_Project1.Validation
{
    public static class Validator
    {
        public static bool ValidateUserInput(string initialWord, string[] variables, string productionRules, int[] steps)
        {
            return (!string.IsNullOrEmpty(initialWord) && variables.Length != 0 && steps.Length != 0 && productionRules.Length != 0
                    && steps.Length <= productionRules.Split(",").Length);
        }

        public static bool ValidateGramma(string[] rulesBfA, string[] rulesAfA, string initialWord)
        {
            return (rulesBfA.ToList().Contains(initialWord) && rulesBfA.Length == rulesAfA.Length);
        }
    }
}


