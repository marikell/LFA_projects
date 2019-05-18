using LFA_project4.Model;
using System.Collections.Generic;
using System.Linq;

namespace LFA_project4.model
{
    public class MinimizeState
    {
        public List<Node> RowNodes { get; set; }
        public List<Node> ColumnNodes { get; set; }

        private Graph _graph;

        private Dictionary<string, List<State>> _memoryList;

        private List<State> _states;
        public MinimizeState(Graph graph)
        {
            _graph = graph;
            _states = new List<State>();
            RowNodes = new List<Node>();
            ColumnNodes = new List<Node>();
            _memoryList = new Dictionary<string, List<State>>();
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

                    if(state.Marked) { break; }

                }
            }


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

                    var row = int.Parse(s.Key.Substring(0,1));
                    int column = int.Parse(s.Key.Substring(1,1));

                    State st = _states.FirstOrDefault(o => (o.NodeRow.ID == row && o.NodeColumn.ID == column) ||
                                                              (o.NodeRow.ID == column && o.NodeColumn.ID == row));
                    if(!st.Marked)
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
                    nodes.Add(state);
                    _memoryList[stateName] = nodes;
                }
                else
                {
                    if (_memoryList.ContainsKey($"{currentState.NodeColumn.ID}{currentState.NodeRow.ID}"))
                    {
                        var nodes = _memoryList[stateName];
                        nodes.Add(state);
                        _memoryList[stateName] = nodes;
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
