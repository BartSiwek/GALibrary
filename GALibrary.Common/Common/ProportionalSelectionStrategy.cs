using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Core;
using GALibrary.Probability;

namespace GALibrary.Common
{
    public class ProportionalSelectionStrategy<T> : ISelectionStrategy<T>
        where T : Chromosome
    {
        #region Construction

        public ProportionalSelectionStrategy()
        { }

        #endregion

        #region ISelectionStrategy<T> Members

        public T[] Select(T[] population, int genePoolSize)
        {
            if (genePoolSize <= 0)
                throw new ArgumentOutOfRangeException("genePoolSize", genePoolSize, "Gene pool size must be either greater than zero");

            T[] genePool = new T[genePoolSize];
            double[] probabilities = CalculateProbabilities(population);

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
        { }

        #endregion

        #region Private methods

        private double[] CalculateProbabilities(T[] population)
        {
            double sum = 0;
            double[] probabilities = new double[population.Length];

            //Compute the probability base
            for (int i = 0; i < population.Length; i++)
            {
                sum += population[i].Fitness;
                probabilities[i] = sum;
            }

            //Compute the probability
            for (int i = 0; i < population.Length; i++)
            {
                probabilities[i] /= sum;
            }

            return probabilities;
        }

        #endregion
    }
}
