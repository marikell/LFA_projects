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


            Stack<string> queue = new Stack<string>();
            List<string> results = new List<string>();

            StringBuilder posFixa = new StringBuilder();

            var operands = GetOperands(expression);

            foreach (var c in expression)
            {
                string character = c.ToString();

                //se é operando(letra...)
                if (operands.Contains(character))
                {
                    //copia para a saída
                    posFixa.Append(character);
                }
                //se é parenteses de abertura
                else if (bracketsEntry.Contains(character))
                {
                    //coloca na pilha
                    queue.Push(character);
                }
                //se é um operador *,+
                else if(operators.Contains(character))
                {
                    int prioridade = Prioridade(character);

                    //enquanto a pilha nao está vazia, e houver no seu topo um operador com prioridade maior ou igual ao encontrado.
                    //desempilha e copia para a saída
                    while ((queue.Count != 0) && (Prioridade(queue.Peek()) >= prioridade))
                    {
                        posFixa.Append(queue.Pop());
                    }
                    
                    //empilhando o caracter encontrado
                    queue.Push(character);

                }
                //se é parenteses de fechamento
                else if (bracketsClose.Contains(character))
                {
                    //parenteses de abertura recorrente a esse de fechamento
                    string dequeuedEntry = bracketsEntry[bracketsClose.IndexOf(character)];

                    //desempilha e copia para a saída até encontrar o parenteses de abertura correspondente.
                    while(!queue.Peek().Equals(dequeuedEntry))
                    {
                        posFixa.Append(queue.Pop());
                    }
                    //removendo o parenteses de abertura
                    queue.Pop();
                }
            }

            while (queue.Count != 0)
            {
                posFixa.Append(queue.Pop());
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
