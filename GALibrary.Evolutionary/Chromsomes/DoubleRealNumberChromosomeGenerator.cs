using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Core;
using GALibrary.Probability;

namespace GALibrary.Evolutionary.Chromsomes
{
    public class DoubleRealNumberChromosomeGenerator : IChromosomeGenerator<DoubleRealNumberChromosome>
    {
        #region Storage

        private int m_length;
        private double m_min;
        private double m_max;
        private double m_sigmaMax;

        #endregion

        #region Construction

        public DoubleRealNumberChromosomeGenerator()
        {
            m_length = 0;
            m_min = 0;
            m_max = 1;
            m_sigmaMax = 1;
        }

        public DoubleRealNumberChromosomeGenerator(int length, double min, double max, double sigmaMax)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException("length", length, "Length must be greater than zero");
            if (sigmaMax <= 0)
                throw new ArgumentOutOfRangeException("sigmaMax", sigmaMax, "Maximal sigma be greater than zero");

            m_length = length;
            m_min = min;
            m_max = max;
            m_sigmaMax = sigmaMax;
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

        public double MaxSigma
        {
            get { return m_sigmaMax; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("value", value, "Maximal sigma be greater than zero");
                m_sigmaMax = value;
            }
        }

        #endregion

        #region IChromosomeGenerator<DoubleRealNumberChromosome> Members

        public DoubleRealNumberChromosome Generate()
        {
            double[] values = new double[m_length];
            double[] sigmas = new double[m_length];

            for (int i = 0; i < m_length; i++)
            {
                double u = RandomNumberGenerator.NextDouble();
                values[i] = (m_max - m_min) * u + m_min;

                double v = RandomNumberGenerator.NextDouble();
                sigmas[i] = m_sigmaMax * v;
            }

            return new DoubleRealNumberChromosome(values, sigmas);
        }

        public void Initialize()
        { }

        #endregion
    }
}
