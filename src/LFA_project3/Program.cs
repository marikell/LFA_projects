using LFA_project3.Model;
using System;
using System.Linq;

namespace LFA_project3
{
    //Transformar um AFe em um AFD
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(BuildGraph().ShowGraph().ToString());

            Graph graph = BuildGraph();

            GraphConversion g = new GraphConversion(graph);

            Node nodeStart = graph.Edges.FirstOrDefault(o => o.NodeFrom.ID.Contains("0")).NodeFrom;

            foreach (var item in g.GetClosure(nodeStart).States)
            {
                Console.WriteLine(item.ID);
            }
            
            Console.ReadKey();
        }

        private static Graph BuildGraph()
        {
            Graph graph = new Graph();

            Node n0 = new Node("q0","q0");
            Node n1 = new Node("q1", "q1");
            Node n2 = new Node("q2", "q2");
            Node n3 = new Node("q3", "q3");
            Node n4 = new Node("q4", "q4");
            Node n5 = new Node("q5", "q5");
            Node n6 = new Node("q6", "q6");
            Node n7 = new Node("q7", "q7");

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
