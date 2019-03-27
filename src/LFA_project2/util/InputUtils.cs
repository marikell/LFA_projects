using LFA_project2.Config;
using LFA_project2.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LFA_project2.Utils
{
    public class InputUtils
    {
        private Input _input;
        public InputUtils(Input input)
        {
            _input = input;
        }

        public string ValidateEmptySpaces() => _input.RegularExpression.Replace(" ", "");

        private bool ValidateBrackets()
        {
            int entryBracketsCount = 0;
            int closeBracketsCount = 0;

            foreach (char c in _input.RegularExpression)
            {
                string character = c.ToString();

                if (Constants.Brackets.Any(o => o.Item1 == character))
                {
                    entryBracketsCount++;
                }

                if (Constants.Brackets.Any(o => o.Item2 == character))
                {
                    closeBracketsCount++;
                }
            }

            return entryBracketsCount == closeBracketsCount;
        }
        
        private bool ValidateOperands()
        {

            List<string> operands = new List<string>();

            //se nao for bracket e nao estiver no alfabeto, deve ser operador
            for (int i = 0; i < _input.RegularExpression.Length; i++)
            {
                string character = _input.RegularExpression[i].ToString();
                // se nao é bracket
                if (!Constants.Brackets.Any(o => o.Item1 == character) &&
                     !Constants.Brackets.Any(v => v.Item2 == character)
                     && !Constants.Operators.Any(x => x.Item1 == character))
                {
                    StringBuilder operand = new StringBuilder();

                    //enquanto um operador ou um bracket nao aparece, vou concatenando
                    while (!Constants.Brackets.Any(o => o.Item1 == character)
                        && !Constants.Brackets.Any(x => x.Item2 == character)
                        && !Constants.Operators.Any(v => v.Item1 == character))
                    {                        
                        operand.Append(character);
                        i++;
                        character = _input.RegularExpression[i].ToString();
                    }
                }
            }

            //lista de strings com o retorno de todas as ocorrências do alfabeto.


            return ValidateRegex(operands);
        }

        private bool ValidateRegex(List<string> occurrences)
        {
            List<string> found = new List<string>();
            //para cada suposto alfabeto
            foreach (string oc in occurrences)
            {
                if(_input.Operands.Contains(oc))
                {
                    found.Append(oc);
                }                
            }

            //significa que o alfabeto está valido
            return occurrences.Count == found.Count;
        }

        public bool Validate()
        {
            //Removendo os espaços vazios
            _input = new Input(_input.Operands.ToArray(), ValidateEmptySpaces());
            //Validando colchetes, chaves e parenteses
            return ValidateBrackets() & ValidateOperands();
        }
    }
}
