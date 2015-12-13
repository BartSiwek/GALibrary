using System;
using System.Collections.Generic;
using System.Text;

namespace GALibrary.Common
{
    public class GeometricRankBasedProbabilityComputator : IRankBasedProbabilityComputator
    {
        #region Storage

        private double m_selectionPressure;

        #endregion

        #region Construction

        public GeometricRankBasedProbabilityComputator(double selectionPressure)
        {
            if (selectionPressure <= 0 || selectionPressure >= 1)
                throw new ArgumentOutOfRangeException("selectionPressure", selectionPressure, "The selection pressure must be between 0.0 and 1.0 (exclusive)");
            m_selectionPressure = selectionPressure;
        }

        public GeometricRankBasedProbabilityComputator() : this(0.1)
        { }

        #endregion

        #region Properties

        public double SelectionPressure
        {
            get { return m_selectionPressure; }
            set
            {
                if (value <= 0 || value >= 1)
                    throw new ArgumentOutOfRangeException("value", value, "The selection pressure must be between 1.0 and 2.0");
                m_selectionPressure = value;
            }
        }

        #endregion

        #region IRankBasedProbabilityComputator Members

        public double[] ComputeProbabilities(int maxRank)
        {
            //Compute the probabilities
            double[] probabilities = new double[maxRank];
            for (int i = 0; i < maxRank; i++)
                probabilities[i] = m_selectionPressure * Math.Pow(1 - m_selectionPressure, i);

            return probabilities;
        }

        public void Initialize()
        { }

        #endregion
    }
}
