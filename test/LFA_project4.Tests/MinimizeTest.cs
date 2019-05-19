using LFA_project4.Model;
using Xunit;
using LFA_project4.Tests.model;
using System;
using System.Linq;

namespace LFA_project4.Tests
{
    public class MinimizeTest
    {
        [Theory]
        [MemberData(nameof(AFDData.Graphs), MemberType = typeof(AFDData))]
        public void ShouldMinimizeAFD(Graph graph, string[] variables, Graph expectedGraph)
        {
            AFDMinimize minimize = new AFDMinimize(graph, variables);

            var minGraphEdges = minimize.Resolve().Edges.OrderBy(o => o.NodeFrom.Value);
            var expectedGraphEdges = expectedGraph.Edges.OrderBy(o => o.NodeFrom.Value);

            for (int i = 0; i < expectedGraphEdges.Count(); i++)
            {
                Assert.Equal($"{expectedGraphEdges.ElementAt(i).NodeFrom.Value}" +
                    $"{expectedGraphEdges.ElementAt(i).NodeTo.Value}" +
                    $"{expectedGraphEdges.ElementAt(i).Cost}",
                    $"{minGraphEdges.ElementAt(i).NodeFrom.Value}" +
                    $"{minGraphEdges.ElementAt(i).NodeTo.Value}" +
                    $"{minGraphEdges.ElementAt(i).Cost}");
            }
            
        }

    }
}
