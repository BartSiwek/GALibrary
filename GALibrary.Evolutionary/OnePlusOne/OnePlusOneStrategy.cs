using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Core;
using GALibrary.Common;

namespace GALibrary.Evolutionary.OnePlusOne
{
    public class OnePlusOneStrategy<T> : IterationalGAStrategy<T>
        where T : Chromosome
    {
        #region Construction

        public OnePlusOneStrategy()
            : base(1)
        { }

        #endregion

        #region IterationalGAStrategy implementation

        protected override void PerformInitialization()
        {
            //Check
            if (JudgeStrategy == null)
                throw new InvalidOperationException("The JudgeStrategy property cannot be null");
            if (MutationStrategy == null)
                throw new InvalidOperationException("The MutationStrategy property cannot be null");

            //Intiialize
            JudgeStrategy.Initialize();
            MutationStrategy.Initialize();

            //Judge first pop
            JudgeStrategy.Judge(Population);
        }

        protected override void PerformIteration()
        {
            T[] genePool = { (T)Population[0].Clone() };
            MutationStrategy.Mutate(ref genePool);
            JudgeStrategy.Judge(genePool);

            if (genePool[0].Fitness > Population[0].Fitness)
                Population = genePool;
        }

        protected override void PerformShutdown()
        {
            //Judge final pop
            JudgeStrategy.Judge(Population);
        }

        #endregion
    }
}
