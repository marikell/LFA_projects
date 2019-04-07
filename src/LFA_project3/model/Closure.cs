using LFA_project3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LFA_project3.Model
{
    public class Closure
    {
        #region Properties
        public Node StateFrom { get; private set; }

        public string Character { get; private set; }
        public List<Node> States { get; private set; }
        public string Name { get; set; }
        #endregion
        #region Constructor 
        public Closure(Node stateFrom, List<Node> states, string name)
        {
            Name = name;
            StateFrom = stateFrom;
            States = states;
        }
        #endregion
        #region Public Methods 
        public string GetFormattedState()
        {
            return String.Join(",", States.OrderBy(v => v.ID).Select(k => k.Value));
        }
        #endregion
        #region Overrides
        public override string ToString() => $"({StateFrom.Value},{Character} = {String.Join(",", States.OrderBy(v => v.ID).Select(k => k.Value))})";

        #endregion

        public bool verifyIfStatesIsEqual(List<Node> states)
        {
            if (States.Count != states.Count)
            {
                return false;
            }

            for (int i = 0; i < States.Count; i++)
            {
                if (States[i].Value != states[i].Value)
                {
                    return false;
                }
            }

            return true;

        }
    }
}
