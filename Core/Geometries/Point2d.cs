namespace Cession.Geometries
{
    using System;

    public struct Point2d:IEquatable<Point2d>
    {
        public static readonly Point2d Empty = new Point2d(0, 0);

        public double X, Y;

        public Point2d(double x,double y)
        {
            this.X = x;
            this.Y = y;
        }

        public bool Equals(Point2d p)
        {
            return X == p.X && Y == p.Y;
        }

        public override bool Equals(object obj)
        {
            if (null == obj || !(obj is Point2d))
                return false;
            return this.Equals((Point2d)obj);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", X.ToString(), Y.ToString());
        }

        public static bool operator ==(Point2d p1,Point2d p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Point2d p1,Point2d p2)
        {
            return !p1.Equals(p2);
        }

        public static Vector operator -(Point2d p1,Point2d p2)
        {
            return new Vector(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static Point2d operator -(Point2d p,Vector v)
        {
            return new Point2d(p.X - v.X, p.Y - v.Y);
        }

        public static Point2d operator +(Point2d p,Vector v)
        {
            return new Point2d(p.X + v.X, p.Y + v.Y);
        }

        public void Offset(double x,double y)
        {
            this.X += x;
            this.Y += y;
        }

        public void Offset(Vector vector)
        {
            this.Offset(vector.X, vector.Y);
        }

        public static Point2d Flip(Point2d point, Point2d origin)
        {
            return origin + (origin - point);
        }

        public void Flip(Point2d p)
        {
            this = Point2d.Flip(this, p);
        }
       
        public static Point2d Rotate(Point2d point,Point2d origin,double angle)
        {
            var v = point - origin;
            v.Rotate(angle);
            return origin + v;
        }

        public void Rotate(Point2d origin,double angle)
        {
            this = Point2d.Rotate(this, origin, angle);
        }

        public void Rotate(double angle)
        {
            this = Point2d.Rotate(this, Point2d.Empty, angle);
        }

        public static double DistanceBetween(Point2d p1,Point2d p2)
        {
            return (p1 - p2).Length;
        }

        public double DistanceBetween(Point2d point)
        {
            return Point2d.DistanceBetween(this, point);
        }

        public static double DistanceSquared(Point2d p1,Point2d p2)
        {
            return (p1 - p2).LengthSquared;
        }

        public double DistanceSquared(Point2d point)
        {
            return DistanceSquared(this, point);
        }
    }
}
