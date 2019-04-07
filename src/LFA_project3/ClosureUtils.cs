using LFA_project3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LFA_project3
{
    public static class ClosureUtils
    {
        /// <summary>
        /// Retorna o closure, caso o mesmo já tenha sido gerado.
        /// </summary>
        /// <param name="closure"></param>
        /// <returns></returns>
        public static Closure FindClosureByState(List<Node> states, List<Closure> searchClosures)
        {
            string argumentStates = String.Join(",", states.OrderBy(v => v.ID).Select(k => k.Value));

            return searchClosures.FirstOrDefault(o => o.GetFormattedState() == argumentStates);
        }
    }
}
