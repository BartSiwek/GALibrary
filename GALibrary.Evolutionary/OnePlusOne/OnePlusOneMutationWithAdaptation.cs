using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Common;
using GALibrary.Evolutionary.Chromsomes;

namespace GALibrary.Evolutionary.OnePlusOne
{
    public class OnePlusOneMutationWithAdaptation : IOneChromosomeMutation<RealNumberChromosome>
    {
        #region Storage

        private OnePlusOneMutation m_mutation;
        private int m_k;
        private int m_count;
        private int m_successes;
        private double m_previous;

        #endregion

        #region Construction

        public OnePlusOneMutationWithAdaptation()
        {
            m_mutation = new OnePlusOneMutation();
            m_k = 5;
            m_count = 0;
            m_successes = 0;
            m_previous = double.NaN;
        }

        public OnePlusOneMutationWithAdaptation(double sigma, int k)
        {
            m_mutation = new OnePlusOneMutation(sigma);
            m_k = k;
            m_count = 0;
            m_successes = 0;
            m_previous = double.NaN;
        }

        #endregion

        #region Properties

        public double Sigma
        {
            get { return m_mutation.Sigma; }
            set { m_mutation.Sigma = value; }
        }

        public int K
        {
            get { return m_k; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value", value, "The K paramter must be positive");
                m_k = value;
            }
        }

        #endregion

        #region IOneChromosomeMutation<RealNumberChromosome> Members

        public void Mutate(ref RealNumberChromosome chromosome)
        {
            if (!double.IsNaN(m_previous))
            {
                if (chromosome.Fitness > m_previous)
                    ++m_successes;
                ++m_count;

                if (m_count == m_k)
                {
                    if (5 * m_successes > m_k)
                        m_mutation.Sigma = (1.0 / 0.82) * m_mutation.Sigma;
                    else if (5 * m_successes < m_k)
                        m_mutation.Sigma = 0.82 * m_mutation.Sigma;

                    m_count = 0;
                    m_successes = 0;
                }
            }

            m_previous = chromosome.Fitness;
            m_mutation.Mutate(ref chromosome);
        }

        public void Initialize()
        {
            //Intialize
            m_count = 0;
            m_successes = 0;
            m_previous = double.NaN;
        }

        public int SingleOperationOutputs
        {
            get
            {
                return m_mutation.SingleOperationOutputs;
            }
        }

        #endregion
    }
}
