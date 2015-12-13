using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Core;

namespace GALibrary.Common
{
    public class LinearScaling<T> : IJudgeStrategy<T>
        where T : Chromosome
    {
        #region Storage

        private IJudgeStrategy<T> m_judgeStrategy;
        private double m_mult;

        #endregion

        #region Construction

        public LinearScaling(IJudgeStrategy<T> judgeStrategy, double multiplier)
        {
            if (multiplier <= 1)
                throw new ArgumentOutOfRangeException("multiplier", multiplier, "The multiplier must be greater than one");
            m_judgeStrategy = judgeStrategy;
            m_mult = multiplier;
        }

        public LinearScaling(IJudgeStrategy<T> judgeStrategy) : this(judgeStrategy, 1.6)
        { }

        public LinearScaling(double multiplier) : this(null, multiplier)
        { }

        public LinearScaling() : this(null, 1.6)
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
            set
            {
                if (value <= 1)
                    throw new ArgumentOutOfRangeException("value", value, "The multiplier must be greater than one");
                m_mult = value;
            }
        }

        #endregion

        #region IJudgeStrategy<T> Members

        public void Judge(T[] population)
        {
            m_judgeStrategy.Judge(population);
            
            //Compute the scaling parameters
            double avg = 0;
            double min = double.PositiveInfinity;
            double max = double.NegativeInfinity;

            for (int i = 0; i < population.Length; i++)
            {
                avg += population[i].Fitness;
                if (min > population[i].Fitness)
                    min = population[i].Fitness;
                if (max < population[i].Fitness)
                    max = population[i].Fitness;
            }

            avg = avg / population.Length;

            //Compute the scaling paramters
            double a, b;
            ComputeScalingParameters(min, avg, max, out a, out b);

            //Scale
            for (int i = 0; i < population.Length; i++)
            {
                double fitness = population[i].Fitness;
                double value = population[i].Value;
                population[i].SetValueAndFitness(value, a * fitness + b);
            }
        }

        public void Initialize()
        {
            m_judgeStrategy.Initialize();
        }

        #endregion

        #region Private methods

        private void ComputeScalingParameters(double min, double avg, double max, out double a, out double b)
        {
            if (min > (m_mult * avg - max) / (m_mult - 1))
            {
                double delta = max - avg;
                a = (m_mult - 1) * avg / delta;
                b = avg * (max - m_mult * avg) / delta;
            }
            else
            {
                double delta = avg - min;
                a = avg / delta;
                b = -min * avg / delta;
            }
        }

        #endregion
    }
}
