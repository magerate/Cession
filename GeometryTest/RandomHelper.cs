using System;

namespace GeometryTest
{
    public static class RandomHelper
    {
        private static Random random = new Random ();
       
        public static int Next (int maxValue)
        {
            return random.Next (maxValue);
        }

        public static int Next(int minValue,int maxValue)
        {
            return random.Next (minValue, maxValue);
        }

        public static int Next()
        {
            return random.Next ();
        }


        public static double NextDouble (double maxValue)
        {
            return random.NextDouble () * maxValue;
        }

        public static double NextDouble ()
        {
            return random.NextDouble ();
        }

    }
}

