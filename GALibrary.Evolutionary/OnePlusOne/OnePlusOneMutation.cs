using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Probability.Distributions;
using GALibrary.Common;
using GALibrary.Evolutionary.Chromsomes;

namespace GALibrary.Evolutionary.OnePlusOne
{
    public class OnePlusOneMutation : IOneChromosomeMutation<RealNumberChromosome>
    {
        #region Storage

        private double m_sigma;
        private Normal m_normal;

        #endregion

        #region Construction

        public OnePlusOneMutation() : this(1.0)
        { }

        public OnePlusOneMutation(double sigma)
        {
            m_sigma = sigma;
            m_normal = new Normal(0, 1);
        }

        #endregion

        #region Properties

        public double Sigma
        {
            get { return m_sigma; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("value", value, "The sigma must be a posiritve real number");
                m_sigma = value;
            }
        }

        #endregion

        #region IOneChromosomeMutation<RealNumberChromosome> Members

        public void Mutate(ref RealNumberChromosome chromosome)
        {
            for (int i = 0; i < chromosome.Genome.Length; i++)
                chromosome.Genome[i] += m_sigma * m_normal.Next();
        }

        public void Initialize()
        { }

        public int SingleOperationOutputs
        {
            get { return 1; }
        }

        #endregion
    }
}
