using System;
using LFA_Project1;
using LFA_Project1.Model;
using System.Linq;
using LFA_Project1.Validation;

namespace LFA_project1
{
    class Program
    {

        static void Main(string[] args)
        {
            #region Default
            // var ba = new string[] { "S", "X", "X", "X", "Aa", "Ab", "AY", "Ba", "Bb", "BY", "Fa", "Fb", "FY" };
            // var aa = new string[] { "XY", "XaA", "XbB", "F", "aA", "bA", "Ya", "aB", "bB", "Yb", "aF", "bF", "" };
            // var initialWord = "S";
            // var steps = new int[] { 1, 2, 7, 3, 8, 10, 4, 12, 11, 13 };
            // var variables = new string[] { "a", "b" };
            // Derivation derivation = new Derivation(ba, aa, steps, variables, initialWord);
            #endregion

            #region UserInputs
            try
            {
                Console.WriteLine("Variável Inicial: ");
                string initialWord = Console.ReadLine();

                Console.WriteLine("Varíaveis (separadas por vírgula a,b,):");
                string variables = Console.ReadLine();
                string[] variablesVector = variables.Split(',');

                Console.WriteLine("Digite as regras de produção, separados no formato (X->B, A->B) separadas por virgula.");
                string productionRules = Console.ReadLine();

                Console.WriteLine("Digite os passos separados por vírgula. (1,2,3...)");
                string steps = Console.ReadLine();
                int[] stepsVector = steps.Split(',').Select(o => Convert.ToInt32(o)).ToArray();

                if (!Validator.ValidateUserInput(initialWord, variablesVector, productionRules, stepsVector))
                {   
                    throw new Exception("Não é possível gerar palavras com dados inválidos inseridos pelo usuários.");
                }

                var productionRulesData = productionRules.Split(',').Select(o =>o.Split("->"));

                string[] ba = new string[productionRulesData.Count()];
                string[] aa = new string[productionRulesData.Count()];

                for(int i = 0; i < productionRulesData.Count(); i++)
                {
                    ba[i] = productionRulesData.ElementAt(i)[0];
                    aa[i] = productionRulesData.ElementAt(i)[1];
                }

                if(!Validator.ValidateGramma(ba, aa, initialWord)){ throw new Exception("Não é possível gerar palavras com dados inválidos.");};

                Derivation derivation = new Derivation(ba, aa, stepsVector, variablesVector, initialWord);

                Console.WriteLine(new Derivator(derivation).Derive());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            #endregion



        }
    }
}
