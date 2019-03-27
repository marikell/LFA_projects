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


            var t = new Thompson("A+BC|*.D.");
            t.Resolve();
            t.PrintGraph();
            return;

            Console.ReadKey();
        }
    }
}
