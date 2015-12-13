using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Core;
using GALibrary.Common;

namespace GALibrary.Evolutionary.MuLambda
{
    public class MuLambdaStrategy<T> : IterationalGAStrategy<T>
        where T : Chromosome
    {
        #region Storage

        private int m_lambda;
        private int m_genePoolSize;

        #endregion

        #region Construction

        public MuLambdaStrategy(int mu, int lambda)
            : base(mu)
        {
            if (mu > lambda)
                throw new ArgumentException("The mu must be less or equal to lambda");
            m_lambda = lambda;
        }

        #endregion

        #region IterationalGAStrategy implementation

        protected override void PerformInitialization()
        {
            //Check for needed startegies
            if (JudgeStrategy == null)
                throw new InvalidOperationException("The JudgeStrategy property cannot be null");
            if (SelectionStrategy == null)
                throw new InvalidOperationException("The SelectionStrategy property cannot be null");
            if (CrossoverStrategy == null)
                throw new InvalidOperationException("The CrossoverStrategy property cannot be null");
            if (MutationStrategy == null)
                throw new InvalidOperationException("The MutationStrategy property cannot be null");

            //Compute and check the needed gene pool size
            int needed = m_lambda;
            if(needed % MutationStrategy.SingleOperationOutputs != 0)
                throw new InvalidOperationException("Current lambda is not suitable for chosen mutation strategy");

            needed = (needed / MutationStrategy.SingleOperationOutputs) * MutationStrategy.SingleOperationInputs;
            if(needed % CrossoverStrategy.SingleOperationOutputs != 0)
                throw new InvalidOperationException("Current lambda is not suitable for chosen crossover strategy");

            m_genePoolSize = (needed / CrossoverStrategy.SingleOperationOutputs) * CrossoverStrategy.SingleOperationInputs;

            //Initialize the strategies
            JudgeStrategy.Initialize();
            SelectionStrategy.Initialize();
            CrossoverStrategy.Initialize();
            MutationStrategy.Initialize();

            //Judge the first pop
            JudgeStrategy.Judge(Population);
        }

        protected override void PerformIteration()
        {
            //Judge the current population
            JudgeStrategy.Judge(Population);

            //Generate the new generation
            T[] genePool = SelectionStrategy.Select(Population, m_genePoolSize);
            T[] g1 = genePool;
            CrossoverStrategy.Crossover(ref genePool);
            T[] g2 = genePool;
            MutationStrategy.Mutate(ref genePool);
            T[] g3 = genePool;

            //Judge the new generation
            JudgeStrategy.Judge(genePool);

            //Sort the population
            Array.Sort<Chromosome>(genePool as Chromosome[], ChromosomeComparer.DescendingInstance);

            //Get new population
            for (int i = 0; i < Population.Length; i++)
                Population[i] = genePool[i];
        }

        protected override void PerformShutdown()
        {
            JudgeStrategy.Judge(Population);
        }

        #endregion
    }
}
