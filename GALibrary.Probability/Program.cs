using System;
using System.Collections.Generic;
using System.Text;

namespace Probability
{
    class Program
    {
        static void Test()
        {
            IDistribution dist = new Distributions.Poisson(12);

            double sum = 0;
            int max = 200000;

            for (int i = 0; i < max; i++)
            {
                double value = dist.Next();
                sum += value;
            }

            Console.WriteLine(sum / max);
        }
    }
}
