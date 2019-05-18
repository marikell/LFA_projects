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

            //Console.WriteLine("Escreva o alfabeto separado por virgula: ");
            //string operands = Console.ReadLine();

            //Console.WriteLine("Escreva uma ER: ");
            //string er = Console.ReadLine();

            //Thompson t = new Thompson(ConversionUtils.ToPostFix(er, operands.Split(',').ToList()));
            //t.Resolve();
            //Graph graphInitial = t.Graph;

            //AFD afd = new AFD(graphInitial, operands.Split(','));

            //Console.WriteLine($"\nAutômato Inicial\n{graphInitial.ToString()}");

            //Console.WriteLine("AFD Equivalente: ");
            //afd.Resolve();
            //afd.PrintGraph();
            //Graph graphAfd = afd.Graph;

            Console.WriteLine("AFD Minimizado: ");

            AFDMinimize minimize = new AFDMinimize(TestInitTable(), new string[] { "a", "b" });

            minimize.Resolve();

            Console.ReadKey();
        }

        private static Graph TestInitTable()
        {
            Graph graph1 = new Graph();

            Node q0 = new Node(0, "q0", true, true);
            Node q1 = new Node(1, "q1", false, false);
            Node q2 = new Node(2, "q2", false, false);
            Node q3 = new Node(3, "q3", false, false);
            Node q4 = new Node(4, "q4", false, true);
            Node q5 = new Node(5, "q5", false, true);
            

            graph1.Edges.Add(new Edge(q0, q2, "a"));
            graph1.Edges.Add(new Edge(q0, q1, "b"));

            graph1.Edges.Add(new Edge(q1, q1, "a"));
            graph1.Edges.Add(new Edge(q1, q0, "b"));

            graph1.Edges.Add(new Edge(q2, q4, "a"));
            graph1.Edges.Add(new Edge(q2, q5, "b"));

            graph1.Edges.Add(new Edge(q3, q5, "a"));
            graph1.Edges.Add(new Edge(q3, q4, "b"));

            graph1.Edges.Add(new Edge(q4, q3, "a"));
            graph1.Edges.Add(new Edge(q4, q2, "b"));

            graph1.Edges.Add(new Edge(q5, q2, "a"));
            graph1.Edges.Add(new Edge(q5, q3, "b"));

            //Graph graph2 = new Graph();

            //graph2.Edges.Add(new Edge(q0, q1, "a"));
            //graph2.Edges.Add(new Edge(q0, q3, "b"));

            //graph2.Edges.Add(new Edge(q1, q4, "a"));
            //graph2.Edges.Add(new Edge(q1, q2, "b"));

            //graph2.Edges.Add(new Edge(q2, q1, "a"));
            //graph2.Edges.Add(new Edge(q2, q5, "b"));

            //graph2.Edges.Add(new Edge(q3, q0, "a"));
            //graph2.Edges.Add(new Edge(q3, q4, "b"));

            //graph2.Edges.Add(new Edge(q5, q2, "a"));
            //graph2.Edges.Add(new Edge(q5, q4, "b"));

            return graph1;
        }

        private static Graph TestProgramFunction()
        {
            //TESTE BÁSICO
            Graph graph1 = new Graph();

            Node q0 = new Node(0, "q0", true, false);
            Node q1 = new Node(1, "q1", false, false);

            //Ambos requerem adicionais
            graph1.Edges.Add(new Edge(q0, q1, "a"));
            graph1.Edges.Add(new Edge(q1, q0, "b"));

            //Apenas um deles requer adicionais
            Graph graph2 = new Graph();
            graph2.Edges.Add(new Edge(q0, q1, "a"));
            graph2.Edges.Add(new Edge(q0, q0, "b"));
            graph2.Edges.Add(new Edge(q1, q0, "b"));

            //Os dois estão completos
            Graph graph3 = new Graph();
            graph3.Edges.Add(new Edge(q0, q1, "a"));
            graph3.Edges.Add(new Edge(q0, q0, "b"));
            graph3.Edges.Add(new Edge(q1, q0, "b"));
            graph3.Edges.Add(new Edge(q1, q1, "a"));

            return graph3;

        }

    }
}
