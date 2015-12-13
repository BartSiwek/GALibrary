using System;
using System.Collections.Generic;
using System.Text;

namespace GALibrary.Common
{
    public class LinearRankBasedProbabilityComputator : IRankBasedProbabilityComputator
    {
        #region Storage

        private double m_selectionPressure;

        #endregion

        #region Construction

        public LinearRankBasedProbabilityComputator(double selectionPressure)
        {
            if (selectionPressure < 1 || selectionPressure > 2)
                throw new ArgumentOutOfRangeException("selectionPressure", selectionPressure, "The selection pressure must be between 1.0 and 2.0");
            m_selectionPressure = selectionPressure;
        }

        public LinearRankBasedProbabilityComputator() : this(1.5)
        { }

        #endregion

        #region Properties

        public double SelectionPressure
        {
            get { return m_selectionPressure; }
            set
            {
                if (value < 1 || value > 2)
                    throw new ArgumentOutOfRangeException("value", value, "The selection pressure must be between 1.0 and 2.0");
                m_selectionPressure = value;
            }
        }

        #endregion

        #region IRankBasedProbabilityComputator Members

        public double[] ComputeProbabilities(int maxRank)
        {
            double r, q;

            //Compute a, k, maxRank
            ComputeParameters(out r, out q, maxRank);

            //Compute the probabilities
            double[] probabilities = new double[maxRank];
            for (int i = 0; i < maxRank; i++)
                probabilities[i] = q - i * r;

            return probabilities;
        }

        public void Initialize()
        { }

        #endregion

        #region Private methods

        private void ComputeParameters(out double r, out double q, int maxRank)
        {
            q = m_selectionPressure / maxRank;
            r = 2 * (m_selectionPressure - 1) / (maxRank * (maxRank - 1));
        }

        #endregion
    }
}
