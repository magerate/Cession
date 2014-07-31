// -----------------------------------------------------------------------
// <copyright file="OpenRange.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Cession.Geometries
{
    using System;
   
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public struct OpenRange
    {
        public static bool Contains(double x, double direction, double value)
        {
            if (direction == 0)
                return x == value;

            if (direction > 0)
                return value >= x;
            return value <= x;
        }

        public static bool Contains(double x, double direction, double value, double delta)
        {
            if (direction == 0)
                return Math.Abs(x - value) <= delta;

            if (direction > 0)
                return value >= x - delta;

            return value <= x + delta;
        }
    }
}
