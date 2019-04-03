using LFA_project3.model;
using LFA_project3.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LFA_project3
{
    //Transformar um AFe em um AFD
    class Program
    {
        static void Main(string[] args)
        {
            Graph graphInitial = BuildGraph();
            //Criação do automato AFE inicial
            AFD afd = new AFD(graphInitial, new string[] { "a", "b", "c" });

            Console.WriteLine($"\nAutômato Inicial\n{graphInitial.ToString()}");

            //q0 como estado inicial
            afd.GenerateAFDSteps(new Node(0, "q0"));

            //Tabela completa dos closures
            var table = afd.GetClosureTable();

            Console.WriteLine("\nPasso a passo do DFAEdge\n");

            foreach (var closure in table)
            {
                Console.WriteLine(closure.ToString());
            }

            Console.WriteLine("\nPassos Finais\n");

            foreach (var closure in afd.States)
            {
                Console.WriteLine(closure.ToString());
            }

            Console.WriteLine("\nAutômato Gerado\n");           

            Console.WriteLine(Convert.AFEToAFD(afd.GetClosureTable()).ToString());

            Console.ReadKey();
        }

        private static Graph BuildGraph()
        {
            Graph graph = new Graph();

            Node n0 = new Node(0, "q0");
            Node n1 = new Node(1, "q1");
            Node n2 = new Node(2, "q2");
            Node n3 = new Node(3, "q3");
            Node n4 = new Node(4, "q4");
            Node n5 = new Node(5, "q5");
            Node n6 = new Node(6, "q6");
            Node n7 = new Node(7, "q7");

            //vindos do N0
            graph.Edges.Add(new Edge(n0, n0, "a"));
            graph.Edges.Add(new Edge(n0, n0, "b"));
            graph.Edges.Add(new Edge(n0, n0, "c"));
            graph.Edges.Add(new Edge(n0, n4, "&"));
            graph.Edges.Add(new Edge(n0, n2, "&"));
            graph.Edges.Add(new Edge(n0, n1, "&"));

            //vindos do N1
            graph.Edges.Add(new Edge(n1, n7, "a"));

            //vindos do N2
            graph.Edges.Add(new Edge(n2, n3, "b"));

            //vindos do N3
            graph.Edges.Add(new Edge(n3, n7, "b"));

            //vindos do N4
            graph.Edges.Add(new Edge(n4, n5, "c"));

            //vindos do N5
            graph.Edges.Add(new Edge(n5, n6, "c"));

            //vindos do N6
            graph.Edges.Add(new Edge(n6, n7, "c"));

            return graph;
        }
    }
}
