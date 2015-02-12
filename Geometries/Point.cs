using System;

namespace Cession.Geometries
{
    public struct Point:IEquatable<Point>
    {
        public static readonly Point Empty = new Point (0, 0);
        public int X, Y;

        public Point (int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals (Point p)
        {
            return X == p.X && Y == p.Y;
        }

        public override bool Equals (object obj)
        {
            if (null == obj || !(obj is Point))
                return false;
            return this.Equals ((Point)obj);
        }

        public override int GetHashCode ()
        {
            return X.GetHashCode () ^ Y.GetHashCode ();
        }

        public override string ToString ()
        {
            return string.Format ("({0},{1})", X.ToString (), Y.ToString ());
        }

        public static bool operator == (Point p1, Point p2)
        {
            return p1.Equals (p2);
        }

        public static bool operator != (Point p1, Point p2)
        {
            return !p1.Equals (p2);
        }

        public static Vector operator - (Point p1, Point p2)
        {
            return new Vector (p1.X - p2.X, p1.Y - p2.Y);
        }

        public static Point operator - (Point p, Vector v)
        {
            return new Point (MathHelper.Round (p.X - v.X), MathHelper.Round (p.Y - v.Y));
        }

        public static Point operator + (Point p, Vector v)
        {
            return new Point (MathHelper.Round (p.X + v.X), MathHelper.Round (p.Y + v.Y));
        }

        public void Offset (int x, int y)
        {
            X += x;
            Y += y;
        }

        public void Offset (Vector vector)
        {
            X += MathHelper.Round (vector.X);
            Y += MathHelper.Round (vector.Y);
        }

        public static double DistanceBetween (Point p1, Point p2)
        {
            double dx = (double)p1.X - p2.X;
            double dy = (double)p1.Y - p2.Y;
            return Math.Sqrt (dx * dx + dy * dy);
        }

        public double DistanceBetween (Point point)
        {
            return Point.DistanceBetween (this, point);
        }

        public void Rotate (Point point, double radian)
        {
            this = Point.Rotate (this, point, radian);
        }

        public static Point Rotate (Point point, Point referencePoint, double radian)
        {
            if (point == referencePoint)
                return point;

            var v = point - referencePoint;
            v.Rotate (radian);
            return referencePoint + v;
        }

        public static Point Project (Point p1, Point p2, Point point)
        {
            if (p1 == p2)
                return p1;

            var v1 = point - p1;
            var v2 = p2 - p1;
            return p1 + v1 * v2 / v2.LengthSquared * v2;
        }

        public Point Project (Point p1, Point p2)
        {
            return Point.Project (p1, p2, this);
        }
    }
}
