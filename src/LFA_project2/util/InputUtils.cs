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

            return (entryBracketsCount == closeBracketsCount) ? true : throw new Exception("As chaves, colchetes ou parentêses da expressão estão errados.");
        }

        private bool ValidateCharacterOperands(List<string> occurrences)
        {
            foreach (string oc in occurrences)
            {
                if(oc.Length > 1) { throw new Exception("Só são aceitos operandos com 1 caracter."); }
            }

            return true;
        }
        private List<string> GetOperandsOccurrences()
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
                        && !Constants.Operators.Any(v => v.Item1 == character)
                        && !string.IsNullOrEmpty(character))
                    {
                        operand.Append(character);
                        i++;

                        character = (i < _input.RegularExpression.Length) ?
                                    _input.RegularExpression[i].ToString() : string.Empty;

                    }

                    operands.Add(operand.ToString());
                }
            }

            return operands;
        }

        private bool ValidateRegex(List<string> occurrences)
        {
            List<string> found = new List<string>();
            //para cada suposto alfabeto
            foreach (string oc in occurrences)
            {
                if (_input.Operands.Contains(oc))
                {
                    found.Add(oc);
                }
            }

            return (occurrences.Count == found.Count) ? true : throw new Exception("A expressão está inválida em relação ao alfabeto.");
        }

        private bool ValidateEmptyStrings()
        {
            return (!string.IsNullOrEmpty(_input.RegularExpression) && _input.Operands.Count > 0) ? true : throw new Exception("Há campos vazios.");
        }

        public bool Validate()
        {
            //Removendo os espaços vazios
            _input = new Input(_input.Operands.ToArray(), ValidateEmptySpaces());
            //Validando colchetes, chaves e parenteses
            return ValidateEmptyStrings() & ValidateBrackets() & ValidateRegex(GetOperandsOccurrences()) & ValidateCharacterOperands(GetOperandsOccurrences());
        }
    }
}
