using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Core;
using GALibrary.Probability;

namespace GALibrary.Evolutionary.Chromsomes
{
    public class RealNumberChromosomeGenerator : IChromosomeGenerator<RealNumberChromosome>
    {
        #region Storage

        private int m_length;
        private double m_min;
        private double m_max;

        #endregion

        #region Construction

        public RealNumberChromosomeGenerator()
        {
            m_length = 0;
            m_min = 0;
            m_max = 1;
        }

        public RealNumberChromosomeGenerator(int length, double min, double max)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException("length", length, "Length must be greater than zero");
            m_length = length;
            m_min = min;
            m_max = max;
        }

        #endregion

        #region Properties

        public int Length
        {
            get { return m_length; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("value", value, "Length must be greater than zero");
                m_length = value;
            }
        }

        public double Min
        {
            get { return m_min; }
            set { m_min = value; }
        }

        public double Max
        {
            get { return m_max; }
            set { m_max = value; }
        }

        #endregion

        #region IChromosomeGenerator<RealNumberChromosome> Members

        public RealNumberChromosome Generate()
        {
            double[] genome = new double[m_length];

            for (int i = 0; i < m_length; i++)
            {
                double u = RandomNumberGenerator.NextDouble();
                genome[i] = (m_max - m_min) * u + m_min;
            }

            return new RealNumberChromosome(genome);
        }

        public void Initialize()
        { }

        #endregion
    }
}
