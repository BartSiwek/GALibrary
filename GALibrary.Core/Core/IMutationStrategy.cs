using System;
using System.Collections.Generic;
using System.Text;

namespace GALibrary.Core
{
    public interface IMutationStrategy<T> where T : Chromosome
    {
        void Mutate(ref T[] genePool);
        void Initialize();
        int SingleOperationInputs { get; }
        int SingleOperationOutputs { get; }
    }
}
