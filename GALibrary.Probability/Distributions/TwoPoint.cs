using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace GALibrary.Probability.Distributions
{
    public class TwoPoint : Distribution
    {
        #region Storage

        private double a;
        private double b;
        private double p;

        #endregion

        #region Construction}

        internal TwoPoint(string[] parameters)
        {
            if (parameters.Length != 3)
                ThrowBadParametersNumberException();

            this.a = double.Parse(parameters[0], CultureInfo.InvariantCulture);
            this.b = double.Parse(parameters[1], CultureInfo.InvariantCulture);
            this.p = double.Parse(parameters[2], CultureInfo.InvariantCulture);
        }

        public TwoPoint() : this(1, 0)
        { }

        public TwoPoint(double p) : this(1, 0, p)
        { }

        public TwoPoint(double a, double b) : this(a, b, 0.5)
        { }

        public TwoPoint(double a, double b, double p)
        {
            if (p <= 0 || p >= 1)
                throw new ArgumentException("Probability must be in a (0,1) range", "p");
            this.a = a;
            this.b = b;
            this.p = p;
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

        public double P
        {
            get { return p; }
            set { p = value; }
        }

        #endregion

        #region IDistribution members

        public override double Next()
        {
            double value = RandomNumberGenerator.NextDouble();
            if (value < p)
                return a;
            else
                return b;
        }

        public override string ToString()
        {
            return String.Format("TwoPoint({0},{1},{2})", a, b, p);
        }

        public override DistributionType DistributionType
        {
            get { return DistributionType.TwoPoint; }
        }

        #endregion
    }
}
