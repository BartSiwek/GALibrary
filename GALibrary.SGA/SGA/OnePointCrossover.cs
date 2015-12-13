using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Probability;
using GALibrary.Common;

namespace GALibrary.SGA
{
    public class OnePointCrossover : ITwoChromosomeCrossover<BinaryChromosome>
    {
        #region Storage

        private double m_crossoverProbabilty;

        #endregion

        #region Construction

        public OnePointCrossover()
        {
            m_crossoverProbabilty = 0.75;
        }

        public OnePointCrossover(double crossoverProbabilty)
        {
            if (crossoverProbabilty < 0.0 || crossoverProbabilty > 1.0)
                throw new ArgumentOutOfRangeException("crossoverProbabilty", crossoverProbabilty, 
                    "The crossover probability must be within 0.0 to 1.0 range");
            m_crossoverProbabilty = crossoverProbabilty;
        }

        #endregion

        #region Properties

        public double CrossoverProbabilty
        {
            get
            {
                return m_crossoverProbabilty;
            }
            set
            {
                if (value < 0.0 || value > 1.0)
                    throw new ArgumentOutOfRangeException("value", value, 
                        "The crossover probability must be within 0.0 to 1.0 range");
                m_crossoverProbabilty = value;
            }
        }

        #endregion

        #region ITwoChromosomeCrossover<BinaryChromosome> Members

        public void Crossover(ref BinaryChromosome first, ref BinaryChromosome second)
        {
            if (first.Genome.Length != second.Genome.Length)
                throw new ArgumentException("Both chromosomes must be of the same length");

            if (first.Genome.Length < 2)
                return;

            if (RandomNumberGenerator.NextDouble() < m_crossoverProbabilty)
            {
                int k = RandomNumberGenerator.Next(1, first.Genome.Length);
                for (int i = 0; i < k; i++)
                {
                    bool temp = first.Genome[i];
                    first.Genome[i] = second.Genome[i];
                    second.Genome[i] = temp;
                }
            }
        }

        public void Initialize()
        { }

        public int SingleOperationOutputs
        {
            get { return 2; }
        }

        #endregion
    }
}
