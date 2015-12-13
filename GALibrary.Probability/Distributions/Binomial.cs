using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace GALibrary.Probability.Distributions
{
    public class Binomial : Distribution
    {
        #region Storage

        private int n;
        private TwoPoint twoPointDistribution;

        #endregion

        #region Construction

        internal Binomial(string[] parameters)
        {
            if (parameters.Length != 2)
                ThrowBadParametersNumberException();

            this.n = int.Parse(parameters[0]);
            this.twoPointDistribution = new TwoPoint(double.Parse(parameters[1], CultureInfo.InvariantCulture));
        }

        public Binomial(int n, double succesProbability)
        {
            this.n = n;
            this.twoPointDistribution = new TwoPoint(succesProbability);
        }

        #endregion

        #region Properties

        public int N
        {
            get { return n; }
            set { n = value; }
        }

        public double SuccesProbability
        {
            get { return twoPointDistribution.P; }
            set { twoPointDistribution.P = value; }
        }

        #endregion

        #region IDistribution members

        public override double Next()
        {
            int sum = 0;
            for (int i = 0; i < n; i++)
            {
                double value = twoPointDistribution.Next();
                if (value == 1)
                    sum++;
            }
            return sum;
        }

        public override string ToString()
        {
            return String.Format("Binomial({0},{1})", n, twoPointDistribution.P);
        }

        public override DistributionType DistributionType
        {
            get { return DistributionType.Binomial; }
        }

        #endregion
    }
}
