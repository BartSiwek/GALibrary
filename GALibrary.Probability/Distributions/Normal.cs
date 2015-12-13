using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace GALibrary.Probability.Distributions
{
    public class Normal : Distribution
    {
        #region Storage

        private double mean;
        private double sigma;

        #endregion

        #region Construction

        internal Normal(string[] parameters)
        {
            if (parameters.Length != 2)
                ThrowBadParametersNumberException();

            this.mean = double.Parse(parameters[0], CultureInfo.InvariantCulture);
            double sigmaSquare = double.Parse(parameters[1], CultureInfo.InvariantCulture);
            this.sigma = Math.Sqrt(sigmaSquare);
        }

        public Normal(double mean, double variance)
        {
            this.mean = mean;
            this.sigma = Math.Sqrt(variance);
        }

        public Normal() : this(0, 1)
        { }

        #endregion

        #region Private methods

        private double GetStandardNumber()
        {
            double x, y, rSquare;
            do 
            {
                x = 2 * RandomNumberGenerator.NextDouble() - 1;
                y = 2 * RandomNumberGenerator.NextDouble() - 1;

                rSquare = x * x + y * y;
            } while (rSquare > 1);

            double u = Math.Sqrt(-2 * Math.Log(rSquare) / rSquare) * x;
            double v = Math.Sqrt(-2 * Math.Log(rSquare) / rSquare) * x;

            if (RandomNumberGenerator.NextDouble() < 0.5)
                return u;
            else
                return v;
        }

        #endregion

        #region Properties

        public double Mean
        {
            get { return mean; }
            set { mean = value; }
        }

        public double Sigma
        {
            get { return sigma; }
            set { sigma = value; }
        }

        public double Variance
        {
            get { return sigma*sigma; }
            set { sigma = Math.Sqrt(value); }
        }

        #endregion

        #region IDistribution members

        public override double Next()
        {
            return sigma * GetStandardNumber() + mean;
        }

        public override string ToString()
        {
            return String.Format("Normal({0},{1})", mean, sigma * sigma);
        }

        public override DistributionType DistributionType
        {
            get { return DistributionType.Normal; }
        }

        #endregion
    }
}
