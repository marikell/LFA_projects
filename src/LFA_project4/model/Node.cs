using System;
using System.Collections.Generic;
using System.Text;

namespace LFA_project4.Model
{
    public class Node
    {

        public int ID { get; set; }
        public string Value { get; set; }
        public bool Start { get; set; }
        public bool End { get; set; }

        public Node(int id, string value, bool isStart = false, bool isEnd = false)
        {
            ID = id;
            Value = value;
            Start = isStart;
            End = isEnd;
        }

        public override string ToString()
        {
            return $"{Value}";
        }

    }
}
