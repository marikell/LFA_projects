using System;
using System.Collections.Generic;
using System.Text;

namespace LFA_project2
{
    public class Edge
    {
        public string Name { get; set; }

        public ICollection<Node> NodesFrom { get; set; }

        public ICollection<Node> NodesTo { get; set; }

        public Edge()
        {
            NodesFrom = new List<Node>();
            NodesTo = new List<Node>();
        }
    }
}
