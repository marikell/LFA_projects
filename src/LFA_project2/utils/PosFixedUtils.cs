using System;
using System.Collections.Generic;
using System.Text;

namespace LFA_project2
{
    public static class PostFixUtils
    {
        public static List<Tuple<string, int>> Operators => new List<Tuple<string, int>>
        {
            new Tuple<string, int>(".",4),
            new Tuple<string, int>("|",4),
            new Tuple<string, int>("+",5),
            new Tuple<string, int>("*",5)
        };

        public static List<Tuple<string, int>> Priorities => new List<Tuple<string, int>>
        {
            new Tuple<string,int>("{",1),
            new Tuple<string, int>("[",2),
            new Tuple<string, int>("(",3),
            new Tuple<string, int>(".",4),
            new Tuple<string, int>("|",4),
            new Tuple<string, int>("+",5),
            new Tuple<string, int>("*",5)
        };
        public static List<Tuple<string, string>> Brackets => new List<Tuple<string, string>>
        {
            new Tuple<string,string>("{","}"),
            new Tuple<string, string>("[","]"),
            new Tuple<string, string>("(",")")
        };

    }
}
