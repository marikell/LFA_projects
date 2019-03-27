using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LFA_project2
{
    public static class Conversion
    {
        public static bool GetPriority(string peek, string currentOperator) => 
                    PostFixUtils.Priorities.FirstOrDefault(x => x.Item1 == peek).Item2 >= 
                    PostFixUtils.Priorities.FirstOrDefault(o => o.Item1 == currentOperator).Item2;
        
        public static string ToPostFix(string regularExpression, List<string> operands)
        {
            string validatedRegularExpression = Validate.TurnExpressionValid(regularExpression);
            
            Stack<string> stack = new Stack<string>();
            List<string> results = new List<string>();

            StringBuilder postFix = new StringBuilder();

            foreach (var c in validatedRegularExpression)
            {
                string character = c.ToString();

                //se é operando(letra...)
                if (operands.Contains(character))
                {
                    //copia para a saída
                    postFix.Append(character);
                }
                //se é parenteses de abertura
                else if (PostFixUtils.Brackets.Any(o => o.Item1 == character))
                {
                    //coloca na pilha
                    stack.Push(character);
                }
                //se é um operador *,+
                else if(PostFixUtils.Operators.Any(o => o.Item1 == character))
                {
                    //enquanto a pilha nao está vazia, e houver no seu topo um operador com prioridade maior ou igual ao encontrado.
                    //desempilha e copia para a saída
                    while ((stack.Count != 0) && (GetPriority(stack.Peek(), character)))
                    {
                        postFix.Append(stack.Pop());
                    }
                    
                    //empilhando o caracter encontrado
                    stack.Push(character);

                }
                //se é parenteses de fechamento
                else if (PostFixUtils.Brackets.Any(o => o.Item2 == character))
                {
                    //parenteses de abertura recorrente a esse de fechamento
                    string dequeuedEntry = PostFixUtils.Brackets.FirstOrDefault(o => o.Item2 == character).Item1;
                    //desempilha e copia para a saída até encontrar o parenteses de abertura correspondente.
                    while(!stack.Peek().Equals(dequeuedEntry))
                    {
                        postFix.Append(stack.Pop());
                    }
                    //removendo o parenteses de abertura
                    stack.Pop();
                }
            }
            while (stack.Count != 0)
            {
                postFix.Append(stack.Pop());
            }

            return postFix.ToString();

        }
    }
}