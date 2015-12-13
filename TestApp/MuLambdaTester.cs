using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Common;
using GALibrary.Evolutionary;
using GALibrary.Evolutionary.Chromsomes;
using GALibrary.Evolutionary.Operations;
using GALibrary.Evolutionary.MuLambda;

namespace TestApp
{
    class MuLambdaJudgeClass : IOneChromosomeJudge<DoubleRealNumberChromosome>
    {
        #region IOneChromosomeJudge<T> Members

        public void Judge(DoubleRealNumberChromosome chromosome)
        {
            double value = AckleyFunction.GetValue(chromosome.Values);
            if(double.IsNaN(value))
                chromosome.SetValueAndFitness(24.9999, 0.0001);
            else
                chromosome.SetValueAndFitness(value, 25 - value);
        }

        public void Initialize()
        { }

        #endregion
    }

    class MuLambdaTester
    {
        private DoubleRealNumberChromosome best = null;

        private void OnIterationComeplete(object sender, EventArgs e)
        {
            //Get the args
            MuLambdaStrategy<DoubleRealNumberChromosome> ga =
                (MuLambdaStrategy<DoubleRealNumberChromosome>)sender;
            IterationalGAStrategyEventArgs iterE =
                (IterationalGAStrategyEventArgs)e;

            //Find the max
            int max = 0;
            for (int i = 1; i < ga.Population.Length; i++)
                if (ga.Population[i].Fitness > ga.Population[max].Fitness)
                    max = i;

            //Renember the max
            if (best == null)
                best = (DoubleRealNumberChromosome)ga.Population[max].Clone();
            else
            {
                if (best.Fitness < ga.Population[max].Fitness)
                    best = (DoubleRealNumberChromosome)ga.Population[max].Clone();
            }

            //Display the info
            if (iterE.Iteration % 500 == 0)
            {
                Console.WriteLine("Best at {0}: {1} ({2})", iterE.Iteration, best.Value, best.Fitness);
            }
        }

        public void Run()
        {
            Console.WriteLine("Mu,Lambda #1 (LocalAverage)");
            best = null;

            //Create the strategy
            MuLambdaStrategy<DoubleRealNumberChromosome> ga1 =
                new MuLambdaStrategy<DoubleRealNumberChromosome>(1000, 1500);
            //Assign a generator
            ga1.ChromosomeGenerator =
                new DoubleRealNumberChromosomeGenerator(AckleyFunction.Dimentions, -30, 30, 10);
            //Assign a crossover
            ga1.CrossoverStrategy =
                new LocalAverageCrossover();
            //Assign a judge
            ga1.JudgeStrategy =
                new AutonomousJudgeStrategy<DoubleRealNumberChromosome>(
                    new MuLambdaJudgeClass()
                );
            //Assign a mutation
            ga1.MutationStrategy =
                new OneChromosomeMutationStrategy<DoubleRealNumberChromosome>(
                    new EvolutionaryMutation()
                );
            //Assign a selection
            ga1.SelectionStrategy =
                new ProportionalSelectionStrategy<DoubleRealNumberChromosome>();
            //Assign a stop condition
            ga1.StopCondition =
                new IterationNumberStopCondition(2500);

            //Subscribe to a event
            ga1.ReportProgress += OnIterationComeplete;
            ga1.Run();

            //Print result
            Console.WriteLine("Mu,Lambda Best: {0}", best.Value);

            Console.WriteLine("Mu,Lambda #2 (LocalDiscrete)");
            best = null;

            //Create the strategy
            MuLambdaStrategy<DoubleRealNumberChromosome> ga2 =
                new MuLambdaStrategy<DoubleRealNumberChromosome>(1000, 1500);
            //Assign a generator
            ga2.ChromosomeGenerator =
                new DoubleRealNumberChromosomeGenerator(AckleyFunction.Dimentions, -30, 30, 10);
            //Assign a crossover
            ga2.CrossoverStrategy =
                new LocalDiscreteCrossover();
            //Assign a judge
            ga2.JudgeStrategy =
                new AutonomousJudgeStrategy<DoubleRealNumberChromosome>(
                    new MuLambdaJudgeClass()
                );
            //Assign a mutation
            ga2.MutationStrategy =
                new OneChromosomeMutationStrategy<DoubleRealNumberChromosome>(
                    new EvolutionaryMutation()
                );
            //Assign a selection
            ga2.SelectionStrategy =
                new ProportionalSelectionStrategy<DoubleRealNumberChromosome>();
            //Assign a stop condition
            ga2.StopCondition =
                new IterationNumberStopCondition(2500);

            //Subscribe to a event
            ga2.ReportProgress += OnIterationComeplete;
            ga2.Run();

            //Print result
            Console.WriteLine("Mu,Lambda Best: {0}", best.Value);

            Console.WriteLine("Mu,Lambda #3 (GlobalAverage)");
            best = null;

            //Create the strategy
            MuLambdaStrategy<DoubleRealNumberChromosome> ga3 =
                new MuLambdaStrategy<DoubleRealNumberChromosome>(1000, 1500);
            //Assign a generator
            ga3.ChromosomeGenerator =
                new DoubleRealNumberChromosomeGenerator(AckleyFunction.Dimentions, -30, 30, 10);
            //Assign a crossover
            ga3.CrossoverStrategy =
                new GlobalAverageCrossover(AckleyFunction.Dimentions);
            //Assign a judge
            ga3.JudgeStrategy =
                new AutonomousJudgeStrategy<DoubleRealNumberChromosome>(
                    new MuLambdaJudgeClass()
                );
            //Assign a mutation
            ga3.MutationStrategy =
                new OneChromosomeMutationStrategy<DoubleRealNumberChromosome>(
                    new EvolutionaryMutation()
                );
            //Assign a selection
            ga3.SelectionStrategy =
                new ProportionalSelectionStrategy<DoubleRealNumberChromosome>();
            //Assign a stop condition
            ga3.StopCondition =
                new IterationNumberStopCondition(2500);

            //Subscribe to a event
            ga3.ReportProgress += OnIterationComeplete;
            ga3.Run();

            //Print result
            Console.WriteLine("Mu,Lambda Best: {0}", best.Value);

            Console.WriteLine("Mu,Lambda #4 (GlobalDiscrete)");
            best = null;

            //Create the strategy
            MuLambdaStrategy<DoubleRealNumberChromosome> ga4 =
                new MuLambdaStrategy<DoubleRealNumberChromosome>(1000, 1500);
            //Assign a generator
            ga4.ChromosomeGenerator =
                new DoubleRealNumberChromosomeGenerator(AckleyFunction.Dimentions, -30, 30, 10);
            //Assign a crossover
            ga4.CrossoverStrategy =
                new GlobalDiscreteCrossover(AckleyFunction.Dimentions);
            //Assign a judge
            ga4.JudgeStrategy =
                new AutonomousJudgeStrategy<DoubleRealNumberChromosome>(
                    new MuLambdaJudgeClass()
                );
            //Assign a mutation
            ga4.MutationStrategy =
                new OneChromosomeMutationStrategy<DoubleRealNumberChromosome>(
                    new EvolutionaryMutation()
                );
            //Assign a selection
            ga4.SelectionStrategy =
                new ProportionalSelectionStrategy<DoubleRealNumberChromosome>();
            //Assign a stop condition
            ga4.StopCondition =
                new IterationNumberStopCondition(2500);

            //Subscribe to a event
            ga4.ReportProgress += OnIterationComeplete;
            ga4.Run();

            //Print result
            Console.WriteLine("Mu,Lambda Best: {0}", best.Value);
        }
    }
}
