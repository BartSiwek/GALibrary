using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Core;

namespace GALibrary.Common
{
    public class OneChromosomeMutationStrategy<T> : IMutationStrategy<T>
        where T : Chromosome
    {
        #region Storage

        private IOneChromosomeMutation<T> m_chromosomeMutation;

        #endregion 

        #region Construction

        public OneChromosomeMutationStrategy()
        {
            m_chromosomeMutation = null;
        }

        public OneChromosomeMutationStrategy(IOneChromosomeMutation<T> chromosomeMutation)
        {
            m_chromosomeMutation = chromosomeMutation;
        }

        #endregion

        #region Properties

        public IOneChromosomeMutation<T> ChromosomeMutation
        {
            get
            {
                return m_chromosomeMutation;
            }
            set
            {
                m_chromosomeMutation = value;
            }
        }

        #endregion

        #region IMutationStrategy<T> Members

        public void Mutate(ref T[] genePool)
        {
            for (int i = 0; i < genePool.Length; i++)
            {
                m_chromosomeMutation.Mutate(ref genePool[i]);
            }
        }

        public void Initialize()
        {
            m_chromosomeMutation.Initialize();
        }

        public int SingleOperationInputs
        {
            get { return 1; }
        }

        public int SingleOperationOutputs
        {
            get { return m_chromosomeMutation.SingleOperationOutputs; }
        }

        #endregion
    }
}
