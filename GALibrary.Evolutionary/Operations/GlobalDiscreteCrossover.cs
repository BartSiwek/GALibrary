using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Core;
using GALibrary.Probability;
using GALibrary.Evolutionary.Chromsomes;

namespace GALibrary.Evolutionary.Operations
{
    public class GlobalDiscreteCrossover : ICrossoverStrategy<DoubleRealNumberChromosome>
    {
        #region Storage

        private int m_chromosomeLength;

        #endregion

        #region Construction

        public GlobalDiscreteCrossover(int chromosomeLength)
        {
            m_chromosomeLength = chromosomeLength;
        }

        #endregion

        #region ICrossoverStrategy<DoubleRealNumberChromosome> Members

        public void Crossover(ref DoubleRealNumberChromosome[] genePool)
        {
            //Check argument for correctness
            for (int i = 0; i < genePool.Length; i++)
            {
                if (genePool[i].Values.Length != m_chromosomeLength)
                    throw new ArgumentException("One of the chromosomes in the gene pool has invalid values lenght");
                if (genePool[i].Sigmas.Length != m_chromosomeLength)
                    throw new ArgumentException("One of the chromosomes in the gene pool has invalid sigmas lenght");
            }

            //Do crossover
            DoubleRealNumberChromosome[] parentGenePool = genePool;
            genePool = new DoubleRealNumberChromosome[parentGenePool.Length / SingleOperationInputs];

            for (int i = 0; i < genePool.Length; i++)
            {
                double[] values = new double[m_chromosomeLength];
                double[] sigmas = new double[m_chromosomeLength];

                //Claculate the values
                int baseIndex = i * SingleOperationInputs;
                for (int j = 0; j < m_chromosomeLength; j++)
                {
                    double u = RandomNumberGenerator.NextDouble();

                    if(u < 0.5)
                        values[j] = parentGenePool[baseIndex + 2 * j].Values[j];
                    else
                        values[j] = parentGenePool[baseIndex + 2 * j + 1].Values[j];
                }

                //Calculate the sigmas
                baseIndex += 2 * m_chromosomeLength;
                for (int j = 0; j < m_chromosomeLength; j++)
                {
                    double u = RandomNumberGenerator.NextDouble();

                    if(u < 0.5)
                        sigmas[j] = parentGenePool[baseIndex + 2 * j].Sigmas[j];
                    else
                        sigmas[j] = parentGenePool[baseIndex + 2 * j + 1].Sigmas[j];
                }

                genePool[i] = new DoubleRealNumberChromosome(values, sigmas);
            }
        }

        public void Initialize()
        { }

        public int SingleOperationInputs
        {
            get { return m_chromosomeLength * 4; }
        }

        public int SingleOperationOutputs
        {
            get { return 1; }
        }

        #endregion
    }
}
