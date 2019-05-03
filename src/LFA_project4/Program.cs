using LFA_project4.model;
using LFA_project4.Model;
using LFA_project4.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LFA_project4
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Escreva o alfabeto separado por virgula: ");
            string operands = Console.ReadLine();

            Console.WriteLine("Escreva uma ER: ");
            string er = Console.ReadLine();

            Thompson t = new Thompson(ConversionUtils.ToPostFix(er, operands.Split(',').ToList()));
            t.Resolve();
            Graph graphInitial = t.Graph;

            AFD afd = new AFD(graphInitial, operands.Split(','));

            Console.WriteLine($"\nAutômato Inicial\n{graphInitial.ToString()}");

            Console.WriteLine("AFD Equivalente: ");
            afd.Resolve();
            afd.PrintGraph();

            Console.ReadKey();
        }

    }
}
