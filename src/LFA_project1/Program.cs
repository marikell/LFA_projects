using System;
using Newtonsoft.Json;
using LFA_Project1.Model;
using LFA_Project1;
using System.Linq;
using LFA_Project1.Validation;
using System.IO;
using System.Collections.Generic;
namespace LFA_project1
{
    class Program
    {

        static string DerivateProcess(Derivation derivation) => new Derivator(derivation).Derive();
        static List<int> GetStepsByWord(Derivation derivation, string word) => new Derivator(derivation).GetStepsByWord(word);
        static void Main(string[] args)
        {
            #region UserInputs
            try
            {
                #region Leitura do JSON
                DerivationInput input = new DerivationInput();

                using (StreamReader r = new StreamReader("config//userInput.json"))
                {
                    string json = r.ReadToEnd();
                    input = JsonConvert.DeserializeObject<DerivationInput>(json);
                }

                if (input == null) { throw new Exception("Erro ao tentar ler arquivo .json"); }
                #endregion

                #region Validações
                if (!Validator.ValidateUserInput(input.InitialWord, input.Variables.ToArray(), input.rulesBfA, input.rulesAfA, input.Steps.ToArray()))
                {
                    throw new Exception("Não é possível gerar palavras com dados inválidos inseridos pelo usuários.");
                }

                if (!Validator.ValidateGramma(input.rulesBfA, input.rulesAfA, input.InitialWord)) { throw new Exception("Não é possível gerar palavras com dados inválidos."); };

                #endregion

                Derivation derivation = new Derivation(input.rulesBfA, input.rulesAfA, input.Steps.ToArray(),
                                        input.Variables.ToArray(), input.InitialWord);

                string derivate = DerivateProcess(derivation);
                Console.WriteLine(string.Format("Derivação: {0}", derivate));

                List<int> steps = GetStepsByWord(derivation, derivate);
                Console.WriteLine(string.Format("Passos para gerar a palavra {0}: {1}"),
                derivate, string.Join(",", steps));

                derivation.Steps = steps;
                Console.WriteLine(string.Format("Palavra gerada através dos passos gerados acima: {0}", DerivateProcess(derivation)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            #endregion
        }
    }
}
