using System;
using System.Collections.Generic;
using System.Text;

namespace GALibrary.Core
{
    public interface IChromosomeGenerator<T> where T : Chromosome
    {
        T Generate();

        void Initialize();
    }
}
