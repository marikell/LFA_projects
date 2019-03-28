using LFA_project2.Config;
using LFA_project2.Constant;
using LFA_project2.util;
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

        public string RemoveEmptySpaces() => _input.RegularExpression.Replace(" ", "");

        public bool ValidateBrackets()
        {
            Dictionary<string, int> occurrences = new Dictionary<string, int>();

            foreach (char c in _input.RegularExpression)
            {
                string character = c.ToString();

                if (Constants.Brackets.Any(o => o.Item1 == character))
                {
                    if (occurrences.ContainsKey(character)) { occurrences[character] = occurrences[character] + 1; }

                    else
                    {
                        string close = Constants.Brackets.FirstOrDefault(x => x.Item1 == character).Item2;

                        occurrences.Add(character, 1);
                        occurrences.Add(close, 0);
                    }
                }
                else if(Constants.Brackets.Any(o => o.Item2 == character))
                {
                    if (occurrences.ContainsKey(character)) { occurrences[character] = occurrences[character] + 1; }
                    else
                    {
                        string entry = Constants.Brackets.FirstOrDefault(x => x.Item2 == character).Item1;

                        occurrences.Add(character, 1);
                        occurrences.Add(entry, 0);
                    }
                }
            }

            var entries = occurrences.Where(o => Constants.Brackets.Select(v => v.Item1).Contains(o.Key)).ToList();

            foreach (var e in entries)
            {
                string close = Constants.Brackets.FirstOrDefault(o => o.Item1 == e.Key).Item2;

                if(occurrences[close] != e.Value) { return false; }
            }            

            return true;
        }

        public bool ValidateCharacterOperands()
        {
            foreach (string o in _input.Operands)
            {
                if (o.Length > 1) { return false; }
            }

            return true;
        }
        public List<string> GetOperandsOccurrences()
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

                        character = (i < _input.RegularExpression.Length) ? _input.RegularExpression[i].ToString() : string.Empty;
                    }

                    operands.Add(operand.ToString());
                }
            }

            return operands;
        }

        public bool ValidateRegex(List<string> occurrences)
        {
            var commutes = CommuteUtils.CommuteWord(string.Join("", _input.Operands));

            commutes.AddRange(_input.Operands);

            List<string> found = new List<string>();

            foreach (string oc in occurrences)
            {
                if (commutes.Contains(oc))
                {
                    found.Add(oc);
                }
            }

            return (occurrences.Count == found.Count);
        }


        public bool ValidateEmptyStrings()
        {
            return (!string.IsNullOrEmpty(_input.RegularExpression) && _input.Operands.Count > 0);
        }

        public bool Validate()
        {
            //Removendo os espaços vazios
            _input = new Input(_input.Operands.ToArray(), RemoveEmptySpaces());
            //Validando colchetes, chaves e parenteses
            return ValidateEmptyStrings() & ValidateBrackets() & ValidateRegex(GetOperandsOccurrences()) & ValidateCharacterOperands();
        }
    }
}
