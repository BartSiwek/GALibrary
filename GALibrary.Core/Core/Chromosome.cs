using System;
using System.Collections.Generic;
using System.Text;

namespace GALibrary.Core
{
    public abstract class Chromosome : ICloneable
    {
        #region Storage

        private double m_fitness;
        private double m_value;

        #endregion

        #region Construction

        public Chromosome()
        {
            m_fitness = double.NaN;
            m_value = double.NaN;
        }

        protected Chromosome(Chromosome other)
        {
            m_fitness = other.m_fitness;
            m_value = other.m_value;
        }

        #endregion

        #region Properties

        public double Fitness 
        {
            get { return m_fitness; }
        }

        public double Value 
        {
            get { return m_value; }
        }

        #endregion

        #region Methods

        public void SetValueAndFitness(double value, double fitness)
        {
            m_value = value;
            m_fitness = fitness;
        }

        #endregion

        #region ICloneable Members

        public abstract object Clone();

        #endregion
    }
}
