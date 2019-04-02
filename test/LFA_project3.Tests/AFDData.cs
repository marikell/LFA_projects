using LFA_project3.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LFA_project3.Tests
{
    public class AFDData
    {
        public static IEnumerable<object[]> AFDs =>
        new List<object[]>
        {
           new object[]{GenerateAFNToAFDGraph()["AFN1"],new string []{"a","b" },ExpectedClosures()["AFN1"] },
           //new object[]{GenerateAFNToAFDGraph()["AFN2"],ExpectedClosures()["AFN2"] },
           //new object[]{GenerateAFEToAFDGraph()["AFE1"],new string[] { "a", "b" } ,ExpectedClosures()["AFE1"] }

        };        

        #region Closures

        private static Dictionary<string,List<Closure>> ExpectedClosures()
        {
            Dictionary<string, List<Closure>> closures = new Dictionary<string, List<Closure>>();

            closures.Add("AFN1",
                new List<Closure> {
                    new Closure(CreateNode(0,"s"),
                    new List<Node>{
                        CreateNode(0,"q")
                    }),
                    new Closure(CreateNode(1,"s"),
                    new List<Node>{
                        CreateNode(0,"q"),
                        CreateNode(1,"q")
                    }),
                    new Closure(CreateNode(2,"s"),
                    new List<Node>{
                        CreateNode(0,"q"),
                        CreateNode(1,"q"),
                        CreateNode(2,"q")
                    }),
                    new Closure(CreateNode(3,"s"),
                    new List<Node>{
                        CreateNode(0,"q"),
                        CreateNode(1,"q"),
                        CreateNode(2,"q"),
                        CreateNode(3,"q")
                    })
                });

            return closures;

        }
        #endregion
        private static Node CreateNode(int id, string s)
        {
            return new Node(id, $"{s}{id}");
        }
        #region AFD Graphs

        private static Dictionary<string,AFD> GenerateAFEToAFDGraph()
        {
            Dictionary<string, AFD> dictionary = new Dictionary<string, AFD>();

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

            dictionary.Add("AFE1", new AFD(graph));

            return dictionary;
        }

        private static Dictionary<string, AFD> GenerateAFNToAFDGraph()
        {
            Dictionary<string, AFD> dictionary = new Dictionary<string, AFD>();

            Graph graph1 = new Graph();

            Node n0 = new Node(0, "q0");
            Node n1 = new Node(1, "q1");
            Node n2 = new Node(2, "q2");
            Node n3 = new Node(3, "q3");

            //vindos do N0
            graph1.Edges.Add(new Edge(n0, n0, "a"));
            graph1.Edges.Add(new Edge(n0, n0, "b"));
            graph1.Edges.Add(new Edge(n0, n1, "a"));

            //vindos do N1
            graph1.Edges.Add(new Edge(n1, n2, "a"));

            //vindos do N2
            graph1.Edges.Add(new Edge(n2, n3, "a"));

            dictionary.Add("AFN1", new AFD(graph1));

            Graph graph2 = new Graph();

            n0 = new Node(0, "q0");
            n1 = new Node(1, "q1");
            n2 = new Node(2, "q2");
            n3 = new Node(3, "q3");

            //vindos do N0
            graph2.Edges.Add(new Edge(n0, n1, "a"));


            //vindos do N1
            graph2.Edges.Add(new Edge(n1, n0, "b"));
            graph2.Edges.Add(new Edge(n1, n2, "b"));

            //vindos do N2
            graph2.Edges.Add(new Edge(n2, n0, "a"));

            dictionary.Add("AFN2", new AFD(graph2));

            return dictionary;

        }

        #endregion
    }
}
