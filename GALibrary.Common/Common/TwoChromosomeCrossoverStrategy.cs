using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Core;

namespace GALibrary.Common
{
    public class TwoChromosomeCrossoverStrategy<T> : ICrossoverStrategy<T>
        where T : Chromosome
    {
        #region Storage

        private ITwoChromosomeCrossover<T> m_chromosomeCrossover;

        #endregion

        #region Construction

        public TwoChromosomeCrossoverStrategy()
        {
            m_chromosomeCrossover = null;
        }

        public TwoChromosomeCrossoverStrategy(ITwoChromosomeCrossover<T> chromosomeCrossover)
        {
            m_chromosomeCrossover = chromosomeCrossover;
        }

        #endregion

        #region Properties

        public ITwoChromosomeCrossover<T> ChromosomeCrossover
        {
            get
            {
                return m_chromosomeCrossover;
            }
            set
            {
                m_chromosomeCrossover = value;
            }
        }

        #endregion

        #region ICrossoverStrategy<T> Members

        public void Crossover(ref T[] genePool)
        {
            for (int i = 0; i < genePool.Length; i += 2)
            {
                m_chromosomeCrossover.Crossover(ref genePool[i], ref genePool[i + 1]);
            }
        }

        public void Initialize()
        {
            m_chromosomeCrossover.Initialize();
        }

        public int SingleOperationInputs
        {
            get { return 2; }
        }

        public int SingleOperationOutputs
        {
            get { return m_chromosomeCrossover.SingleOperationOutputs; }
        }

        #endregion
    }
}
