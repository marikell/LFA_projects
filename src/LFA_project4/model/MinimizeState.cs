using LFA_project4.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LFA_project4.model
{
    public class MinimizeState
    {
        public List<Node> RowNodes { get; set; }
        public List<Node> ColumnNodes { get; set; }

        private Graph _graph;

        private Graph _minimizedGraph;

        private Dictionary<string, List<State>> _memoryList;

        private List<State> _states;

        private List<Node> _newNodes;
        public MinimizeState(Graph graph)
        {
            _graph = graph;
            _states = new List<State>();
            RowNodes = new List<Node>();
            ColumnNodes = new List<Node>();
            _minimizedGraph = new Graph();
            _newNodes = new List<Node>();
            _memoryList = new Dictionary<string, List<State>>();
        }
        public void ResolveProgramFunction(string[] operands)
        {
            Node dNode = null;

            var allNodes = _graph.GetAllNodes().OrderBy(o => o.ID);

            //todos os nodes
            foreach (var node in allNodes)
            {
                //Pegar todos os edges que tem esse nó como From
                var edges = _graph.Edges.Where(o => o.NodeFrom.ID == node.ID);

                foreach (var operand in operands)
                {
                    var edge = edges.FirstOrDefault(o => o.Cost == operand);

                    if (edge != null)
                        continue;

                    if (dNode == null) { dNode = new Node(allNodes.Count(), "d", false, false); }
                    //Conectando o D no node que não possui determinado consumido de letra
                    _graph.Edges.Add(new Edge(node, dNode, operand));

                }
            }

            if (dNode != null)
            {
                foreach (var operand in operands)
                {
                    _graph.Edges.Add(new Edge(dNode, dNode, operand));
                }
            }

        }

        public void Union()
        {
            var notMarked = _states.Where(o => !o.Marked).ToList();

            foreach (var state in notMarked)
            {
                //Gerando novos nós a partir dos não marcados

                //Busco o nó D
                Node d = _graph.GetAllNodes().FirstOrDefault(o => o.Value == "d");

                string nodeName = "";

                //nó atual é o D
                if (d != null && (d.ID == state.NodeColumn.ID || d.ID == state.NodeRow.ID))
                {
                    nodeName = $"q{state.NodeRow.ID}";
                }
                else
                {
                    nodeName = $"q{state.NodeRow.ID}{state.NodeColumn.ID}";
                }

                Node qState = new Node(_graph.GetAllNodes().Count + _newNodes.Count,
                    nodeName, state.NodeRow.Start || state.NodeColumn.Start, state.NodeRow.End || state.NodeColumn.End);

                _newNodes.Add(qState);
            }

        }

        public void PairAnalysis(string[] operands)
        {
            //nós nao marcados
            var notMarkedNodes = _states.Where(o => !o.Marked).OrderBy(x => x.NodeRow.ID).ToList();

            //var mappedStates = new Dictionary<string, List>

            foreach (State state in notMarkedNodes)
            {
                foreach (var operand in operands)
                {
                    Node rowReach = GetStateByCost(state.NodeRow, operand);
                    Node columnReach = GetStateByCost(state.NodeColumn, operand);

                    VerifyIfCurrentStateCanBeMarked(state, rowReach, columnReach);

                    if (state.Marked) { break; }

                }
            }
        }

        public void RemoveNotAccessibleNodesFromMinimized(string[] operands)
        {

            Dictionary<int, List<Node>> mappingReachedStates = new Dictionary<int, List<Node>>();

            Node qStart = _minimizedGraph.GetStartNode();

            Stack<int> auxStack = new Stack<int>();

            auxStack.Push(qStart.ID);

            while(auxStack.Count > 0)
            {
                int currentId = auxStack.Pop();

                Node n = _minimizedGraph.GetAllNodes().FirstOrDefault(o => o.ID == currentId);

                List<Node> mappingNodes = new List<Node>();

                foreach (var operand in operands)
                {
                    Edge e = GetEdge(currentId, operand, _minimizedGraph);

                    if (!auxStack.Contains(e.NodeTo.ID) && e.NodeTo.ID != e.NodeFrom.ID && !mappingReachedStates.ContainsKey(e.NodeTo.ID))
                    {
                        auxStack.Push(e.NodeTo.ID);
                    }

                    if (e.NodeFrom.ID != e.NodeTo.ID)
                    {
                        mappingNodes.Add(e.NodeTo);
                    }
                }

                if (!mappingReachedStates.ContainsKey(n.ID))
                {
                    mappingReachedStates.Add(n.ID, mappingNodes);
                }
            }

            var nodesCantReach = new List<Node>();

            foreach (var m in mappingReachedStates)
            {
                if(m.Value.Count == 0)
                {
                    Node q = _minimizedGraph.GetAllNodes().FirstOrDefault(o => o.ID == m.Key);

                    if (!q.End)
                    {
                        nodesCantReach.Add(q);
                    }
                }               
            }

            var edges = new List<Edge>();

            edges.AddRange(_minimizedGraph.Edges);

            foreach (var n in nodesCantReach)
            {
                foreach (var e in edges)
                {
                    if (e.NodeFrom.ID == n.ID || e.NodeTo.ID == n.ID)
                    {
                        _minimizedGraph.Edges.Remove(e);
                    }
                }
            }


            //Node qStart = _minimizedGraph.GetStartNode();
            //Stack<int> auxStack = new Stack<int>();
            //List<int> processed = new List<int>();
            //List<int> canReachFinalNodes = new List<int>();

            //auxStack.Push(qStart.ID);

            //while (auxStack.Count > 0)
            //{
            //    int currentId = auxStack.Pop();

            //     Node n = _minimizedGraph.GetAllNodes().FirstOrDefault(o => o.ID == currentId);


            //    foreach (var operand in operands)
            //    {
            //        Edge e = GetEdge(currentId, operand, _minimizedGraph);

            //        //esse aqui é final ou está na lista de finais?
            //        if ((e.NodeTo.End || canReachFinalNodes.Contains(currentId)) && !canReachFinalNodes.Contains(currentId))
            //        {
            //            canReachFinalNodes.Add(currentId);
            //        }
            //        if (!processed.Contains(e.NodeTo.ID) && !auxStack.Contains(e.NodeTo.ID)
            //            && !(e.NodeTo.ID == currentId))
            //        {
            //            auxStack.Push(e.NodeTo.ID);
            //        }

            //    }

            //    if (n.End)
            //    {
            //        canReachFinalNodes.Add(currentId);
            //    }
            //    processed.Add(currentId);
            //}

            ////Removo todos os nós que não são alcançados e seus respectivos edges

            //var nodes = _minimizedGraph.GetAllNodes().Where(o => !canReachFinalNodes.Contains(o.ID)).ToList();
            //var edges = new List<Edge>();

            //edges.AddRange(_minimizedGraph.Edges);

            //foreach (var n in nodes)
            //{
            //    foreach (var e in edges)
            //    {
            //        if (e.NodeFrom.ID == n.ID || e.NodeTo.ID == n.ID)
            //        {
            //            _minimizedGraph.Edges.Remove(e);
            //        }
            //    }
            //}
        }

        public void RemoveNotAcessibleNodes(string[] operands, Graph graph)
        {
            var notAcessibleNodes = GetNotAccessibleNodes(operands, graph);

            var edges = new List<Edge>();

            edges.AddRange(graph.Edges);

            foreach (var n in notAcessibleNodes)
            {
                foreach (var e in edges)
                {
                    if (e.NodeFrom.ID == n.ID)
                    {
                        graph.Edges.Remove(e);
                    }
                }
            }
        }


        //Nós que não estão no to de ninguem
        private List<Node> GetNotAccessibleNodes(string[] operands, Graph graph)
        {
            Stack<int> auxStack = new Stack<int>();

            List<int> processedNodes = new List<int>();

            Node nodeStart = graph.GetStartNode();

            auxStack.Push(nodeStart.ID);

            while (auxStack.Count > 0)
            {
                var currentNodeID = auxStack.Pop();

                foreach (var operand in operands)
                {
                    Edge edg = GetEdge(currentNodeID, operand, graph);

                    if (edg != null)
                    {
                        if (!auxStack.Contains(edg.NodeTo.ID) && !processedNodes.Contains(edg.NodeTo.ID))
                        {
                            auxStack.Push(edg.NodeTo.ID);
                        }
                    }
                }

                processedNodes.Add(currentNodeID);
            }

            return graph.GetAllNodes().Where(o => !processedNodes.Contains(o.ID)).ToList();

        }

        /// <summary>
        /// Busco o estado que o nó atinge ao consumir determinada letra.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        private Node GetStateByCost(Node node, string cost)
        {
            return _graph.Edges.FirstOrDefault(o => o.NodeFrom.ID == node.ID && o.Cost == cost).NodeTo;
        }

        //Hora de buscar considerar junção de estados por string, com suas possíveis permutações (q12 = q21)
        private void VerifyIfCurrentStateCanBeMarked(State currentState, Node stateFromRow, Node stateFromColumn)
        {
            //Encontro o estado gerado na minha tabela
            State state = _states.FirstOrDefault(o => (o.NodeColumn.ID == stateFromColumn.ID && o.NodeRow.ID == stateFromRow.ID)
            || (o.NodeColumn.ID == stateFromRow.ID && o.NodeRow.ID == stateFromColumn.ID));

            //Não deverá ser marcado, portanto, saio da função, pois ele já não está marcado.
            if (stateFromRow.ID == stateFromColumn.ID) { return; }

            //Estados gerados são diferentes
            if (state.Marked)
            {
                //Tenho que marcar o nó
                currentState.Marked = true;
                //Marcando todos os nós cujo currentState está presente na lista encabeçada, ou seja, o currentState
                //tá na lista de states de algum
                foreach (var s in _memoryList)
                {
                    //procurando o estado que foi marcado, atual nos valores de outros s
                    var headedStates = s.Value.Where(o => ((o.NodeColumn.ID == currentState.NodeColumn.ID && o.NodeRow.ID == currentState.NodeRow.ID
                    ) || (o.NodeColumn.ID == currentState.NodeRow.ID && o.NodeRow.ID == currentState.NodeColumn.ID)));

                    if (!headedStates.Any()) continue;

                    var row = int.Parse(s.Key.Substring(0, 1));
                    int column = int.Parse(s.Key.Substring(1, 1));

                    State st = _states.FirstOrDefault(o => (o.NodeRow.ID == row && o.NodeColumn.ID == column) ||
                                                              (o.NodeRow.ID == column && o.NodeColumn.ID == row));
                    if (!st.Marked)
                    {
                        st.Marked = true;
                    }
                }

            }
            //Posterior análise
            else
            {
                string stateName = $"{currentState.NodeRow.ID}{currentState.NodeColumn.ID}";

                //Encontrando o state row,column

                if (_memoryList.ContainsKey(stateName))
                {
                    var nodes = _memoryList[stateName];

                    if (!(nodes.Any(o => (o.NodeRow.ID == state.NodeRow.ID && o.NodeColumn.ID == state.NodeColumn.ID) ||
                     (o.NodeColumn.ID == state.NodeRow.ID && o.NodeRow.ID == state.NodeColumn.ID))))
                    {
                        nodes.Add(state);
                        _memoryList[stateName] = nodes;
                    }

                }
                else
                {
                    if (_memoryList.ContainsKey($"{currentState.NodeColumn.ID}{currentState.NodeRow.ID}"))
                    {
                        var nodes = _memoryList[stateName];
                        if (!(nodes.Any(o => (o.NodeRow.ID == state.NodeRow.ID && o.NodeColumn.ID == state.NodeColumn.ID) ||
                            (o.NodeColumn.ID == state.NodeRow.ID && o.NodeRow.ID == state.NodeColumn.ID))))
                        {
                            nodes.Add(state);
                            _memoryList[stateName] = nodes;
                        }
                    }
                    else
                    {
                        var nodes = new List<State>
                        {
                            state
                        };

                        _memoryList.Add(stateName, nodes);
                    }
                }

            }

        }

        private Edge GetEdge(int id, string cost, Graph graph)
        {
            return graph.Edges.FirstOrDefault(o => o.Cost == cost && o.NodeFrom.ID == id);
        }

        private Node CheckIfNodeWasUnited(Node node)
        {
            string nodeId = Convert.ToString(node.ID);

            foreach (var n in _newNodes)
            {
                //significa que se o estado na união está sozinho, é algum nó que teve contato com o D.
                if (n.Value.Length == 2)
                {
                    var NodeRowID = n.Value.Substring(1, 1);

                    if (nodeId == NodeRowID)
                    {
                        return n;
                    }
                }

                else
                {
                    var NodeRowID = n.Value.Substring(1, 1);
                    var NodeColumnID = n.Value.Substring(2, 1);

                    if (nodeId == NodeRowID || nodeId == NodeColumnID)
                    {
                        return n;
                    }
                }
            }

            return null;
        }

        private List<Node> GetNodesFromUnited(Node unitedNode)
        {
            if (unitedNode.Value.Length == 2)
            {
                var nodeRowID = Convert.ToInt32(unitedNode.Value.Substring(1, 1));

                return _graph.GetAllNodes().Where(o => o.ID == nodeRowID).ToList();

            }
            else
            {
                var nodeRowID = Convert.ToInt32(unitedNode.Value.Substring(1, 1));
                var nodeColumnID = Convert.ToInt32(unitedNode.Value.Substring(2, 1));

                return _graph.GetAllNodes().Where(o => o.ID == nodeRowID || o.ID == nodeColumnID).ToList();

            }
        }

        public List<Node> GetNodesWhichAccessDNode()
        {
            return _graph.Edges.Where(o => o.NodeTo.Value == "d").Select(o => o.NodeFrom).ToList();
        }

        public void Generate(string[] operands)
        {
            //Verifico qual dos nós originais foram unificados e coloco seus respectivos Edges no grafo.

            foreach (var node in _graph.GetAllNodes())
            {
                Node q = CheckIfNodeWasUnited(node);

                if (q != null) { continue; }

                //if(node.Value == "d") { continue; }

                foreach (var operand in operands)
                {
                    _minimizedGraph.Edges.Add(new Edge(node, null, operand));
                }
            }

            foreach (var un in _newNodes)
            {
                foreach (var operand in operands)
                {
                    _minimizedGraph.Edges.Add(new Edge(un, null, operand));
                }
            }

            foreach (var edg in _minimizedGraph.Edges)
            {
                //checo se é um node normal ou é um node unido

                Edge originalEdge = GetEdge(edg.NodeFrom.ID, edg.Cost, _graph);

                if (originalEdge != null)
                {
                    Node toUnitedNode = CheckIfNodeWasUnited(originalEdge.NodeTo);
                    //Checo de o nodeTo é um united. Se for, tenho que apontar para o united especifico.
                    if (toUnitedNode != null)
                    {
                        edg.NodeTo = toUnitedNode;
                    }
                    else
                    {
                        edg.NodeTo = originalEdge.NodeTo;
                    }
                    continue;
                }

                //nesse caso terei os dois juntos, então desenho a aresta que vai dos dois. Porém, verifico se já não existe.

                foreach (var n in GetNodesFromUnited(edg.NodeFrom))
                {
                    Edge originalEdgeUnited = GetEdge(n.ID, edg.Cost, _graph);

                    if (originalEdgeUnited != null)
                    {
                        Node toUnitedNode = CheckIfNodeWasUnited(originalEdgeUnited.NodeTo);

                        if (toUnitedNode != null)
                        {
                            edg.NodeTo = toUnitedNode;
                        }
                        else
                        {
                            edg.NodeTo = originalEdgeUnited.NodeTo;
                        }
                    }

                }

                //Se ele não encontrar, provavelmente é um nó que foi unido

            }

            //removo os edges que saem de D e altero os que chegavam em d para chegarem neles mesmo.

            var edges = new List<Edge>();
            edges.AddRange(_minimizedGraph.Edges);

            foreach (var edg in edges)
            {
                if (edg.NodeFrom.Value == "d")
                {
                    _minimizedGraph.Edges.Remove(edg);
                }

                else if (edg.NodeTo.Value == "d")
                {
                    Edge e = GetEdge(edg.NodeFrom.ID, edg.Cost, _minimizedGraph);

                    e.NodeTo = e.NodeFrom;
                }
            }
        }

        public void Mark()
        {
            foreach (var row in RowNodes)
            {
                //se ele é final, marco apenas os não finais
                bool state = row.End;

                foreach (var column in ColumnNodes)
                {
                    if (row.ID < column.ID)
                    {
                        _states.Add(new State
                        {
                            NodeColumn = column,
                            NodeRow = row,
                            Marked = (state == column.End) ? false : true
                        });
                    }
                }
            }
        }


        public Graph GetMinimizedGraph()
        {
            return _minimizedGraph;
        }


        public void InitTable()
        {
            var orderedNodes = _graph.GetAllNodes().OrderBy(o => o.ID).ToList();

            //Preenchendo as linhas
            for (int i = 0; i < orderedNodes.Count() - 1; i++)
            {
                RowNodes.Add(orderedNodes.ElementAt(i));
            }
            //Preenchendo as colunas

            for (int i = 1; i < orderedNodes.Count(); i++)
            {
                ColumnNodes.Add(orderedNodes.ElementAt(i));
            }
        }

    }

    internal class State
    {
        public Node NodeRow { get; set; } //1
        public Node NodeColumn { get; set; } //2
        public bool Marked { get; set; } //Marcado na tabela 

        public override string ToString()
        {
            return $"{NodeRow.Value}-{NodeColumn.Value} M: {(Marked ? 'S' : 'N')}";
        }
    }
}
