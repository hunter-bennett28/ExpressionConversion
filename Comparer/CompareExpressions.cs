using System.Collections.Generic;

namespace Project2_Group_17
{
    public class CompareExpressions : IComparer<string>
    {
        /// <summary>
        /// Compares the two results to ensure they are the same
        /// </summary>
        /// <param name="x">A result</param>
        /// <param name="y">A result</param>
        /// <returns>1 if they are the same, 0 otherwise</returns>
        public int Compare(string x, string y)
        {
            if (x == y) return 1;
            else return 0;
        }
    }
}
