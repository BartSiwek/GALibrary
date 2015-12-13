using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Common;
using GALibrary.Probability.Distributions;
using GALibrary.Evolutionary.Chromsomes;

namespace GALibrary.Evolutionary.Operations
{
    public class EvolutionaryMutation : IOneChromosomeMutation<DoubleRealNumberChromosome>
    {
        #region Storage

        private Normal m_normal;

        #endregion

        #region Construction

        public EvolutionaryMutation()
        {
            m_normal = new Normal(0, 1);
        }

        #endregion

        #region IOneChromosomeMutation<DoubleRealNumberChromosome> Members

        public void Mutate(ref DoubleRealNumberChromosome chromosome)
        {
            double tau1 = 1.0 / Math.Sqrt(2 * Math.Sqrt(chromosome.Sigmas.Length));
            double tau2 = 1.0 / Math.Sqrt(2 * chromosome.Sigmas.Length);
            double u = m_normal.Next();

            for (int i = 0; i < chromosome.Sigmas.Length; i++)
            {
                double v = m_normal.Next();
                chromosome.Sigmas[i] *= Math.Exp(tau1 * u + tau2 * v);
            }

            for (int i = 0; i < chromosome.Values.Length; i++)
            {
                double v = m_normal.Next();
                chromosome.Values[i] += chromosome.Sigmas[i] * v;
            }
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
