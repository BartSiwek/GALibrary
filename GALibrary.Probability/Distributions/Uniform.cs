using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace GALibrary.Probability.Distributions
{
    public class Uniform : Distribution
    {
        #region Storage

        private double a;
        private double b;

        #endregion

        #region Construction

        internal Uniform(string[] parameters)
        {
            if (parameters.Length != 2)
                ThrowBadParametersNumberException();

            this.a = double.Parse(parameters[0], CultureInfo.InvariantCulture);
            this.b = double.Parse(parameters[1], CultureInfo.InvariantCulture);
        }

        public Uniform() : this(0, 1)
        { }

        public Uniform(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        #endregion

        #region Properties

        public double A
        {
            get { return a; }
            set { a = value; }
        }

        public double B
        {
            get { return b; }
            set { b = value; }
        }

        #endregion

        #region IDistribution members

        public override double Next()
        {
            return RandomNumberGenerator.NextDouble() * (b - a) + a;
        }

        public override string ToString()
        {
            return String.Format("Uniform({0},{1}", a, b);
        }

        public override DistributionType DistributionType
        {
            get { return DistributionType.Uniform; }
        }

        #endregion
    }
}
