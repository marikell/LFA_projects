using System;
using System.Collections.Generic;
using System.Text;

namespace LFA_project4.util
{
    public static class CommuteUtils
    {
        private static List<string> _permutes = new List<string>();

        private static void Commute(String word, int l, int r)
        {
            if (l == r)
                _permutes.Add(word);
            else
            {
                for (int i = l; i <= r; i++)
                {
                    word = Swap(word, l, i);
                    Commute(word, l + 1, r);
                    word = Swap(word, l, i);
                }
            }
        }

        public static List<string> CommuteWord(string wrd)
        {
            Commute(wrd, 0, wrd.Length - 1);
            return _permutes;
        }

        private static String Swap(String a, int i, int j)
        {
            char temp;
            char[] charArray = a.ToCharArray();
            temp = charArray[i];
            charArray[i] = charArray[j];
            charArray[j] = temp;
            string s = new string(charArray);
            return s;
        }

    }
}