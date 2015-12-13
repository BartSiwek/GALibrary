using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Common;
using GALibrary.Probability;

namespace GALibrary.SGA
{
    public class SGAMutation : IOneChromosomeMutation<BinaryChromosome>
    {
        #region Storage

        private double m_mutationProbability;

        #endregion

        #region Construction

        public SGAMutation()
        {
            m_mutationProbability = 0.001;
        }

        public SGAMutation(double mutationProbability)
        {
            if (mutationProbability < 0.0 || mutationProbability > 1.0)
                throw new ArgumentOutOfRangeException("mutationProbability", mutationProbability,
                    "The mutation probability must be within 0.0 to 1.0 range");

            m_mutationProbability = mutationProbability;
        }

        #endregion

        #region Properties

        public double MutationProbability
        {
            get
            {
                return m_mutationProbability;
            }
            set
            {
                if (value < 0.0 || value > 1.0)
                    throw new ArgumentOutOfRangeException("value", value, 
                        "The mutation probability must be within 0.0 to 1.0 range");
                m_mutationProbability = value;
            }
        }

        #endregion

        #region IOneChromosomeMutation<BinaryChromosome> Members

        public void Mutate(ref BinaryChromosome chromosome)
        {
            for (int i = 0; i < chromosome.Genome.Length; i++)
            {
                if (RandomNumberGenerator.NextDouble() < m_mutationProbability)
                    chromosome.Genome[i] = !chromosome.Genome[i];
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
