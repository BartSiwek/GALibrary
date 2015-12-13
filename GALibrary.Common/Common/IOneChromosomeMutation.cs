using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Core;

namespace GALibrary.Common
{
    public interface IOneChromosomeMutation<T> where T : Chromosome
    {   
        void Mutate(ref T chromosome);
        void Initialize();
        int SingleOperationOutputs { get; }
    }
}
