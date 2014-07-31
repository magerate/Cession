namespace Cession.Geometries
{
    using System;

    public struct Range
    {
        private double min;
        private double max;


        public Range(double x, double y)
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

        public bool Contains(double value)
        {
            return Range.Contains(min, max, value);
        }

        public static bool Contains(double x, double y, double value)
        {
            return value >= Math.Min(x, y) && value <= Math.Max(x, y);
        }

        public static bool Contains(double x,double y,double value,double delta)
        {
            return value >= Math.Min(x, y) - Math.Abs(delta) && 
                value <= Math.Max(x, y) + Math.Abs(delta);
        }
    }
}
