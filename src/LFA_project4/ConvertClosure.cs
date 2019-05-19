using LFA_project4.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LFA_project4
{
    public static class ConvertClosure
    {
        public static List<Edge> GetClosureWithStates(List<Closure> afdTable, List<Closure> states)
        {
            List<Edge> closuresWithStates = new List<Edge>();

            foreach (Closure closure in afdTable)
            {
                closuresWithStates.Add(new Edge(closure.StateFrom, ClosureUtils.FindClosureByState(closure.States, states).StateFrom, closure.Character));
            }

            return closuresWithStates;
        }

        public static Graph AFEToAFD(List<Edge> afdEdges)
        {
            throw new Exception("Not implemented!");
        }
    }
}
