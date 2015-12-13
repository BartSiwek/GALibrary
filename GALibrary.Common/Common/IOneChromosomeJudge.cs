using System;
using System.Collections.Generic;
using System.Text;

namespace GALibrary.Common
{
    public interface IOneChromosomeJudge<T>
    {
        void Judge(T chromosome);

        void Initialize();
    }
}
