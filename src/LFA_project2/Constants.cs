using System;
using System.Collections.Generic;
using System.Text;

namespace LFA_project2
{
    public static class Constants
    {
        public static List<Tuple<string,int>> Operators => new List<Tuple<string, int>>
        {
            new Tuple<string, int>(".",2),
            new Tuple<string, int>("|",2),
            new Tuple<string, int>("+",3),
            new Tuple<string, int>("*",3)
        };

        // public static List<Tuple<string,string>> 
    }
}
