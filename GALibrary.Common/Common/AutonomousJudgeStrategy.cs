using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Core;

namespace GALibrary.Common
{
    public class AutonomousJudgeStrategy<T> : IJudgeStrategy<T>
        where T : Chromosome
    {
        #region Storage

        private IOneChromosomeJudge<T> m_chromosomeJudge;

        #endregion

        #region Construction

        public AutonomousJudgeStrategy()
        {
            m_chromosomeJudge = null;
        }

        public AutonomousJudgeStrategy(IOneChromosomeJudge<T> chromosomeJudge)
        {
            m_chromosomeJudge = chromosomeJudge;
        }

        #endregion

        #region Properties

        public IOneChromosomeJudge<T> ChromosomeJudge
        {
            get
            {
                return m_chromosomeJudge;
            }
            set
            {
                m_chromosomeJudge = value;
            }
        }

        #endregion

        #region IJudgeStrategy<T> Members

        public void Judge(T[] population)
        {
            for (int i = 0; i < population.Length; i++)
            {
                m_chromosomeJudge.Judge(population[i]);
            }
        }

        public void Initialize()
        {
            m_chromosomeJudge.Initialize();
        }

        #endregion
    }
}
