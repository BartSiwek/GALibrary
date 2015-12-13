using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Core;

namespace GALibrary.Common
{
    public abstract class IterationalGAStrategy<T> : GAStrategy<T>
        where T : Chromosome
    {
        #region Construction

        public IterationalGAStrategy(int populationSize) : base(populationSize)
        { }

        #endregion

        #region GAStrategy implementation

        public override void Run()
        {
            if (ChromosomeGenerator == null)
                throw new InvalidOperationException("The ChromosomeGenerator property cannot be null");
            if (StopCondition == null)
                throw new InvalidOperationException("The StopCondition property cannot be null");

            ChromosomeGenerator.Initialize();
            StopCondition.Initialize();

            //Initialize
            for (int i = 0; i < Population.Length; i++)
            {
                Population[i] = ChromosomeGenerator.Generate();
            }

            //Perform derived class initialization
            PerformInitialization();
            NotifyReportProgress(new IterationalGAStrategyEventArgs(0));

            //Prepare for iteration
            int iteration = 0;
            StopCondition.Initialize();

            //Iterate
            while (!StopCondition.Stop(Population as Chromosome[]))
            {
                iteration++;
                PerformIteration();
                NotifyReportProgress(new IterationalGAStrategyEventArgs(iteration));
            }

            //End the starategy
            PerformShutdown();
        }

        #endregion

        #region Abstract methods

        protected abstract void PerformInitialization();
        protected abstract void PerformIteration();
        protected abstract void PerformShutdown();

        #endregion
    }
}
