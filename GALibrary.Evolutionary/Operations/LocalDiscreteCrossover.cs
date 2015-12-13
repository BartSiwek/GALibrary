using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Core;
using GALibrary.Probability;
using GALibrary.Evolutionary.Chromsomes;

namespace GALibrary.Evolutionary.Operations
{
    public class LocalDiscreteCrossover : ICrossoverStrategy<DoubleRealNumberChromosome>
    {
        #region ICrossoverStrategy<DoubleRealNumberChromosome> Members

        public void Crossover(ref DoubleRealNumberChromosome[] genePool)
        {
            DoubleRealNumberChromosome[] parentGenePool = genePool;
            genePool = new DoubleRealNumberChromosome[parentGenePool.Length / 2];

            for (int i = 0; i < genePool.Length; i++)
            {
                DoubleRealNumberChromosome first = parentGenePool[2*i];
                DoubleRealNumberChromosome second = parentGenePool[2*i + 1];

                double[] values = new double[first.Values.Length];
                double[] sigmas = new double[first.Sigmas.Length];

                for (int j = 0; j < values.Length; j++)
                {
                    if (RandomNumberGenerator.NextDouble() < 0.5)
                        values[j] = first.Values[j];
                    else
                        values[j] = second.Values[j];
                }

                for (int j = 0; j < sigmas.Length; j++)
                {
                    if (RandomNumberGenerator.NextDouble() < 0.5)
                        sigmas[j] = first.Sigmas[j];
                    else
                        sigmas[j] = second.Sigmas[j];
                }

                genePool[i] = new DoubleRealNumberChromosome(values, sigmas);
            }
        }

        public void Initialize()
        { }

        public int SingleOperationInputs
        {
            get { return 2; }
        }

        public int SingleOperationOutputs
        {
            get { return 1; }
        }

        #endregion
    }
}
