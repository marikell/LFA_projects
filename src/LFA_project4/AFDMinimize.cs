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


        public Graph Resolve()
        {
            _state = new MinimizeState(_afd);
            _state.RemoveNotAcessibleNodes(_operands, _afd);
            _state.ResolveProgramFunction(_operands);
            _state.InitTable();
            _state.Mark();
            _state.PairAnalysis(_operands);
            _state.Union();
            _state.Generate(_operands);
            _state.RemoveNotAccessibleNodesFromMinimized(_operands);
            return _state.GetMinimizedGraph();
        }

        public string PrintGraph()
        {
            return _afd.ToString();
        }

    }
}
