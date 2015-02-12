using System;

namespace Cession.Geometries
{
    public struct Range
    {
        private int _min;
        private int _max;

        public Range (int x, int y)
        {
            if (x > y) {
                _min = y;
                _max = x;
            } else {
                _min = x;
                _max = y;
            }
        }

        public bool Contains (int value)
        {
            return Range.Contains (_min, _max, value);
        }

        public static bool Contains (int x, int y, int value)
        {
            return Range.Contains (x, y, value, 0);
        }

        public static bool Contains (int x, int y, int value, int delta)
        {
            if (delta < 0 || delta > 1000)
                throw new ArgumentNullException ("delta");

            return value >= Math.Min (x, y) - delta && value <= Math.Max (x, y) + delta;
        }
    }
}
