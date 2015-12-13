using System;
using System.Collections;
using System.Text;
using GALibrary.Core;

namespace GALibrary.SGA
{
    public class BinaryChromosome : Chromosome
    {
        #region Storage

        private BitArray m_genome;

        #endregion

        #region Construction

        public BinaryChromosome(BitArray genome) : base()
        {
            m_genome = (BitArray)genome.Clone();
        }

        private BinaryChromosome(BinaryChromosome other) : base(other)
        {
            m_genome = (BitArray)other.m_genome.Clone();
        }

        #endregion

        #region Properties

        public BitArray Genome
        {
            get
            {
                return m_genome;
            }
        }

        #endregion

        #region ICloneable Members

        public override object Clone()
        {
            return new BinaryChromosome(this);
        }

        #endregion
    }
}
