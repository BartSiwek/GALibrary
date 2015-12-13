using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Common;
using GALibrary.Evolutionary;
using GALibrary.Evolutionary.Chromsomes;
using GALibrary.Evolutionary.OnePlusOne;

namespace TestApp
{
    class OnePlusOneJudgeClass : IOneChromosomeJudge<RealNumberChromosome>
    {
        #region IOneChromosomeJudge<T> Members

        public void Judge(RealNumberChromosome chromosome)
        {
            double value = AckleyFunction.GetValue(chromosome.Genome);
            if (double.IsNaN(value))
                chromosome.SetValueAndFitness(24.9999, 0.0001);
            else
                chromosome.SetValueAndFitness(value, 25 - value);
        }

        public void Initialize()
        { }

        #endregion
    }

    class OnePlusOneTester
    {
        private RealNumberChromosome best = null;

        private void OnIterationComeplete(object sender, EventArgs e)
        {
            //Get the args
            OnePlusOneStrategy<RealNumberChromosome> ga =
                (OnePlusOneStrategy<RealNumberChromosome>)sender;
            IterationalGAStrategyEventArgs iterE =
                (IterationalGAStrategyEventArgs)e;

            //Find the max
            int max = 0;
            for (int i = 1; i < ga.Population.Length; i++)
                if (ga.Population[i].Fitness > ga.Population[max].Fitness)
                    max = i;

            //Renember the max
            if (best == null)
                best = (RealNumberChromosome)ga.Population[max].Clone();
            else
            {
                if (best.Fitness < ga.Population[max].Fitness)
                    best = (RealNumberChromosome)ga.Population[max].Clone();
            }

            //Display the info
            if (iterE.Iteration % 1000 == 0)
            {
                Console.WriteLine("Best at {0}: {1} ({2})", iterE.Iteration, best.Value, best.Fitness);
            }
        }

        public void Run()
        {
            Console.WriteLine("OnePlusOne #1");
            best = null;

            //Create the strategy
            OnePlusOneStrategy<RealNumberChromosome> ga1 =
                new OnePlusOneStrategy<RealNumberChromosome>();
            //Assign a generator
            ga1.ChromosomeGenerator =
                new RealNumberChromosomeGenerator(AckleyFunction.Dimentions, -30, 30);
            //Assign a judge
            ga1.JudgeStrategy =
                new AutonomousJudgeStrategy<RealNumberChromosome>(
                    new OnePlusOneJudgeClass()
                );
            //Assign a mutation
            ga1.MutationStrategy =
                new OneChromosomeMutationStrategy<RealNumberChromosome>(
                    new OnePlusOneMutation(1)
                );
            //Assign a stop condition
            ga1.StopCondition =
                new IterationNumberStopCondition(10000);

            //Subscribe to a event
            ga1.ReportProgress += OnIterationComeplete;
            ga1.Run();

            //Print result
            Console.WriteLine("OnePlusOne Best: {0}", best.Value);

            Console.WriteLine("OnePlusOne #2");
            best = null;

            //Create the strategy
            OnePlusOneStrategy<RealNumberChromosome> ga2 =
                new OnePlusOneStrategy<RealNumberChromosome>();
            //Assign a generator
            ga2.ChromosomeGenerator =
                new RealNumberChromosomeGenerator(AckleyFunction.Dimentions, -30, 30);
            //Assign a judge
            ga2.JudgeStrategy =
                new AutonomousJudgeStrategy<RealNumberChromosome>(
                    new OnePlusOneJudgeClass()
                );
            //Assign a mutation
            ga2.MutationStrategy =
                new OneChromosomeMutationStrategy<RealNumberChromosome>(
                    new OnePlusOneMutationWithAdaptation(1, 5)
                );
            //Assign a stop condition
            ga2.StopCondition =
                new IterationNumberStopCondition(10000);

            //Subscribe to a event
            ga2.ReportProgress += OnIterationComeplete;
            ga2.Run();

            //Print result
            Console.WriteLine("OnePlusOne Best: {0}", best.Value);
        }
    }
}
