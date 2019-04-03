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

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var edge in Edges)
            {
                stringBuilder.AppendLine($"{edge.NodeFrom.Value} -----> {edge.NodeTo.Value} ({edge.Cost})");
            }

            return stringBuilder.ToString();
        }
    }
}
