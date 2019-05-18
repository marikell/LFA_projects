using LFA_project4.model;
using LFA_project4.Model;
using System.Collections.Generic;
using System.Linq;

namespace LFA_project4
{
    public class AFDMinimize
    {
        private readonly Graph _afd = null;
        private readonly string[] _operands = null;
        private MinimizeState _state = null;

        public AFDMinimize(Graph afdGraph, string[] operands)
        {
            _afd = afdGraph;
            _operands = operands;
        }

        public void Resolve()
        {
            ResolveProgramFunction();
            _state = new MinimizeState(_afd);
            _state.InitTable();
            _state.Mark();
            _state.PairAnalysis(_operands);
        }
        public string PrintGraph()
        {
            return _afd.ToString();
        }
        
        public void ResolveProgramFunction()
        {
            Node dNode = null;

            var allNodes = _afd.GetAllNodes().OrderBy(o => o.ID);

            //todos os nodes
            foreach (var node in allNodes)
            {
                //Pegar todos os edges que tem esse nó como From
                var edges = _afd.Edges.Where(o => o.NodeFrom.ID == node.ID);

                foreach (var operand in _operands)
                {
                    var edge = edges.FirstOrDefault(o => o.Cost == operand);

                    if (edge != null)
                        continue;

                    if (dNode == null) { dNode = new Node(allNodes.Count(), "d", false, false); }
                    //Conectando o D no node que não possui determinado consumido de letra
                    _afd.Edges.Add(new Edge(node, dNode, operand));

                }
            }

            if (dNode != null)
            {
                foreach (var operand in _operands)
                {
                    _afd.Edges.Add(new Edge(dNode, dNode, operand));
                }
            }

        }
    }
}
