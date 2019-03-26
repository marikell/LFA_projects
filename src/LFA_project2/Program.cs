using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LFA_project2
{
    class Program
    {
        static void Main(string[] args)
        {

            // para rodar thompson
            //    var t = new Thompson("AB|C.");
            // t.ResolveGraph();
            // t.PrintGraph();

            string expression = "((A|B)*C)|D";

            List<string> operands = new List<string>
            {
                "A",
                "B",
                "C",
                "D",
                "E"
            };

            Console.WriteLine(Convert.ToPosFixed(expression, operands));

            Console.ReadKey();
        }
    }
}
