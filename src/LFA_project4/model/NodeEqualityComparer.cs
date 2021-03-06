﻿using LFA_project4.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LFA_project4.model
{
    public class NodeEqualityComparer : IEqualityComparer<Node>
    {
        public bool Equals(Node x, Node y)
        {
            return x.ID == y.ID;
        }

        public int GetHashCode(Node obj)
        {
            return obj.ID.GetHashCode();
        }
    }
}
