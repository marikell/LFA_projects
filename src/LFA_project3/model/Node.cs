using System;
using System.Collections.Generic;
using System.Text;

namespace LFA_project3.Model
{
    public class Node
    {

        public string ID { get; set; }
        public string Value { get; set; }

        public Node(string id, string value)
        {
            ID = id;
            Value = value;
        }
    }
}
