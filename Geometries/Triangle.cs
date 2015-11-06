using System;

namespace Cession.Geometries
{
    public static class Triangle
    {
        public static bool IsValid (Point p1, Point p2, Point p3)
        {
            return p1 != p2 && p2 != p3 && p1 != p3 && !Line.Contains (p1, p2, p3);
        }

        public static double GetSignedArea (Point p1, Point p2, Point p3)
        {
            Vector v1 = p2 - p1;
            Vector v2 = p3 - p1;
            return Vector.CrossProduct (v1, v2) / 2;
        }

        public static bool IsClockwise (Point p1, Point p2, Point p3)
        {
            return GetSignedArea (p1, p2, p3) >= 0;
        }

        //http://totologic.blogspot.fr/2014/01/accurate-point-in-triangle-test.html
        public static bool Contains (Point p1, Point p2, Point p3, Point point)
        {
            if (!IsValid (p1, p2, p3))
                return false;

            double denominator = (p1.X * (p2.Y - p3.Y) + p1.Y * (p3.X - p2.X) + p2.X * p3.Y - p2.Y * p3.X);
            double t1 = (point.X * (p3.Y - p1.Y) + point.Y * (p1.X - p3.X) - p1.X * p3.Y + p1.Y * p3.X) / denominator;
            double t2 = (point.X * (p2.Y - p1.Y) + point.Y * (p1.X - p2.X) - p1.X * p2.Y + p1.Y * p2.X) / -denominator;
            double s = t1 + t2;

            return 0 <= t1 && t1 <= 1 && 0 <= t2 && t2 <= 1 && s <= 1;
        }

        public static bool IsClamp(Point p1, Point p2, Point p3, Point point)
        {
            double denominator = (p1.X * (p2.Y - p3.Y) + p1.Y * (p3.X - p2.X) + p2.X * p3.Y - p2.Y * p3.X);
            double t1 = (point.X * (p3.Y - p1.Y) + point.Y * (p1.X - p3.X) - p1.X * p3.Y + p1.Y * p3.X) / denominator;
            double t2 = (point.X * (p2.Y - p1.Y) + point.Y * (p1.X - p2.X) - p1.X * p2.Y + p1.Y * p2.X) / -denominator;

            return 0 <= t1 && 0 <= t2;
        }
    }
}

