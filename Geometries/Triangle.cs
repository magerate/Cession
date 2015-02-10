using System;

namespace Cession.Geometries
{
    public static class Triangle
    {
        public static double GetSignedArea (Point p1, Point p2, Point p3)
        {
            return 0;
        }

        public static bool IsClockwise (Point p1, Point p2, Point p3)
        {
            return true;
        }

        public static bool Contains (Point p1, Point p2, Point p3, Point point)
        {
            return false;
        }
    }
}

