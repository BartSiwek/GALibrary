using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Core;
using GALibrary.Common;

namespace GALibrary.Evolutionary.MuPlusLambda
{
    public class MuPlusLambdaStrategy<T> : IterationalGAStrategy<T>
        where T : Chromosome
    {
        #region Storage

        private int m_lambda;
        private int m_genePoolSize;

        #endregion

        #region Construction

        public MuPlusLambdaStrategy(int mu, int lambda) : base(mu)
        {
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
            CrossoverStrategy.Crossover(ref genePool);
            MutationStrategy.Mutate(ref genePool);

            //Judge the new generation
            JudgeStrategy.Judge(genePool);

            //Merge the current population and new generation
            T[] temp = new T[Population.Length + genePool.Length];
            for(int i = 0;i < Population.Length;i++)
                temp[i] = Population[i];
            for(int i = 0;i < genePool.Length;i++)
                temp[Population.Length + i] = genePool[i];

            //Sort the population
            Array.Sort<Chromosome>(temp as Chromosome[], ChromosomeComparer.DescendingInstance);

            //Get new population
            for (int i = 0; i < Population.Length; i++)
                Population[i] = temp[i];
        }

        protected override void PerformShutdown()
        {
            JudgeStrategy.Judge(Population);
        }

        #endregion
    }
}
