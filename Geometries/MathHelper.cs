using System;

namespace Cession.Geometries
{
    public static class MathHelper
    {
        public static bool AlmostEquals (double left, double right, double delta = 1e-5)
        {
            return Math.Abs (left - right) <= delta;
        }

        public static double Clamp(double minValue,double maxValue,double value)
        {
            if (minValue > maxValue)
                throw new ArgumentException ();

            return Math.Min(maxValue,Math.Max (minValue, value));
        }
    }
}

