using System;
using System.Collections.Generic;
using System.Text;

namespace GALibrary.Core
{
    public interface IStopCondition
    {
        bool Stop(Chromosome[] population);
        void Initialize();
    }
}
