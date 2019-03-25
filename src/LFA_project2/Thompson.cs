using System;
using System.Collections.Generic;
using System.Text;

namespace LFA_project2
{
    public class Thompson
    {

        private static List<string> operators = new List<string>
        {
            "*",
            "|",
            ".",
            "+"
        };
        public string Text { get; set; }
        public Graph Graph { get; set; }

        public Thompson(string posFixedString)
        {
            Text = posFixedString;
            Graph = new Graph();
        }

        private Stack<char> MountStackByText(string txt)
        {
            var newStack = new Stack<char>();

            for (int i = txt.Length - 1; i >= 0; i--)
            {
                newStack.Push(txt[i]);
            }

            return newStack;
        }

        public void ResolveGraph()
        {
            Graph = getGraph();
        }

        public void PrintGraph()
        {

            foreach (Edge e in Graph.Edges)
            {

                string nodeFromID, nodeToID;

                if (e.NodeFrom == null)
                {
                    nodeFromID = "s";
                }
                else
                {
                    nodeFromID = e.NodeFrom.ID;
                }

                if (e.NodeTo == null)
                {
                    nodeToID = "e";
                }
                else
                {
                    nodeToID = e.NodeTo.ID;
                }


                Console.WriteLine(string.Format("FROM: {0} // TO: {1} // COST: {2}", nodeFromID, nodeToID, e.Cost));
            }

        }

        private Graph getGraph()
        {

            var result = new Graph();
            var auxStack = new Stack<Tuple<Edge, Edge>>();

            int count = 1;

            foreach (char c in Text)
            {

                if (c == '.')
                {

                    var secondItem = auxStack.Pop();
                    var firstItem = auxStack.Pop();

                    // Conectar o novo node ao começo do primeiro elemento
                    firstItem.Item1.NodeFrom = null;

                    // Cria node intermediario para ligar o primeiro com o segundo
                    var interNode = createNode(count++, '&');
                    firstItem.Item2.NodeTo = interNode;
                    secondItem.Item1.NodeFrom = interNode;

                    // Conectar o final do segundo elemento
                    var unionEndNode = createNode(count++, '&');
                    secondItem.Item2.NodeTo = null;
                    
                    var resultGraph = new Tuple<Edge, Edge>(firstItem.Item1, secondItem.Item2);

                    auxStack.Push(resultGraph);

                }
                else if (c == '|')
                {

                    var graphA = auxStack.Pop();
                    var graphB = auxStack.Pop();

                    // Conectar o novo node ao começo dos elemt.
                    var unionStartNode = createNode(count++, '&');
                    graphA.Item1.NodeFrom = unionStartNode;
                    graphB.Item1.NodeFrom = unionStartNode;

                    // Conectar o node ao final dos elemts.
                    var unionEndNode = createNode(count++, '&');
                    graphA.Item2.NodeTo = unionEndNode;
                    graphB.Item2.NodeTo = unionEndNode;

                    // Criar um novo edge inicial
                    var edgeInitalToStart = createPath(null, unionStartNode, '&');
                    result.Edges.Add(edgeInitalToStart);

                    // Criar um novo edge final
                    var edgeFinalToEnd = createPath(unionEndNode, null, '&');
                    result.Edges.Add(edgeFinalToEnd);

                    var resultGraph = new Tuple<Edge, Edge>(edgeInitalToStart, edgeFinalToEnd);

                    auxStack.Push(resultGraph);
                }
                else
                {
                    var node = createNode(count++, c);

                    var edgeNodeToNull = createPath(node, null, '&');
                    result.Edges.Add(edgeNodeToNull);
                    var edgeNullToNode = createPath(null, node, node.Value);
                    result.Edges.Add(edgeNullToNode);

                    auxStack.Push(new Tuple<Edge, Edge>(edgeNullToNode, edgeNodeToNull));
                }

            }



            // var startNode = new Node("0 (START)", '&');
            // var endNode = new Node(string.Format("{0} (END)", count + 1), '&');

            // foreach (Edge e in result.Edges)
            // {

            //     if (e.NodeFrom == null)
            //     {
            //         e.NodeFrom = startNode;
            //     }

            //     if (e.NodeTo == null)
            //     {
            //         e.NodeTo = endNode;
            //     }

            // }

            return result;

        }

        private Node createNode(int idCount, char value)
        {
            return new Node(string.Format("{0}", idCount), value);
        }

        private Edge createPath(Node nodeA, Node nodeB, char cost)
        {
            return new Edge(nodeA, nodeB, cost);
        }

    }
}
