using System;

namespace Cession.Geometries
{
    internal static class MathHelper
    {
        public static bool AlmostEquals (double left, double right, double delta = 1e-5)
        {
            return Math.Abs (left - right) <= delta;
        }

        public static int Round(double value)
        {
            if (double.IsNaN (value) || double.IsInfinity (value))
                throw new ArgumentException ("value");

            if (value > int.MaxValue)
                throw new ArgumentOutOfRangeException ("value");

            return (int)Math.Round (value);
        }
    }
}

