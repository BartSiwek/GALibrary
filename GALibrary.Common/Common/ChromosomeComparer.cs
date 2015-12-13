using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Core;

namespace GALibrary.Common
{
    public class ChromosomeComparer : IComparer<Chromosome>
    {
        #region Constants

        public static readonly ChromosomeComparer DescendingInstance = new ChromosomeComparer(true);
        public static readonly ChromosomeComparer AscendingInstance = new ChromosomeComparer(false);

        #endregion

        #region Storage

        private bool m_descending;

        #endregion

        #region Construction

        public ChromosomeComparer()
        {
            m_descending = false;
        }

        public ChromosomeComparer(bool descending)
        {
            m_descending = descending;
        }

        #endregion

        #region IComparer<Chromosome> Members

        public int Compare(Chromosome x, Chromosome y)
        {
            int result = x.Fitness.CompareTo(y.Fitness);
            if(m_descending)
                result = -result;
            return result;
        }

        #endregion
    }
}
