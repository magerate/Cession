using System;

namespace Cession.Geometries
{
    public struct Range
    {
        private int min;
        private int max;

        public Range(int x, int y)
        {
            if (x > y)
            {
                min = y;
                max = x;
            }
            else
            {
                min = x;
                max = y;
            }
        }

        public bool Contains(int value)
        {
            return Range.Contains(min, max, value);
        }

        public static bool Contains(int x, int y, int value)
        {
            return value >= Math.Min(x, y) && value <= Math.Max(x, y);
        }

        public static bool Contains(int x,int y,int value,int delta)
        {
            return value >= Math.Min(x, y) - Math.Abs(delta) && 
                value <= Math.Max(x, y) + Math.Abs(delta);
        }
    }
}
