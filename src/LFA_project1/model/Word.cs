using System.Collections.Generic;

namespace LFA_project1.Model
{
    public class Word
    {
        #region Properties
        public string Text { get; set; }
        public List<int> Steps { get; set; }

        #endregion


        #region Constructor
        public Word(string txt)
        {
            Text = txt;
            Steps = new List<int>() { 1 };
        }

        public Word(string txt, List<int> oldList, int newStep)
        {
            Text = txt;

            List<int> newList = new List<int>(oldList);
            newList.Add(newStep);

            Steps = newList;
        }

        #endregion
    }

}

