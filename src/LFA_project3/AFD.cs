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
        private Graph _graph;

        private List<Closure> _states;

        private Queue<Closure> _statesToProcess;

        private int _closureCount;

        private Queue<Closure> _closureTable;

        public AFD(Graph graph)
        {
            _closureCount = 0;
            _graph = graph;
            _closureTable = new Queue<Closure>();
            _statesToProcess = new Queue<Closure>();
            _states = new List<Closure>();
        }

        public List<Closure> States => _states;

        /// <summary>
        /// Retorna a closure do grafo.
        /// </summary>
        /// <returns></returns>
        public Closure GetInitialClosure(Node nodeStart)
        {
            
            Closure closure =  new Closure(new Node(nodeStart.ID, $"s{nodeStart.ID}"), _graph.Edges
                                                      .Where(o => o.NodeFrom.ID == nodeStart.ID && o.Cost == "&")
                                                      .Select(x => x.NodeTo)
                                                      .Distinct()
                                                      .ToList()); 

            if(closure.States.FirstOrDefault(o => o.ID == nodeStart.ID) == null) { closure.States.Add(nodeStart); }

            return closure;

        }

        public void BuildClosures(Node nodeStart,string[] variables)
        {

            Closure initialClosure = GetInitialClosure(nodeStart);

            //começo colocando na fila o closure initial

            _statesToProcess.Enqueue(initialClosure);
            
            while(_statesToProcess.Count > 0)
            {
                Closure stateProcessing = _statesToProcess.Dequeue();

                _states.Add(stateProcessing);

                foreach (string a in variables)
                {
                    Closure ca = GetState(stateProcessing, a);

                    _closureTable.Enqueue(ca);

                    if(_statesToProcess.FirstOrDefault(o => o.StateFrom.ID == ca.StateFrom.ID) == null &&
                        _states.FirstOrDefault(o => o.StateFrom.ID == ca.StateFrom.ID) == null
                        && ca.States.Count > 0)
                    {
                        _statesToProcess.Enqueue(ca);
                        _closureCount++;
                    }
                }
            }
        }

        public Closure GetState(Closure fromState, string character)
        {
            //temos o Estado S alguma coisa e o caracter.
            List<Node> states = new List<Node>();

            //Para cada estado from state, vejo quais consomem o character determinado

            //para cada estado
            foreach (var f in fromState.States)
            {
                var statesReached = _graph.Edges.Where(o => o.NodeFrom.ID == f.ID && (o.Cost == "&" || o.Cost == character)).Select(x => x.NodeTo);
                states.AddRange(statesReached);
            }

            var k = states.OrderBy(o => o.ID).Distinct(new NodeEqualityComparer()).ToList();

            Closure found = FindClosureByState(k);

            if(found == null)
            {
                return new Closure(new Node(GetNewID(), $"s{GetNewID()}"), states.Distinct(new NodeEqualityComparer()).ToList(), character);
            }

            return found;
        }

        private int GetNewID()
        {
            return _closureCount + 1;
        }

        public override string ToString()
        {
            return $"({string.Join("-", _states)})";
        }

        /// <summary>
        /// Verifica se o estado ou closure já existe.
        /// </summary>
        /// <param name="closure"></param>
        /// <returns></returns>
        public Closure FindClosureByState(List<Node> states)
        {
            foreach (Closure c in _states)
            {
                string t = c.GetFormattedState();
                string fe = String.Join(",", states.OrderBy(v => v.ID).Select(k => k.Value));
                if (t.Equals(fe)) { return c; }
            }

            foreach (Closure c in _statesToProcess)
            {
                string t = c.GetFormattedState();
                string fe = String.Join(",", states.OrderBy(v => v.ID).Select(k => k.Value));
                if (t.Equals(fe)) { return c; }
            }

            return null;
        }
    }
}
