using System;
using Newtonsoft.Json;
using LFA_Project1.Model;
using LFA_Project1;
using System.Linq;
using LFA_Project1.Validation;
using System.IO;

namespace LFA_project1
{
    class Program
    {

        static void Main(string[] args)
        {
            #region UserInputs
            try
            {
                DerivationInput input = new DerivationInput();

                using (StreamReader r = new StreamReader("config//userInput.json"))
                {
                    string json = r.ReadToEnd();
                    input = JsonConvert.DeserializeObject<DerivationInput>(json);
                }

                if (input == null) { throw new Exception("Erro ao tentar ler arquivo .json"); }


                if (!Validator.ValidateUserInput(input.InitialWord, input.Variables.ToArray(), input.rulesBfA, input.rulesAfA, input.Steps.ToArray()))
                {
                    throw new Exception("Não é possível gerar palavras com dados inválidos inseridos pelo usuários.");
                }             

                if (!Validator.ValidateGramma(input.rulesBfA, input.rulesAfA, input.InitialWord)) { throw new Exception("Não é possível gerar palavras com dados inválidos."); };

                Derivation derivation = new Derivation(input.rulesBfA, input.rulesAfA, input.Steps.ToArray(), input.Variables.ToArray(), input.InitialWord);

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
