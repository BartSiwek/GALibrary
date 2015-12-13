using System;
using System.Collections;
using System.Text;
using GALibrary.Common;
using GALibrary.SGA;

namespace TestApp
{
    class SGAJudgeClass : IOneChromosomeJudge<BinaryChromosome>
    {
        #region Constants

        public const int BitsForDimension = 64;

        #endregion

        #region Methods

        private double GetValue(BitArray ba)
        {
            double value = 0;
            double pow = 1;
            for (int i = 0; i < ba.Count; i++)
            {
                if (ba[i])
                    value += pow;
                pow *= 2;
            }

            return value/(Math.Pow(2, ba.Count) - 1);
        }

        #endregion

        #region IOneChromosomeJudge<T> Members

        public void Judge(BinaryChromosome chromosome)
        {
            int dims = chromosome.Genome.Length / BitsForDimension;
            double[] values = new double[dims];

            for (int i = 0; i < dims; i++)
            {
                BitArray bits = new BitArray(BitsForDimension);
                for (int j = 0; j < BitsForDimension; j++)
                    bits[j] = chromosome.Genome[i * BitsForDimension + j];

                double x = GetValue(bits);
                values[i] = x;
            }

            double value = AckleyFunction.GetValue(values);
            if (double.IsNaN(value))
                chromosome.SetValueAndFitness(24.9999, 0.0001);
            else
                chromosome.SetValueAndFitness(value, 25 - value);
        }

        public void Initialize()
        { }

        #endregion
    }

    class SGATester
    {
        private BinaryChromosome best = null;

        private void OnIterationComeplete(object sender, EventArgs e)
        {
            //Get the args
            SGAStrategy<BinaryChromosome> ga =
                (SGAStrategy<BinaryChromosome>)sender;
            IterationalGAStrategyEventArgs iterE =
                (IterationalGAStrategyEventArgs)e;

            //Find the max
            int max = 0;
            for (int i = 1; i < ga.Population.Length; i++)
                if (ga.Population[i].Fitness > ga.Population[max].Fitness)
                    max = i;

            //Renember the max
            if (best == null)
                best = (BinaryChromosome)ga.Population[max].Clone();
            else
            {
                if (best.Fitness < ga.Population[max].Fitness)
                    best = (BinaryChromosome)ga.Population[max].Clone();
            }

            //Display the info
            if (iterE.Iteration % 500 == 0)
            {
                Console.WriteLine("Best at {0}: {1} ({2})", iterE.Iteration, best.Value, best.Fitness);
            }
        }

        public void Run()
        {
            Console.WriteLine("SGA #1 (ProportionalSelection)");
            best = null;

            //Create the strategy
            SGAStrategy<BinaryChromosome> ga1 =
                new SGAStrategy<BinaryChromosome>(100);
            //Assign a generator
            ga1.ChromosomeGenerator =
                new BinaryChromosomeGenerator(AckleyFunction.Dimentions * SGAJudgeClass.BitsForDimension);
            //Assign a crossover
            ga1.CrossoverStrategy =
                new TwoChromosomeCrossoverStrategy<BinaryChromosome>(
                    new OnePointCrossover(0.7)
                );
            //Assign a judge
            ga1.JudgeStrategy =
                new AutonomousJudgeStrategy<BinaryChromosome>(
                    new SGAJudgeClass()
                );
            //Assign a mutation
            ga1.MutationStrategy =
                new OneChromosomeMutationStrategy<BinaryChromosome>(
                    new SGAMutation(0.01)
                );
            //Assign a selection
            ga1.SelectionStrategy =
                new ProportionalSelectionStrategy<BinaryChromosome>();
            //Assign a stop condition
            ga1.StopCondition =
                new IterationNumberStopCondition(10000);

            //Subscribe to a event
            ga1.ReportProgress += OnIterationComeplete;
            ga1.Run();

            //Print result
            Console.WriteLine("SGA #1 Best: {0}", best.Value);

            Console.WriteLine("SGA #2 (TournamentSelection)");
            best = null;

            //Create the strategy
            SGAStrategy<BinaryChromosome> ga2 =
                new SGAStrategy<BinaryChromosome>(100);
            //Assign a generator
            ga2.ChromosomeGenerator =
                new BinaryChromosomeGenerator(AckleyFunction.Dimentions * SGAJudgeClass.BitsForDimension);
            //Assign a crossover
            ga2.CrossoverStrategy =
                new TwoChromosomeCrossoverStrategy<BinaryChromosome>(
                    new OnePointCrossover(0.7)
                );
            //Assign a judge
            ga2.JudgeStrategy =
                new AutonomousJudgeStrategy<BinaryChromosome>(
                    new SGAJudgeClass()
                );
            //Assign a mutation
            ga2.MutationStrategy =
                new OneChromosomeMutationStrategy<BinaryChromosome>(
                    new SGAMutation(0.01)
                );
            //Assign a selection
            ga2.SelectionStrategy =
                new TournamentSelection<BinaryChromosome>(2);
            //Assign a stop condition
            ga2.StopCondition =
                new IterationNumberStopCondition(10000);

            //Subscribe to a event
            ga2.ReportProgress += OnIterationComeplete;
            ga2.Run();

            //Print result
            Console.WriteLine("SGA #2 Best: {0}", best.Value);
            
            Console.WriteLine("SGA #3 (RankingSelection - Linear)");
            best = null;

            //Create the strategy
            SGAStrategy<BinaryChromosome> ga3 =
                new SGAStrategy<BinaryChromosome>(100);
            //Assign a generator
            ga3.ChromosomeGenerator =
                new BinaryChromosomeGenerator(AckleyFunction.Dimentions * SGAJudgeClass.BitsForDimension);
            //Assign a crossover
            ga3.CrossoverStrategy =
                new TwoChromosomeCrossoverStrategy<BinaryChromosome>(
                    new OnePointCrossover(0.7)
                );
            //Assign a judge
            ga3.JudgeStrategy =
                new AutonomousJudgeStrategy<BinaryChromosome>(
                    new SGAJudgeClass()
                );
            //Assign a mutation
            ga3.MutationStrategy =
                new OneChromosomeMutationStrategy<BinaryChromosome>(
                    new SGAMutation(0.01)
                );
            //Assign a selection
            ga3.SelectionStrategy =
                new RankingSelection<BinaryChromosome>(
                    new LinearRankBasedProbabilityComputator(1.5)
                );
            //Assign a stop condition
            ga3.StopCondition =
                new IterationNumberStopCondition(10000);

            //Subscribe to a event
            ga3.ReportProgress += OnIterationComeplete;
            ga3.Run();

            //Print result
            Console.WriteLine("SGA #3 Best: {0}", best.Value);
            
            Console.WriteLine("SGA #4 (RankingSelection - Geoemtric)");
            best = null;

            //Create the strategy
            SGAStrategy<BinaryChromosome> ga4 =
                new SGAStrategy<BinaryChromosome>(100);
            //Assign a generator
            ga4.ChromosomeGenerator =
                new BinaryChromosomeGenerator(AckleyFunction.Dimentions * SGAJudgeClass.BitsForDimension);
            //Assign a crossover
            ga4.CrossoverStrategy =
                new TwoChromosomeCrossoverStrategy<BinaryChromosome>(
                    new OnePointCrossover(0.7)
                );
            //Assign a judge
            ga4.JudgeStrategy =
                new AutonomousJudgeStrategy<BinaryChromosome>(
                    new SGAJudgeClass()
                );
            //Assign a mutation
            ga4.MutationStrategy =
                new OneChromosomeMutationStrategy<BinaryChromosome>(
                    new SGAMutation(0.01)
                );
            //Assign a selection
            ga4.SelectionStrategy =
                new RankingSelection<BinaryChromosome>(
                    new GeometricRankBasedProbabilityComputator(0.1)
                );
            //Assign a stop condition
            ga4.StopCondition =
                new IterationNumberStopCondition(10000);

            //Subscribe to a event
            ga4.ReportProgress += OnIterationComeplete;
            ga4.Run();

            //Print result
            Console.WriteLine("SGA #4 Best: {0}", best.Value);
        }
    }
}
