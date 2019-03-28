using LFA_project2.Config;
using LFA_project2.Utils;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LFA_project2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Projeto 02\nAlfabeto separado por (,):");
                string operands = Console.ReadLine();
                Console.WriteLine("\nExpressão Regular válida: ");
                string regularExpression = Console.ReadLine();

                Input input = new Input(operands.Split(','), regularExpression);

                InputUtils inputUtils = new InputUtils(input);

                if(inputUtils.Validate())
                {
                    Thompson thompson = new Thompson(ConversionUtils.ToPostFix(input.RegularExpression, input.Operands));
                    thompson.Resolve();
                    thompson.PrintGraph();
                }                

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao gerar autômato: {ex.Message}");
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
