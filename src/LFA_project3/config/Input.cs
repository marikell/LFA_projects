using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LFA_project3.Config
{
    public class Input
    {
        public List<string> Operands { get; private set; }
        public string RegularExpression { get; private set; }

        public Input(string[] operands, string regularExpression)
        {
            Operands = operands.ToList();
            RegularExpression = regularExpression;
        }
        public Input()
        {

        }
    }
}
