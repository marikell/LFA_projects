using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LFA_project2
{
    public static class Convert
    {
        public static bool GetPriority(string peek, string currentOperator) => 
                    PosFixedUtils.Operators.FirstOrDefault(x => x.Item1 == peek).Item2 >= 
                    PosFixedUtils.Operators.FirstOrDefault(o => o.Item1 == currentOperator).Item2;
        
        public static string ToPosFixed(string regularExpression, List<string> operands)
        {
            string validatedRegularExpression = Validate.TurnExpressionValid(regularExpression);
            
            Stack<string> stack = new Stack<string>();
            List<string> results = new List<string>();

            StringBuilder posFixa = new StringBuilder();

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
                else if (PosFixedUtils.BracketsEntry.Any(o => o.Item1 == character))
                {
                    //coloca na pilha
                    stack.Push(character);
                }
                //se é um operador *,+
                else if(PosFixedUtils.Operators.Any(o => o.Item1 == character))
                {
                    //enquanto a pilha nao está vazia, e houver no seu topo um operador com prioridade maior ou igual ao encontrado.
                    //desempilha e copia para a saída
                    while ((stack.Count != 0) && (GetPriority(stack.Peek(), character)))
                    {
                        posFixa.Append(stack.Pop());
                    }
                    
                    //empilhando o caracter encontrado
                    stack.Push(character);

                }
                //se é parenteses de fechamento
                else if (PosFixedUtils.BracketsClose.Any(o => o.Item1 == character))
                {
                    //parenteses de abertura recorrente a esse de fechamento
                    string dequeuedEntry = PosFixedUtils.BracketsEntry
                                                        .ElementAt(PosFixedUtils.BracketsClose
                                                        .FindIndex(0, PosFixedUtils.BracketsClose.Count, o => o.Item1 == character)).Item1;                                                       
                    
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