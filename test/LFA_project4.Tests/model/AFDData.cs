using LFA_project4.Model;
using System.Collections.Generic;

namespace LFA_project4.Tests.model
{
    public class AFDData
    {
        public static IEnumerable<object[]> Graphs =>
        new List<object[]>
        {
           new object[]{ GetGraphs()["G1"],new string []{"a","b" },ExpectedGraph()["G1A"] },
           new object[]{ GetGraphs()["G2"],new string []{"a","b" },ExpectedGraph()["G1B"] },
           new object[]{ GetGraphs()["G3"],new string []{"a","b" },ExpectedGraph()["G1C"] }
        };

        public static IEnumerable<object[]> ProgramFunction =>
        new List<object[]>
        {
           new object[]{ GetGraphs()["PF1"],new string []{"a","b" },ExpectedGraph()["PF1A"] },
           new object[]{ GetGraphs()["PF2"],new string []{"a","b" },ExpectedGraph()["PF2A"] },
           new object[]{ GetGraphs()["PF3"],new string []{"a","b" },ExpectedGraph()["PF3A"] }
        };

        private static Dictionary<string, Graph> GetProgramFunctionGraph()
        {
            Dictionary<string, Graph> dictionary = new Dictionary<string, Graph>();

            //TESTE BÁSICO
            Graph graph1 = new Graph();

            Node q0 = new Node(0, "q0", true, false);
            Node q1 = new Node(1, "q1", false, false);

            //Ambos requerem adicionais
            graph1.Edges.Add(new Edge(q0, q1, "a"));
            graph1.Edges.Add(new Edge(q1, q0, "b"));

            dictionary.Add("PF1", graph1);

            //Apenas um deles requer adicionais
            Graph graph2 = new Graph();
            graph2.Edges.Add(new Edge(q0, q1, "a"));
            graph2.Edges.Add(new Edge(q0, q0, "b"));
            graph2.Edges.Add(new Edge(q1, q0, "b"));

            dictionary.Add("PF2", graph2);

            //Os dois estão completos
            Graph graph3 = new Graph();
            graph3.Edges.Add(new Edge(q0, q1, "a"));
            graph3.Edges.Add(new Edge(q0, q0, "b"));
            graph3.Edges.Add(new Edge(q1, q0, "b"));
            graph3.Edges.Add(new Edge(q1, q1, "a"));

            dictionary.Add("PF3", graph3);

            return dictionary;
        }

        private static Dictionary<string, Graph> GetGraphs()
        {
            Dictionary<string, Graph> dictionary = new Dictionary<string, Graph>();

            Graph graph1 = new Graph();

            //Slide Renata Exemplo 1

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


            //Slide Renata Exercício 1
            Graph graph2 = new Graph();

            q0 = new Node(0, "q0", true, false);
            q1 = new Node(1, "q1", false, false);
            q2 = new Node(2, "q2", false, true);
            q3 = new Node(3, "q3", false, false);
            q4 = new Node(4, "q4", false, false);
            q5 = new Node(5, "q5", false, true);

            graph2.Edges.Add(new Edge(q0, q1, "a"));
            graph2.Edges.Add(new Edge(q0, q0, "b"));

            graph2.Edges.Add(new Edge(q1, q2, "a"));
            graph2.Edges.Add(new Edge(q1, q1, "b"));

            graph2.Edges.Add(new Edge(q2, q3, "a"));
            graph2.Edges.Add(new Edge(q2, q2, "b"));

            graph2.Edges.Add(new Edge(q3, q4, "a"));
            graph2.Edges.Add(new Edge(q3, q3, "b"));

            graph2.Edges.Add(new Edge(q4, q5, "a"));
            graph2.Edges.Add(new Edge(q4, q4, "b"));

            graph2.Edges.Add(new Edge(q5, q0, "a"));
            graph2.Edges.Add(new Edge(q5, q5, "b"));

            Graph graph3 = new Graph();

            q0 = new Node(0, "q0", true, true);
            q1 = new Node(1, "q1", false, false);
            q2 = new Node(2, "q2", false, true);
            q3 = new Node(3, "q3", false, false);
            q4 = new Node(4, "q4", false, false);
            q5 = new Node(5, "q5", false, false);
            Node q6 = new Node(6, "q6", false, true);
            Node q7 = new Node(7, "q7", false, false);

            graph3.Edges.Add(new Edge(q0, q1, "a"));
            graph3.Edges.Add(new Edge(q0, q3, "b"));

            graph3.Edges.Add(new Edge(q1, q4, "a"));
            graph3.Edges.Add(new Edge(q1, q2, "b"));

            graph3.Edges.Add(new Edge(q2, q1, "a"));
            graph3.Edges.Add(new Edge(q2, q5, "b"));

            graph3.Edges.Add(new Edge(q3, q0, "a"));
            graph3.Edges.Add(new Edge(q3, q4, "b"));

            graph3.Edges.Add(new Edge(q5, q2, "a"));
            graph3.Edges.Add(new Edge(q5, q4, "b"));

            graph3.Edges.Add(new Edge(q6, q5, "a"));
            graph3.Edges.Add(new Edge(q6, q7, "b"));

            graph3.Edges.Add(new Edge(q7, q6, "a"));
            graph3.Edges.Add(new Edge(q7, q2, "b"));

            dictionary.Add("G1", graph1);
            dictionary.Add("G2", graph2);
            dictionary.Add("G3", graph3);

            return dictionary;

        }
        public static Dictionary<string, Graph> ExpectedGraph()
        {
            Dictionary<string, Graph> expectedDictionary = new Dictionary<string, Graph>();

            Graph graph = new Graph();

            Node q0 = new Node(0, "q0", true, true);
            Node q1 = new Node(1, "q1", false, false);
            Node q23 = new Node(2, "q23", false, false);
            Node q45 = new Node(3, "q45", false, true);

            graph.Edges.Add(new Edge(q0, q23, "a"));
            graph.Edges.Add(new Edge(q0, q1, "b"));
            graph.Edges.Add(new Edge(q1, q1, "a"));
            graph.Edges.Add(new Edge(q1, q0, "b"));
            graph.Edges.Add(new Edge(q23, q45, "a"));
            graph.Edges.Add(new Edge(q23, q45, "b"));
            graph.Edges.Add(new Edge(q45, q23, "a"));
            graph.Edges.Add(new Edge(q45, q23, "b"));

            Graph graph1 = new Graph();

            Node q03 = new Node(0, "q03", false, false);
            Node q14 = new Node(1, "q14", false, false);
            Node q25 = new Node(2, "q25", true, true);

            graph1.Edges.Add(new Edge(q03, q14, "a"));
            graph1.Edges.Add(new Edge(q03, q03, "b"));
            graph1.Edges.Add(new Edge(q14, q25, "a"));
            graph1.Edges.Add(new Edge(q14, q14, "b"));
            graph1.Edges.Add(new Edge(q25, q03, "a"));
            graph1.Edges.Add(new Edge(q25, q25, "b"));

            Graph graph2 = new Graph();

            Node q02 = new Node(0, "q02", true, true);
            q1 = new Node(1, "q1", false, false);
            Node q35 = new Node(2, "q35", false, false);

            graph2.Edges.Add(new Edge(q02, q1, "a"));
            graph2.Edges.Add(new Edge(q02, q35, "b"));
            graph2.Edges.Add(new Edge(q1, q02, "b"));
            graph2.Edges.Add(new Edge(q35, q02, "a"));

            expectedDictionary.Add("G1A", graph);
            expectedDictionary.Add("G1B", graph1);
            expectedDictionary.Add("G1C", graph2);

            return expectedDictionary;
        }
    }
}