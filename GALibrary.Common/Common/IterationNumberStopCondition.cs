using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Core;

namespace GALibrary.Common
{
    public class IterationNumberStopCondition : IStopCondition
    {
        #region Storage

        private int m_maxIteration;
        private int m_currentIteration;

        #endregion 

        #region Construction

        public IterationNumberStopCondition()
        {
            m_maxIteration = 1000;
            m_currentIteration = 0;
        }

        public IterationNumberStopCondition(int maxIteration)
        {
            if (maxIteration < 1)
                throw new ArgumentOutOfRangeException("maxIteration", maxIteration, 
                    "The maximum iteration must be at least one");

            m_maxIteration = maxIteration;
            m_currentIteration = 0;
        }

        #endregion

        #region Properties

        public int MaxIteration
        {
            get
            {
                return m_maxIteration;
            }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("value", value, "The maximum iteration must be at least one");
                m_maxIteration = value;
            }
        }

        #endregion

        #region IStopCondition Members

        public bool Stop(Chromosome[] population)
        {
            if (m_currentIteration < m_maxIteration)
            {
                ++m_currentIteration;
                return false;
            }
            return true;
        }

        public void Initialize()
        {
            m_currentIteration = 0;
        }

        #endregion
    }
}
