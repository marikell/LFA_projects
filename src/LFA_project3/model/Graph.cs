using System;
using System.Collections.Generic;
using System.Text;

namespace LFA_project3.Model
{
    public class Graph
    {
        public List<Edge> Edges { get; set; }

        public Graph()
        {
            Edges = new List<Edge>();
        }

        public StringBuilder ShowGraph()
        {
            StringBuilder stringBuilder = new StringBuilder();

            Edges.ForEach(o => { stringBuilder.AppendLine($"FROM: {o.NodeFrom.ID} TO: {o.NodeTo.ID} CONSUMING {o.Cost}"); });

            return stringBuilder;
        }
    }
}
