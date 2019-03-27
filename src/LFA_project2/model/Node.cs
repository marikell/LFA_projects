using System;
using System.Collections.Generic;
using System.Text;

namespace LFA_project2.Model
{
    public class Node
    {

        public string ID { get; set; }
        public char Value { get; set; }
        // public Edge EdgeFrom { get; set; }
        // public Edge EdgeTo { get; set; }

        public Node(string id, char value)
        {
            ID = id;
            Value = value;
        }
    }
}
