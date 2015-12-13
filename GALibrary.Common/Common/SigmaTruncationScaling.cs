using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Core;

namespace GALibrary.Common
{
    public class SigmaTruncationScaling<T> : IJudgeStrategy<T>
        where T : Chromosome
    {
        #region Storage

        private IJudgeStrategy<T> m_judgeStrategy;
        private double m_mult;

        #endregion

        #region Construction

        public SigmaTruncationScaling(IJudgeStrategy<T> judgeStrategy, double multiplier)
        {
            m_judgeStrategy = judgeStrategy;
            m_mult = multiplier;
        }

        public SigmaTruncationScaling(IJudgeStrategy<T> judgeStrategy) : this(judgeStrategy, 3)
        { }

        public SigmaTruncationScaling(double multiplier) : this(null, multiplier)
        { }

        public SigmaTruncationScaling() : this(null, 3)
        { }

        #endregion

        #region Properties

        public IJudgeStrategy<T> JudgeStrategy
        {
            get { return m_judgeStrategy; }
            set { m_judgeStrategy = value; }
        }

        public double Multiplier
        {
            get { return m_mult; }
            set { m_mult = value; }
        }

        #endregion

        #region IJudgeStrategy<T> Members

        public void Judge(T[] population)
        {
            m_judgeStrategy.Judge(population);

            //Compute the scaling parameters
            double avg = 0;
            double sigmaSquare = 0;
            double sigma;

            for (int i = 0; i < population.Length; i++)
                avg += population[i].Fitness;
            avg = avg / population.Length;

            for (int i = 0; i < population.Length; i++)
                sigmaSquare += Math.Pow(population[i].Fitness - avg, 2);
            sigmaSquare = sigmaSquare / (population.Length - 1);

            sigma = Math.Sqrt(sigmaSquare);

            //Scale the fitness
            for (int i = 0; i < population.Length; i++)
            {
                double value = population[i].Value;
                double fitness = population[i].Fitness;
                double newFitness = Math.Max(0, fitness + (avg - m_mult * sigma));
                population[i].SetValueAndFitness(value, newFitness);
            }
        }

        public void Initialize()
        {
            m_judgeStrategy.Initialize();
        }

        #endregion
    }
}
