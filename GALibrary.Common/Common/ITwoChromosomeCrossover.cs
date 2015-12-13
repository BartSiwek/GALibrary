using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Core;

namespace GALibrary.Common
{
    public interface ITwoChromosomeCrossover<T>
        where T : Chromosome
    {   
        void Crossover(ref T first, ref T second);
        void Initialize();
        int SingleOperationOutputs { get; }
    }
}
