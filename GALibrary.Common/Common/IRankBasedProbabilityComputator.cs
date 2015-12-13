using System;
using System.Collections.Generic;
using System.Text;

namespace GALibrary.Common
{
    public interface IRankBasedProbabilityComputator
    {
        double[] ComputeProbabilities(int maxRank);
        void Initialize();
    }
}
