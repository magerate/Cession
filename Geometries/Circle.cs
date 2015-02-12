using System;

namespace Cession.Geometries
{
    public struct Circle: IEquatable<Circle>
    {
        private Point _center;
        private double _radius;

        public Point Center
        {
            get{ return _center; }
            set{ _center = value; }
        }

        public double Radius
        {
            get{ return _radius; }
            set
            { 
                if (double.IsInfinity (value) || double.IsNaN (value))
                    throw new ArgumentException ();

                if (value < 0)
                    throw new ArgumentOutOfRangeException ();

                _radius = value; 
            }
        }

        public Circle (Point center, double radius)
        {
            if (double.IsInfinity (radius) || double.IsNaN (radius))
                throw new ArgumentException ();

            if (radius < 0)
                throw new ArgumentOutOfRangeException ();

            _center = center;
            _radius = radius;
        }

        public bool Equals (Circle circle)
        {
            return _center == circle.Center && _radius == circle.Radius;
        }

        public override bool Equals (object obj)
        {
            if (null == obj || !(obj is Circle))
                return false;
            return Equals ((Circle)obj);
        }

        public override int GetHashCode ()
        {
            return _center.GetHashCode () ^ _radius.GetHashCode ();
        }

        public static Tuple<Point,Point> Intersects (Point p1, double r1, Point p2, double r2)
        {
            if (double.IsNaN (r1) || double.IsInfinity (r1))
                throw new ArgumentException ("r1");

            if (double.IsNaN (r2) || double.IsInfinity (r2))
                throw new ArgumentException ("r2");

            var d = p1.DistanceBetween (p2);

            if (d > r1 + r2)
                return null;

            if (d < Math.Abs (r1 - r2))
                return null;

            if (p1 == p2)
                return null;

            if (d == r1 + r2)
            {
                var v = p2 - p1;
                v = v / v.Length * r1;
                return new Tuple<Point, Point> (p1 + v, p1 + v);
            }

            if (d == Math.Abs (r1 - r2))
            {
                Point p;
                Vector v;
                if (r1 > r2)
                {
                    p = p1;
                    v = p2 - p1;
                    v = v / v.Length * r1;
                } 
                else
                {
                    p = p2;
                    v = p1 - p2;
                    v = v / v.Length * r2;
                }

                return new Tuple<Point, Point> (p + v, p + v);
            }

            double q = Math.Sqrt ((d + r1 + r2) *
                       (d + r1 - r2) *
                       (d - r1 + r2) *
                       (-d + r1 + r2)) / 4;

            double x1 = (p1.X + p2.X) / 2 +
                        (p2.X - p1.X) * (r1 * r1 - r2 * r2) / (2 * d * d) +
                        2 * (p1.Y - p2.Y) * q / (d * d);
            double x2 = (p1.X + p2.X) / 2 +
                        (p2.X - p1.X) * (r1 * r1 - r2 * r2) / (2 * d * d) -
                        2 * (p1.Y - p2.Y) * q / (d * d);

            double y1 = (p1.Y + p2.Y) / 2 +
                        (p2.Y - p1.Y) * (r1 * r1 - r2 * r2) / (2 * d * d) -
                        2 * (p1.X - p2.X) * q / (d * d);

            double y2 = (p1.Y + p2.Y) / 2 +
                        (p2.Y - p1.Y) * (r1 * r1 - r2 * r2) / (2 * d * d) +
                        2 * (p1.X - p2.X) * q / (d * d);

            return new Tuple<Point, Point> (new Point (x1, y1), new Point (x2, y2));
        }


        public static Tuple<Point,Point> IntersectsWithLine (Point c1, double r1, Point p1, Point p2)
        {
            if (double.IsNaN (r1) || double.IsInfinity (r1))
                throw new ArgumentException ();

            if (p1 == p2)
                return null;

            double dx, dy, a, b, c, bb4ac, t;

            dx = p2.X - p1.X;
            dy = p2.Y - p1.Y;

            a = dx * dx + dy * dy;
            b = 2 * (dx * (p1.X - c1.X) + dy * (p1.Y - c1.Y));
            c = (p1.X - c1.X) * (p1.X - c1.X) + (p1.Y - c1.Y) * (p1.Y - c1.Y) - r1 * r1;

            bb4ac = b * b - 4 * a * c;
            if ((a <= 0.0000001) || (bb4ac < 0))
            {
                // No real solutions.
                return null;
            } else if (bb4ac == 0)
            {
                // One solution.
                t = -b / (2 * a);
                var ip = new Point (p1.X + t * dx, p1.Y + t * dy);
                return new Tuple<Point, Point> (ip, ip);
            } else
            {
                // Two solutions.
                t = (float)((-b + Math.Sqrt (bb4ac)) / (2 * a));
                var ip1 = new Point (p1.X + t * dx, p1.Y + t * dy);
                t = (float)((-b - Math.Sqrt (bb4ac)) / (2 * a));
                var ip2 = new Point (p1.X + t * dx, p1.Y + t * dy);
                return new Tuple<Point, Point> (ip1, ip2);
            }
        }

        public static Point? GetCenter (Point p1, Point p2, Point p3)
        {
            if (p1 == p2 || p2 == p3 || p1 == p3)
                return null;

            if (Line.Contains (p1, p2, p3))
                return null;

            var mp1 = new Point ((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
            var v1 = p2 - p1;
            v1.Rotate (Math.PI / 2);
            v1 /= 2;
            var ep1 = mp1 + v1;

            var mp2 = new Point ((p3.X + p2.X) / 2, (p3.Y + p2.Y) / 2);
            var v2 = p3 - p2;
            v2.Rotate (Math.PI / 2);
            v2 /= 2;
            var ep2 = mp2 + v2;

            return Line.Intersect (mp1, ep1, mp2, ep2);
        }
    }
}

