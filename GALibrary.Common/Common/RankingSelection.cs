using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Core;
using GALibrary.Probability;

namespace GALibrary.Common
{
    public class RankingSelection<T> : ISelectionStrategy<T>
        where T : Chromosome
    {
        #region Storage

        private IRankBasedProbabilityComputator m_probabilityComputator;

        #endregion

        #region Construction

        public RankingSelection(IRankBasedProbabilityComputator probabilityComputator)
        {
            m_probabilityComputator = probabilityComputator;
        }

        public RankingSelection() : this(null)
        { }

        #endregion

        #region Properties

        public IRankBasedProbabilityComputator ProbabilityComputator
        {
            get { return m_probabilityComputator; }
            set { m_probabilityComputator = value; }
        }

        #endregion

        #region ISelectionStrategy<T> Members

        public T[] Select(T[] population, int genePoolSize)
        {
            if (genePoolSize <= 0)
                throw new ArgumentOutOfRangeException("genePoolSize", genePoolSize, "Gene pool size must be either greater than zero");

            //Reserve the gene pool
            T[] genePool = new T[genePoolSize];

            //Compute probabilities
            double[] probabilities = CalculateProbabilities(population);

            //Perform selection
            for (int i = 0; i < genePool.Length; i++)
            {
                double u = RandomNumberGenerator.NextDouble();

                int idx = Array.BinarySearch<double>(probabilities, u);
                if (idx < 0)
                    idx = ~idx;

                genePool[i] = (T)population[idx].Clone();
            }

            return genePool;
        }

        public void Initialize()
        {
            if (m_probabilityComputator == null)
                throw new InvalidOperationException("The ProbabilityComputator cannot be null");

            m_probabilityComputator.Initialize();
        }

        #endregion

        #region Private methods

        private double[] CalculateProbabilities(T[] population)
        {
            //Sort the population -> assign a rank
            Array.Sort<Chromosome>(population as Chromosome[], ChromosomeComparer.DescendingInstance);

            //Compute the non cumulative probability
            double[] probabilities = m_probabilityComputator.ComputeProbabilities(population.Length);

            //Compute the cumulative probability
            for (int i = 1; i < population.Length; i++)
                probabilities[i] = probabilities[i] + probabilities[i - 1];
            probabilities[population.Length - 1] = 1;

            return probabilities;
        }

        #endregion
    }
}
