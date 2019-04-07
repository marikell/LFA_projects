using LFA_project3.model;
using LFA_project3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LFA_project3
{
    public class AFD
    {
        #region Properties/Fields
        public Graph Graph;
        public List<Closure> States { get; set; }
        public List<Closure> NewStates { get; set; }

        private int _closureCount;
        private int _nodeCount;

        private string[] _variables;
        private List<Node> _nodes;
        #endregion
        #region Constructor
        public AFD(Graph graph, string[] variables)
        {
            _closureCount = 0;
            _nodeCount = 0;
            Graph = graph;
            _variables = variables;
            _nodes = new List<Node>();
            States = new List<Closure>();
            NewStates = new List<Closure>();
        }

        #endregion
        #region Public Methods


        private List<Node> getNodesInPath(List<Node> list, Node node, string costSearch = "&")
        {

            if (list == null)
            {
                list = new List<Node>() { };
            }

            if (!list.Contains(node) && costSearch == "&")
            {
                list.Add(node);
            }

            foreach (var edge in Graph.GetAllEdgesWithZeroCostByNodeFrom(node.Value, costSearch))
            {
                if (edge.NodeTo != null && !list.Contains(edge.NodeTo) && edge.Cost == costSearch)
                {
                    var nodes = getNodesInPath(list, edge.NodeTo);
                    list.Concat(nodes);
                }
            }

            return list;
        }

        public Closure GetInitialClosure()
        {

            Edge firstEdge = Graph.Edges.First(o => o.NodeFrom.Start);
            var nodelist = getNodesInPath(null, firstEdge.NodeFrom);

            var newClosure = new Closure(firstEdge.NodeTo, nodelist, String.Format("S{0}", _closureCount++));

            _nodes.Add(new Node(_nodeCount++, newClosure.Name,
             true, newClosure.States.FirstOrDefault(n => n.End) != null));
            return newClosure;
        }

        public void Resolve()
        {

            var initial = GetInitialClosure();

            States = new List<Closure>() { initial };
            NewStates = new List<Closure>() { initial };
            List<Edge> newEdges = new List<Edge>();

            while (NewStates.Count > 0)
            {

                var closure = NewStates[0];
                var closureNode = getOrCreateNodeClosure(null, closure.States);
                NewStates.RemoveAt(0);

                foreach (var v in _variables)
                {
                    List<Node> newState = resolveClosure(closure, v);

                    if (newState.Count == 0)
                    {
                        continue;
                    }

                    var newNode = getOrCreateNodeClosure(null, newState);

                    newEdges.Add(new Edge(closureNode, newNode, v));
                }
            }



            Graph = new Graph();
            Graph.Edges = newEdges;

        }

        public void PrintGraph()
        {
            Console.WriteLine(Graph.ToString());
        }

        private Node getOrCreateNodeClosure(Node nodeFrom, List<Node> stateList)
        {

            var closure = States.FirstOrDefault(n => n.verifyIfStatesIsEqual(stateList));

            if (closure == null)
            {

                bool isFinal = false;

                // É final ?
                if (stateList.FirstOrDefault(n => n.End) != null)
                {
                    isFinal = true;
                }

                var newClosure = new Closure(nodeFrom, stateList, String.Format("S{0}", _closureCount++));
                States.Add(newClosure);
                NewStates.Add(newClosure);
                var newNode = new Node(_nodeCount++, newClosure.Name, false, isFinal);
                _nodes.Add(newNode);
                return newNode;
            }

            return _nodes.First(q => q.Value == closure.Name);

        }

        private List<Node> resolveClosure(Closure closure, string letter)
        {

            var returnNodes = new List<Node>();

            foreach (Node n in closure.States)
            {
                var emptyPathNodes = getNodesInPath(null, n);

                foreach (Node emptyNodePath in emptyPathNodes)
                {
                    var newNodes = getNodesInPath(null, emptyNodePath, letter);
                    returnNodes = AddOnNodeListIfNotExist(returnNodes, newNodes);
                }
            }

            return returnNodes.OrderBy(q => q.Value).ToList();


        }


        private List<Node> AddOnNodeListIfNotExist(List<Node> original, List<Node> newValues)
        {

            foreach (Node n in newValues)
            {
                if (!original.Contains(n))
                {
                    original.Add(n);
                }
            }

            return original;

        }



        #endregion
        #region Private Methods
        /// <summary>
        /// Método que retorna o próximo ID do estado gerado.
        /// </summary>
        /// <returns></returns>
        private int GetNewID()
        {
            return _closureCount + 1;
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"({string.Join("-", States)})";
        }
        #endregion
    }
}
