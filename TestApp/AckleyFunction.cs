using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{
    public class AckleyFunction
    {
        public static readonly int Dimentions = 30;

        private static double MyCos(double d)
        {
            if (d > 9223372036854775295)
                d = double.PositiveInfinity;
            if (d < -9223372036854775295)
                d = double.NegativeInfinity;

            return Math.Cos(d);
        }

        public static double GetValue(double[] vector)
        {
            double sum1 = 0;
            double sum2 = 0;

            for (int i = 0; i < vector.Length; i++)
            {
                sum1 += Math.Pow(vector[i], 2);
                sum2 += MyCos(2 * Math.PI * vector[i]);;
            }

            sum1 = sum1 / vector.Length;
            sum2 = sum2 / vector.Length;

            double value = -20 * Math.Exp(-0.2 * Math.Sqrt(sum1)) - Math.Exp(sum2) + 20 + Math.E;

            return value;
        }
    }
}
