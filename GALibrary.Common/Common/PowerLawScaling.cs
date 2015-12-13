using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Core;

namespace GALibrary.Common
{
    public class PowerLawScaling<T> : IJudgeStrategy<T>
        where T : Chromosome
    {
        #region Storage

        private IJudgeStrategy<T> m_judgeStrategy;
        private double m_exponent;

        #endregion

        #region Construction

        public PowerLawScaling(IJudgeStrategy<T> judgeStrategy, double exponent)
        {
            m_judgeStrategy = judgeStrategy;
            m_exponent = exponent;
        }

        public PowerLawScaling(IJudgeStrategy<T> judgeStrategy) : this(judgeStrategy, 1.005)
        { }

        public PowerLawScaling(double exponent) : this(null, exponent)
        { }

        public PowerLawScaling() : this(null, 1.005)
        { }

        #endregion

        #region Properties 

        public IJudgeStrategy<T> JudgeStrategy
        {
            get { return m_judgeStrategy; }
            set { m_judgeStrategy = value; }
        }

        public double Exponent
        {
            get { return m_exponent; }
            set { m_exponent = value; }
        }

        #endregion

        #region IJudgeStrategy<T> Members

        public void Judge(T[] population)
        {
            m_judgeStrategy.Judge(population);

            //Perform scaling
            for (int i = 0; i < population.Length; i++)
            {
                double value = population[i].Value;
                double fitness = population[i].Fitness;
                population[i].SetValueAndFitness(value, Math.Pow(fitness, m_exponent));
            }
        }

        public void Initialize()
        {
            m_judgeStrategy.Initialize();
        }

        #endregion
    }
}
