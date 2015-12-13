using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Core;
using GALibrary.Evolutionary.Chromsomes;

namespace GALibrary.Evolutionary.Operations
{
    public class LocalAverageCrossover : ICrossoverStrategy<DoubleRealNumberChromosome>
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
                    values[j] = (first.Values[j] + second.Values[j]) / 2;

                for (int j = 0; j < sigmas.Length; j++)
                    sigmas[j] = (first.Sigmas[j] + second.Sigmas[j]) / 2;

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
