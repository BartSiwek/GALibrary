using System;
using System.Collections.Generic;
using System.Text;

namespace GALibrary.Core
{
    public interface ISelectionStrategy<T> where T : Chromosome
    {
        T[] Select(T[] population, int genePoolSize);

        void Initialize();
    }
}
