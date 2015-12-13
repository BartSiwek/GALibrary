using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace GALibrary.Probability.Distributions
{
    public class OnePoint : Distribution
    {
        #region Storage

        private double value;

        #endregion

        #region Construction

        internal OnePoint(string[] parameters)
        {
            if (parameters.Length != 1)
                ThrowBadParametersNumberException();

            this.value = double.Parse(parameters[0], CultureInfo.InvariantCulture);
        }

        public OnePoint(double value)
        {
            this.value = value;
        }

        #endregion

        #region Properties

        public double Value
        {
            get { return value; }
            set { this.value = value; }
        }

        #endregion

        #region IDistribution members

        public override double Next()
        {
            return value;
        }

        public override string ToString()
        {
            return String.Format("OnePoint({0})", value);
        }

        public override DistributionType DistributionType
        {
            get { return DistributionType.OnePoint; }
        }

        #endregion
    }
}
