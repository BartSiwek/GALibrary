using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Core;
using GALibrary.Common;

namespace GALibrary.SGA
{
    public class SGAStrategy<T> : IterationalGAStrategy<T>
        where T : Chromosome
    {
        #region Storage

        private int m_genePoolSize;

        #endregion

        #region Construction

        public SGAStrategy(int populationSize) : base(populationSize)
        { }

        #endregion

        #region IterationalGAStrategy implementation

        protected override void PerformInitialization()
        {
            //Check for needed strategies
            if(JudgeStrategy == null)
                throw new InvalidOperationException("The JudgeStrategy property cannot be null");
            if(SelectionStrategy == null)
                throw new InvalidOperationException("The SelectionStrategy property cannot be null");
            if(CrossoverStrategy == null)
                throw new InvalidOperationException("The CrossoverStrategy property cannot be null");
            if(MutationStrategy == null)
                throw new InvalidOperationException("The MutationStrategy property cannot be null");

            //Compute the required gene pool size
            int needed = Population.Length;
            if (needed % MutationStrategy.SingleOperationOutputs != 0)
                throw new InvalidOperationException("Current population size is not suitable for chosen mutation strategy");

            needed = (needed / MutationStrategy.SingleOperationOutputs) * MutationStrategy.SingleOperationInputs;
            if(needed % CrossoverStrategy.SingleOperationOutputs != 0)
                throw new InvalidOperationException("Current population size is not suitable for chosen crossover strategy");

            m_genePoolSize = (needed / CrossoverStrategy.SingleOperationOutputs) * CrossoverStrategy.SingleOperationInputs;           

            //Initialize the strategies
            JudgeStrategy.Initialize();
            SelectionStrategy.Initialize();
            CrossoverStrategy.Initialize();
            MutationStrategy.Initialize();

            //Judge the first population
            JudgeStrategy.Judge(Population);
        }

        protected override void PerformIteration()
        {
            JudgeStrategy.Judge(Population);
            T[] genePool = SelectionStrategy.Select(Population, m_genePoolSize);
            CrossoverStrategy.Crossover(ref genePool);
            MutationStrategy.Mutate(ref genePool);
            Population = genePool;
        }

        protected override void PerformShutdown()
        {
            JudgeStrategy.Judge(Population);
        }

        #endregion
    }
}
