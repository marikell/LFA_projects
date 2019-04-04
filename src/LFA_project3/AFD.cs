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
        private Graph _graph;

        private Queue<Closure> _statesToProcess;
        public List<Closure> States { get; set; }

        private int _closureCount;

        private string[] _variables;

        private List<Closure> _closureTable;
        #endregion
        #region Constructor
        public AFD(Graph graph, string[] variables)
        {
            _closureCount = 0;
            _graph = graph;
            _variables = variables;
            _closureTable = new List<Closure>();
            _statesToProcess = new Queue<Closure>();
            States = new List<Closure>();
        }

        #endregion
        #region Public Methods

        public List<Closure> GetClosureTable()
        {
            return _closureTable.OrderBy(o => o.StateFrom.ID).ToList();
        }

        /// <summary>
        /// Retorna a closure inicial do grafo baseado no estado inicial..
        /// </summary>
        /// <returns></returns>
        public Closure GetInitialClosure(Node nodeStart)
        {
            Closure closure = new Closure(new Node(nodeStart.ID, $"s{nodeStart.ID}"), _graph.Edges
                                                      .Where(o => o.NodeFrom.ID == nodeStart.ID && o.Cost == "&")
                                                      .Select(x => x.NodeTo)
                                                      .Distinct()
                                                      .ToList());

            if (closure.States.FirstOrDefault(o => o.ID == nodeStart.ID) == null) { closure.States.Add(nodeStart); }

            return closure;
        }

        /// <summary>
        /// Método que retorna todos os DFAEdges para a criação do autômato AFD a partir do AFE.
        /// </summary>
        /// <param name="nodeStart"></param>
        /// <param name="variables"></param>

        public void GenerateAFDSteps(Node nodeStart)
        {
            Closure initialClosure = GetInitialClosure(nodeStart);

            _statesToProcess.Enqueue(initialClosure);

            while (_statesToProcess.Count > 0)
            {
                Closure stateProcessing = _statesToProcess.Dequeue();

                States.Add(stateProcessing);

                foreach (string character in _variables)
                {
                    Closure ca = GetState(stateProcessing, character);

                    if (_statesToProcess.FirstOrDefault(o => o.StateFrom.ID == ca.StateFrom.ID) == null
                        && States.FirstOrDefault(o => o.StateFrom.ID == ca.StateFrom.ID) == null
                        && ca.States.Count > 0)
                    {
                        _statesToProcess.Enqueue(ca);
                        _closureCount++;
                    }
                }
            }
        }

        /// <summary>
        /// Método que dado um estado, retorna todos os estados que alcança consumindo determinado caracter.
        /// </summary>
        /// <param name="fromState"></param>
        /// <param name="character"></param>
        /// <returns></returns>

        public Closure GetState(Closure fromState, string character)
        {
            List<Node> states = new List<Node>();

            foreach (var f in fromState.States)
            {
                var kk = _graph.Edges.Where(o => o.NodeFrom.ID == f.ID && (o.Cost == "&" || o.Cost == character)).ToList();
                states.AddRange(kk.Select(x => x.NodeTo));
            }

            List<Node> distinctListStates = states.OrderBy(o => o.ID).Distinct(new NodeEqualityComparer()).ToList();

            _closureTable.Add(new Closure(fromState.StateFrom, distinctListStates, character));

            Closure existingState = ClosureUtils.FindClosureByState(distinctListStates, States);

            //se não encontrou nos estados adicionados, pode estar nos estados que estão para processar.
            if (existingState == null)
            {
                existingState = ClosureUtils.FindClosureByState(distinctListStates, _statesToProcess.ToList());
            }

            if (existingState == null)
            {
                return new Closure(new Node(GetNewID(), $"s{GetNewID()}"), states.Distinct(new NodeEqualityComparer()).ToList(), character);
            }

            return existingState;
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
