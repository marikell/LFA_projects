using System;
using LFA_Project1;
using LFA_Project1.Model;

namespace LFA_project1
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Default
            var ba = new string[] { "S", "X", "X", "X", "Aa", "Ab", "AY", "Ba", "Bb", "BY", "Fa", "Fb", "FY" };
            var aa = new string[] { "XY", "XaA", "XbB", "F", "aA", "bA", "Ya", "aB", "bB", "Yb", "aF", "bF", "" };
            var initialWord = "S";
            var steps = new int[] { 1, 2, 7, 3, 8, 10, 4, 12, 11, 13 };
            var variables = new string[] { "a", "b" };
            #endregion

            Derivation derivation = new Derivation(ba, aa, steps, variables, initialWord);

            Console.WriteLine(new Derivator(derivation).Derive());

        }
    }
}
