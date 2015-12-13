using System;
using System.Collections.Generic;
using System.Text;

namespace GALibrary.Core
{
    public interface ICrossoverStrategy<T> where T : Chromosome
    {
        void Crossover(ref T[] genePool);
        void Initialize();
        int SingleOperationInputs { get; }
        int SingleOperationOutputs { get; }
    }
}
