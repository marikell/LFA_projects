using System;
using System.Collections.Generic;
using System.Text;

namespace LFA_project3.Model
{
    public class Node
    {

        public int ID { get; set; }
        public string Value { get; set; }

        public Node(int id, string value)
        {
            ID = id;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Value}";
        }

    }
}
