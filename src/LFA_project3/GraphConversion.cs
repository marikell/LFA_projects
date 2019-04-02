using LFA_project3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LFA_project3
{
    public class GraphConversion
    {
        private Graph _graph;
        public GraphConversion(Graph graph)
        {
            _graph = graph;
        }

        /// <summary>
        /// Retorna a closure do grafo.
        /// </summary>
        /// <returns></returns>
        public Closure GetClosure(Node nodeStart) => new Closure(nodeStart, _graph.Edges
                                                    .Where(o => o.NodeFrom.ID == nodeStart.ID && o.Cost == "&")
                                                    .Select(x => x.NodeTo)
                                                    .ToList());

        public void GetPathFromClosure(Closure closure)
        {
            //var teste = _graph.Edges.Where(o => o.NodeFrom == closure.)
        }


    }
}
