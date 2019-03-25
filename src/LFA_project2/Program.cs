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

            string expression = "A+(B*(C-D)/E)";


            Queue<string> queue = new Queue<string>();
            List<string> results = new List<string>();

            StringBuilder posFixa = new StringBuilder();

            var operands = GetOperands(expression);

            foreach (var c in expression)
            {
                string character = c.ToString();

                //se é operando(letra...)
                if (operands.Contains(character))
                {
                    posFixa.Append(character);
                }
                else if (bracketsEntry.Contains(character) || operators.Contains(character))
                {
                    queue.Enqueue(character);
                }
                else if (bracketsClose.Contains(character))
                {
                    string dequeuedEntry = bracketsEntry[bracketsClose.IndexOf(character)];

                    int prioridade = Prioridade(character);
                    while ((queue.Count != 0) && (Prioridade(queue.Peek()) <= prioridade))
                    {

                    }
                }
            }

            while (queue.Count != 0)
            {
                posFixa.Append(queue.Dequeue());
            }

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
