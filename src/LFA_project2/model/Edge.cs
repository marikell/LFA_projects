using System;
using System.Collections.Generic;
using System.Text;

namespace LFA_project2.Model
{
    public class Edge
    {
        public string ID { get; set; }

        public Node NodeFrom { get; set; }

        public Node NodeTo { get; set; }

        public char Cost { get; set; }

        public Edge(Node nodeFrom, Node nodeTo, char cost)
        {
            // ID = string.Format("From:{0} // To: {1} // Cost: {2}", nodeFrom.ID, nodeTo.ID, cost);
            NodeFrom = nodeFrom;
            NodeTo = nodeTo;
            Cost = cost;
        }
    }
}
