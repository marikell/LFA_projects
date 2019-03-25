using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LFA_project2
{
    class Program
    {
        private static List<string> operators = new List<string>
        {
            "*",
            "+",
            "/",
            "-"
        };

        private static List<string> bracketsEntry = new List<string>
        {

            "(",
            "[",
            "{"
        };

        private static List<string> bracketsClose = new List<string>
        {
            ")",
            "]",
            "}"
        };

        static void Main(string[] args)
        {

            // para rodar thompson
            //    var t = new Thompson("AB|C.");
            // t.ResolveGraph();
            // t.PrintGraph();

            string expression = "((A+B)*C-(D-E))*(F-G)";


            Console.WriteLine(posFixa.ToString());

            Console.ReadKey();
        }

        private static List<string> GetOperands(string word)
        {
            List<string> operands = new List<string>();

            foreach (char c in word)
            {
                if (!operators.Contains(c.ToString()) && !bracketsEntry.Contains(c.ToString()) && !bracketsClose.Contains(c.ToString()))
                {
                    if (!operands.Contains(c.ToString()))
                    {
                        operands.Add(c.ToString());
                    }
                }
            }

            return operands;
        }

        private static int Prioridade(string c)
        {
            int prioridade = 0;

            switch (c)
            {
                case "(":
                    prioridade = 1;
                    break;
                case "*":
                    prioridade = 2;
                    break;
                case "+":
                    prioridade = 3;
                    break;
                case "-":
                    prioridade = 3;
                    break;
                case "/":
                    prioridade = 2;
                    break;
                default:
                    break;
            }

            return prioridade;

        }


    }
}
