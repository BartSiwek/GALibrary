using System;
using System.Collections.Generic;
using System.Text;

namespace GALibrary.Probability
{
    public interface IDistribution
    {
        double Next();
        string ToString();
        DistributionType DistributionType
        {
            get;
        }
    }
}
