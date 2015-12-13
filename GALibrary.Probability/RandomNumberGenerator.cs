using System;
using System.Collections.Generic;
using System.Text;

namespace GALibrary.Probability
{
    public static class RandomNumberGenerator
    {
        #region Static storage

        private static Random s_generator = new Random();

        #endregion

        #region Static methods

        public static double NextDouble()
        {
            return s_generator.NextDouble();
        }

        public static int Next()
        {
            return s_generator.Next();
        }

        public static int Next(int maxValue)
        {
            return s_generator.Next(maxValue);
        }

        public static int Next(int minValue, int maxValue)
        {
            return s_generator.Next(minValue, maxValue);
        }

        #endregion
    }
}
