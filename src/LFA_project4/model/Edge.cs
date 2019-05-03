using System;
using System.Collections.Generic;
using System.Text;

namespace LFA_project4.Model
{
    public class Edge
    {
        public string ID { get; set; }

        public Node NodeFrom { get; set; }

        public Node NodeTo { get; set; }

        public string Cost { get; set; }

        public Edge(Node nodeFrom, Node nodeTo, string cost)
        {
            NodeFrom = nodeFrom;
            NodeTo = nodeTo;
            Cost = cost;
        }

        public override string ToString()
        {
            return $"{NodeFrom.Value} -------({Cost})----> {NodeTo.Value}";
        }
    }
}
