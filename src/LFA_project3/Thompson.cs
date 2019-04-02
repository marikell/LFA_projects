using LFA_project3.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LFA_project3
{
    public class Thompson
    {
        private string Text { get; set; }
        private Graph Graph { get; set; }
        private Stack<Tuple<Edge, Edge>> AuxStack { get; set; }
        private int Count { get; set; }

        public Thompson(string postFixString)
        {
            Text = postFixString;
            Graph = new Graph();
            AuxStack = new Stack<Tuple<Edge, Edge>>();
            Count = 1;
        }

        public List<string> PrintGraph()
        {
            List<string> graphFormats = new List<string>();

            foreach (Edge e in Graph.Edges)
            {

                string nodeFromID, nodeToID;

                if (e.NodeFrom == null)
                {
                    nodeFromID = "-";
                }
                else
                {
                    nodeFromID = e.NodeFrom.Value;
                }

                if (e.NodeTo == null)
                {
                    nodeToID = "-";
                }
                else
                {
                    nodeToID = e.NodeTo.Value;
                }

                graphFormats.Add(string.Format("FROM: {0} // TO: {1} // COST: {2}", nodeFromID, nodeToID, e.Cost));
            }

            return graphFormats;

        }

        private void MakeUnion()
        {
            var graphA = AuxStack.Pop();
            var graphB = AuxStack.Pop();

            // cria os dois nós que vão se ligar com
            // o inicio da união e com os nós das expressões
            // que estão na operação
            var nodeAUnion = CreateNode();
            var nodeBUnion = CreateNode();

            // cria o primeiro nó da união
            var unionStartNode = CreateNode();

            // cria os edges entre o nó da união + os
            // dois das laterais
            var edgeUnionToNodeAUnion = CreatePath(unionStartNode, nodeAUnion);
            var edgeUnionToNodeBUnion = CreatePath(unionStartNode, nodeBUnion);

            // conecta os nós originais ao nodeAunion e nodeBunion
            graphA.Item1.NodeFrom = nodeAUnion;
            graphB.Item1.NodeFrom = nodeBUnion;

            // Conectar o node ao final dos elemts.
            var unionEndNode = CreateNode();
            graphA.Item2.NodeTo = unionEndNode;
            graphB.Item2.NodeTo = unionEndNode;

            // Criar um novo edge inicial
            var edgeInitalToStart = CreatePath(null, unionStartNode);

            // Criar um novo edge final
            var edgeFinalToEnd = CreatePath(unionEndNode, null);

            var resultGraph = new Tuple<Edge, Edge>(edgeInitalToStart, edgeFinalToEnd);
            AuxStack.Push(resultGraph);
        }

        private void MakeAdd()
        {
            var secondItem = AuxStack.Pop();
            var firstItem = AuxStack.Pop();

            var andStartNode = CreateNode();
            var andEndNode = CreateNode();

            // Conectar o novo node ao começo do primeiro elemento
            firstItem.Item1.NodeFrom = andStartNode;

            // Cria node intermediario para ligar o primeiro com o segundo
            var interNode = CreateNode();
            firstItem.Item2.NodeTo = interNode;
            secondItem.Item1.NodeFrom = interNode;

            secondItem.Item2.NodeTo = andEndNode;

            // criar edges das pontas
            var startEdge = CreatePath(null, andStartNode);
            var endEdge = CreatePath(andEndNode, null);

            var resultGraph = new Tuple<Edge, Edge>(startEdge, endEdge);

            AuxStack.Push(resultGraph);
        }

        private void MakeExpression(string c)
        {
            var node = CreateNode(c);

            var edgeNullToNode = CreatePath(null, node, node.Value);
            var edgeNodeToNull = CreatePath(node, null);

            AuxStack.Push(new Tuple<Edge, Edge>(edgeNullToNode, edgeNodeToNull));
        }

        private void MakeA()
        {
            var graph = AuxStack.Pop();

            var firstNode = CreateNode();
            var secondNode = CreateNode();
            var finalNode = CreateNode();

            var pathToFirst = CreatePath(null, firstNode);
            var pathToSecond = CreatePath(firstNode, secondNode);

            // path to third
            graph.Item1.NodeFrom = secondNode;
            // path to final
            graph.Item2.NodeTo = finalNode;

            var finalPath = CreatePath(finalNode, null);
            var edgeFirstToLast = CreatePath(firstNode, finalNode);

            var pathToBackSecond = CreatePath(graph.Item2.NodeFrom, secondNode);

            var resultGraph = new Tuple<Edge, Edge>(pathToFirst, finalPath);
            AuxStack.Push(resultGraph);
        }

        public void MakeT()
        {
            var lastExpression = AuxStack.Pop();
            MakeExpression(lastExpression.Item1.NodeTo.Value);
            AuxStack.Push(lastExpression);
            MakeA();
            MakeAdd();
        }

        public void Resolve()
        {
            foreach (char c in Text)
            {

                if (c == '.')
                {
                    MakeAdd();
                }
                else if (c == '|')
                {
                    MakeUnion();
                }
                else if (c == '*')
                {
                    MakeA();
                }
                else if (c == '+')
                {
                    MakeT();
                }
                else
                {
                    MakeExpression(c.ToString());
                }

            }

        }

        private Node CreateNode(string value = "&")
        {
            return new Node(Count++, value);
        }

        private Edge CreatePath(Node nodeA, Node nodeB, string cost = "&")
        {
            Edge newEdge = new Edge(nodeA, nodeB, cost);
            Graph.Edges.Add(newEdge);
            return newEdge;
        }

    }
}
