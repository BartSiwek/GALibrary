using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using GALibrary.Probability.Distributions;

namespace GALibrary.Probability
{
    public abstract class Distribution : IDistribution
    {
        #region IDistribution Members

        public abstract double Next();
        public abstract override string ToString();
        public abstract DistributionType DistributionType
        {
            get;
        }

        #endregion

        #region Distribution parsing

        public static IDistribution Parse(string value)
        {
            //Local vars
            DistributionType type;
            string[] parameters;

            ParseDescription(value, out parameters, out type);

            try
            {
                switch (type)
                {
                    case DistributionType.Binomial:
                        return new Binomial(parameters);
                    case DistributionType.Exponential:
                        return new Exponential(parameters);
                    case DistributionType.Normal:
                        return new Normal(parameters);
                    case DistributionType.OnePoint:
                        return new OnePoint(parameters);
                    case DistributionType.Poisson:
                        return new Poisson(parameters);
                    case DistributionType.TwoPoint:
                        return new TwoPoint(parameters);
                    case DistributionType.Uniform:
                        return new Uniform(parameters);
                    default:
                        throw new FormatException("The specified string is not a valid distribution desctiption");
                }
            }
            catch (FormatException e)
            {
                throw new FormatException("The specified string is not a valid distribution desctiption", e);
            }
        }

        private static void ParseDescription(string input, out string[] parametres, out DistributionType type)
        {
            //Compute the pattern
            string pattern = @"^(?<name>\w+)\((?<digitlist>([^,]+,)*[^,]+)\)$";
            Regex r = new Regex(pattern);
            Match m = r.Match(input);

            if(!m.Success)
                throw new FormatException("The specified string is not a valid distribution desctiption");
            else
            {
                try
                {
                    type = (DistributionType)Enum.Parse(typeof(DistributionType), m.Groups["name"].Value);
                    parametres = m.Groups["digitlist"].Value.Split(',');
                }
                catch (FormatException e)
                {
                    throw new FormatException(String.Format("A '{0}' is not a valid distribution name", m.Groups["name"].Value), e);
                }
            }
        }

        #endregion

        #region Protected methods

        protected void ThrowBadParametersNumberException()
        {
            throw new FormatException(
                String.Format("The number of parameters suplied in not valid for {0} distribution", DistributionType)
            );
        }

        #endregion
    }
}
