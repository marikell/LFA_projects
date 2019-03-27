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

        public bool ValidateBrackets()
        {
            int entryBracketsCount = 0;
            int closeBracketsCount = 0;

            foreach (char c in _input.RegularExpression)
            {
                string character = c.ToString();

                if(Constants.Brackets.Any(o => o.Item1 == character))
{
                    entryBracketsCount++;
                }

                if(Constants.Brackets.Any(o => o.Item2 == character))
                {
                    closeBracketsCount++;
                }
            }

            return entryBracketsCount == closeBracketsCount;
        }
        public bool ValidateOperators()
        {
            return false;
        }

        public bool Validate()
        {
            return ValidateBrackets();
        }
    }
}
