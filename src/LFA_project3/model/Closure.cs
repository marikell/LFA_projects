using LFA_project3.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LFA_project3.Model
{
    public class Closure
    {
        public Node StateFrom { get; private set; }

        public string Character { get; private set; }
        //estados que chego consumindo esse estado corrente (state from)
        public List<Node> States { get; private set; }

        public Closure(Node stateFrom, List<Node> states, string character = "")
        {
            Character = character;
            StateFrom = stateFrom;
            States = states;
        }
    }
}
