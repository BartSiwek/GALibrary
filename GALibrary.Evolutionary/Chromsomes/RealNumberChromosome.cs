using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Core;

namespace GALibrary.Evolutionary.Chromsomes
{
    public class RealNumberChromosome : Chromosome
    {
        #region Storage

        double[] m_genome;

        #endregion

        #region Construction

        public RealNumberChromosome(double[] genome) : base()
        {
            m_genome = (double[])genome.Clone();
        }

        public RealNumberChromosome(RealNumberChromosome other) : base(other)
        {
            m_genome = (double[])other.m_genome.Clone();
        }

        #endregion

        #region Properties

        public double[] Genome
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
            return new RealNumberChromosome(this);
        }

        #endregion
    }
}
