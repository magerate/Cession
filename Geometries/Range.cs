using System;

namespace Cession.Geometries
{
    public struct Range
    {
        private double min;
        private double max;

        public Range (double x, double y)
        {
            if (x > y) {
                min = y;
                max = x;
            } else {
                min = x;
                max = y;
            }
        }

        public bool Contains (double value)
        {
            return Range.Contains (min, max, value);
        }

        public static bool Contains (double x, double y, double value)
        {
            return Range.Contains (x, y, value, 0);
        }

        public static bool Contains (double x, double y, double value, double delta)
        {
            if (double.IsNaN (x))
                throw new ArgumentException ();

            if (double.IsNaN (y))
                throw new ArgumentException ();

            if (double.IsNaN (value))
                throw new ArgumentException ();

            return value >= Math.Min (x, y) - Math.Abs (delta) &&
            value <= Math.Max (x, y) + Math.Abs (delta);
        }
    }
}
