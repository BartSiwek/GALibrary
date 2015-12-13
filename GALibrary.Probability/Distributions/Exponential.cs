using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace GALibrary.Probability.Distributions
{
    public class Exponential : Distribution
    {
        #region Storage

        private double mean;

        #endregion

        #region Construction

        internal Exponential(string[] parameters)
        {
            if (parameters.Length != 1)
                ThrowBadParametersNumberException();

            this.mean = double.Parse(parameters[0], CultureInfo.InvariantCulture);
        }

        public Exponential(double mean)
        {
            this.mean = mean;
        }

        #endregion

        #region Properties

        public double Mean
        {
            get { return mean; }
            set { mean = value; }
        }

        #endregion

        #region IDistribution members

        public override double Next()
        {
            double value = RandomNumberGenerator.NextDouble();
            return -mean * Math.Log(1 - value);
        }

        public override string ToString()
        {
            return String.Format("Exponential({0})", mean);
        }

        public override DistributionType DistributionType
        {
            get { return DistributionType.Exponential; }
        }

        #endregion
    }
}
