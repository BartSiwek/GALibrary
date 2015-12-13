using System;
using System.Collections.Generic;
using System.Text;

namespace GALibrary.Core
{
    public interface IJudgeStrategy<T> where T : Chromosome
    {
        void Judge(T[] population);

        void Initialize();
    }
}
