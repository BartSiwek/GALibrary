using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Core;

namespace GALibrary.Evolutionary.Chromsomes
{
    public class DoubleRealNumberChromosome : Chromosome
    {
        #region Storage

        double[] m_values;
        double[] m_sigmas;

        #endregion

        #region Construction

        public DoubleRealNumberChromosome(double[] values, double[] sigmas) : base()
        {
            m_values = (double[])values.Clone();
            m_sigmas = (double[])sigmas.Clone();
        }

        public DoubleRealNumberChromosome(DoubleRealNumberChromosome other) : base(other)
        {
            m_values = (double[])other.m_values.Clone();
            m_sigmas = (double[])other.m_sigmas.Clone();
        }

        #endregion

        #region Properties

        public double[] Values
        {
            get
            {
                return m_values;
            }
        }

        public double[] Sigmas
        {
            get
            {
                return m_sigmas;
            }
        }

        #endregion

        #region ICloneable Members

        public override object Clone()
        {
            return new DoubleRealNumberChromosome(this);
        }

        #endregion
    }
}
