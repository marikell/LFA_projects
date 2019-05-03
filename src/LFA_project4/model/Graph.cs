using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LFA_project4.Model
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

            stringBuilder.AppendLine("-------");
            stringBuilder.AppendLine($"Start Node: {GetStartNode().Value}");
            stringBuilder.AppendLine("-------");
            stringBuilder.AppendLine($"End Node(s): {String.Join(",", GetFinalNodes())}");
            stringBuilder.AppendLine("-------");

            var edgesOrdered = Edges.OrderBy(e => e.NodeFrom.Value).ToList();


            foreach (var edge in edgesOrdered)
            {
                var fromValue = edge.NodeFrom == null ? "-" : edge.NodeFrom.Value;
                var toValue = edge.NodeTo == null ? "-" : edge.NodeTo.Value;
                stringBuilder.AppendLine($"{fromValue} -----> {toValue} ({edge.Cost})");
            }

            return stringBuilder.ToString();
        }

        public Node GetStartNode()
        {
            return Edges.First(q => q.NodeFrom.Start).NodeFrom;
        }

        public List<Node> GetFinalNodes()
        {

            List<Node> returnList = new List<Node>();

            foreach (Edge e in Edges)
            {
                if (e.NodeTo.End && !returnList.Contains(e.NodeTo))
                {
                    returnList.Add(e.NodeTo);
                }

                if (e.NodeFrom.End && !returnList.Contains(e.NodeFrom))
                {
                    returnList.Add(e.NodeFrom);
                }
            }

            return returnList;
        }

        public List<Edge> GetAllEdgesWithZeroCostByNodeTo(string value, string cost)
        {
            return Edges.Where(q => q.NodeTo != null && q.NodeTo.Value == value && q.Cost == cost).ToList();
        }


        public List<Edge> GetAllEdgesWithZeroCostByNodeFrom(string value, string cost)
        {
            return Edges.Where(q => q.NodeFrom != null && q.NodeFrom.Value == value && q.Cost == cost).ToList();
        }
    }
}
