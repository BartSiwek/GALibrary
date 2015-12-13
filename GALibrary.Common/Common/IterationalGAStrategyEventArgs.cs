using System;
using System.Collections.Generic;
using System.Text;

namespace GALibrary.Common
{
    public class IterationalGAStrategyEventArgs : EventArgs
    {
        #region Storage

        private int m_iteration;

        #endregion

        #region Construction

        public IterationalGAStrategyEventArgs(int iteration)
        {
            m_iteration = iteration;
        }

        #endregion

        #region Properties

        public int Iteration
        {
            get
            {
                return m_iteration;
            }
        }

        #endregion
    }
}
