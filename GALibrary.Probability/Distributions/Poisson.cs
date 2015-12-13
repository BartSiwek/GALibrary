using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace GALibrary.Probability.Distributions
{
    public class Poisson : Distribution
    {
        #region Storage

        private double lambda;

        #endregion

        #region Construction

        internal Poisson(string[] parameters)
        {
            if (parameters.Length != 1)
                ThrowBadParametersNumberException();

            this.lambda = double.Parse(parameters[0], CultureInfo.InvariantCulture);
        }

        public Poisson(double lambda)
        {
            this.lambda = lambda;
        }

        #endregion

        #region Properties

        public double Lambda
        {
            get { return lambda; }
            set { lambda = value; }
        }

        #endregion

        #region IDistribution members

        public override double Next()
        {
            double L = Math.Exp(-lambda);
            int k = 0;
            double p = 1;

            do
            {
                k++;
                p = p * RandomNumberGenerator.NextDouble();
            } while (p >= L);
            return k - 1;
        }

        public override string ToString()
        {
            return String.Format("Poisson({0})", lambda);
        }

        public override DistributionType DistributionType
        {
            get { return DistributionType.Poisson; }
        }

        #endregion
    }
}
