using System.Collections.Generic;
using System.Text;

namespace LFA_project2
{
    public static class Convert
    {
        public static int GetPriority(string character)
        {
            int priority = 0;

            switch (character)
            {
                case "(":
                    priority = 1;
                    break;
                case "*":
                    priority = 2;
                    break;
                case "+":
                    priority = 3;
                    break;
                case "-":
                    priority = 3;
                    break;
                case "/":
                    priority = 2;
                    break;
                default:
                    break;
            }

            return priority;

        } 
        public static string ToPosFixed(string regularExpression)
        {
            string validatedRegularExpression = Validate.TurnExpressionValid(regularExpression);
            
            Stack<string> stack = new Stack<string>();
            List<string> results = new List<string>();

            StringBuilder posFixa = new StringBuilder();

            var operands = GetOperands(expression);

            foreach (var c in validatedRegularExpression)
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
                    stack.Push(character);
                }
                //se é um operador *,+
                else if(operators.Contains(character))
                {
                    int prioridade = GetPriority(character);

                    //enquanto a pilha nao está vazia, e houver no seu topo um operador com prioridade maior ou igual ao encontrado.
                    //desempilha e copia para a saída
                    while ((stack.Count != 0) && (GetPriority(stack.Peek()) >= prioridade))
                    {
                        posFixa.Append(stack.Pop());
                    }
                    
                    //empilhando o caracter encontrado
                    stack.Push(character);

                }
                //se é parenteses de fechamento
                else if (bracketsClose.Contains(character))
                {
                    //parenteses de abertura recorrente a esse de fechamento
                    string dequeuedEntry = bracketsEntry[bracketsClose.IndexOf(character)];

                    //desempilha e copia para a saída até encontrar o parenteses de abertura correspondente.
                    while(!stack.Peek().Equals(dequeuedEntry))
                    {
                        posFixa.Append(stack.Pop());
                    }
                    //removendo o parenteses de abertura
                    stack.Pop();
                }
            }
            while (stack.Count != 0)
            {
                posFixa.Append(stack.Pop());
            }

            return posFixa.ToString();

        }
    }
}