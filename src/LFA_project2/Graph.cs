using System;
using System.Collections.Generic;
using System.Text;

namespace LFA_project2
{
    public class Graph
    {
        public List<Edge> Edges { get; set; }

        public Graph()
        {
            Edges = new List<Edge>();
        }
    }
}
