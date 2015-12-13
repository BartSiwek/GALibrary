using System;
using System.Collections;
using System.Text;
using GALibrary.Core;
using GALibrary.Probability;
using GALibrary.Common;

namespace GALibrary.SGA
{
    public class BinaryChromosomeGenerator : IChromosomeGenerator<BinaryChromosome>
    {
        #region Storage

        private int m_length;

        #endregion

        #region Construction

        public BinaryChromosomeGenerator()
        {
            m_length = 0;
        }

        public BinaryChromosomeGenerator(int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException("length", length, "Length must be greater than zero");
            m_length = length;
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

        #endregion

        #region IChromosomeGenerator<BinaryChromosome> Members

        public BinaryChromosome Generate()
        {
            BitArray ba = new BitArray(m_length);

            for (int i = 0; i < m_length; i++)
            {
                if (RandomNumberGenerator.NextDouble() < 0.5)
                    ba[i] = true;
                else
                    ba[i] = false;
            }

            return new BinaryChromosome(ba);
        }

        public void Initialize()
        { }

        #endregion
    }
}
