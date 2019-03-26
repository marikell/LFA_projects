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
                    nodeFromID = "-";
                }
                else
                {
                    nodeFromID = e.NodeFrom.ID;
                }

                if (e.NodeTo == null)
                {
                    nodeToID = "-";
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

                    var andStartNode = createNode(count++, '&');
                    var andEndNode = createNode(count++, '&');

                    // Conectar o novo node ao começo do primeiro elemento
                    firstItem.Item1.NodeFrom = andStartNode;

                    // Cria node intermediario para ligar o primeiro com o segundo
                    var interNode = createNode(count++, '&');
                    firstItem.Item2.NodeTo = interNode;
                    secondItem.Item1.NodeFrom = interNode;

                    secondItem.Item2.NodeTo = andEndNode;

                    // criar edges das pontas
                    var startEdge = createPath(null, andStartNode, '&');
                    result.Edges.Add(startEdge);
                    var endEdge = createPath(andEndNode, null, '&');
                    result.Edges.Add(endEdge);

                    var resultGraph = new Tuple<Edge, Edge>(startEdge, endEdge);

                    auxStack.Push(resultGraph);

                }
                else if (c == '|')
                {

                    var graphA = auxStack.Pop();
                    var graphB = auxStack.Pop();

                    // cria os dois nós que vão se ligar com
                    // o inicio da união e com os nós das expressões
                    // que estão na operação
                    var nodeAUnion = createNode(count++, '&');
                    var nodeBUnion = createNode(count++, '&');

                    // cria o primeiro nó da união
                    var unionStartNode = createNode(count++, '&');

                    // cria os edges entre o nó da união + os
                    // dois das laterais
                    var edgeUnionToNodeAUnion = createPath(unionStartNode, nodeAUnion, '&');
                    result.Edges.Add(edgeUnionToNodeAUnion);
                    var edgeUnionToNodeBUnion = createPath(unionStartNode, nodeBUnion, '&');
                    result.Edges.Add(edgeUnionToNodeBUnion);

                    // conecta os nós originais ao nodeAunion e nodeBunion
                    graphA.Item1.NodeFrom = nodeAUnion;
                    graphB.Item1.NodeFrom = nodeBUnion;

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
                else if (c == '*')
                {
                    var graph = auxStack.Pop();

                    var firstNode = createNode(count++, '&');
                    var secondNode = createNode(count++, '&');
                    var finalNode = createNode(count++, '&');

                    var pathToFirst = createPath(null, firstNode, '&');
                    var pathToSecond = createPath(firstNode, secondNode, '&');

                    // path to third
                    graph.Item1.NodeFrom = secondNode;
                    // path to final
                    graph.Item2.NodeTo = finalNode;

                    var finalPath = createPath(finalNode, null, '&');
                    var edgeFirstToLast = createPath(firstNode, finalNode, '&');

                    var pathToBackSecond = createPath(graph.Item2.NodeFrom, secondNode, '&');

                    result.Edges.Add(pathToFirst);
                    result.Edges.Add(pathToSecond);
                    result.Edges.Add(finalPath);
                    result.Edges.Add(edgeFirstToLast);
                    result.Edges.Add(pathToBackSecond);

                    var resultGraph = new Tuple<Edge, Edge>(pathToFirst, finalPath);
                    auxStack.Push(resultGraph);
                }
                else
                {
                    var node = createNode(count++, c);

                    var edgeNullToNode = createPath(null, node, node.Value);
                    result.Edges.Add(edgeNullToNode);
                    var edgeNodeToNull = createPath(node, null, '&');
                    result.Edges.Add(edgeNodeToNull);


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
