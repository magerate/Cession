using System;

namespace Cession.Geometries
{
    public struct Line : IEquatable<Line>
    {
        private Point _p1;
        private Point _p2;

        public Line (Point p1, Point p2)
        {
            _p1 = p1;
            _p2 = p2;
        }

        public Point P1
        {
            get { return _p1; }
            set { _p1 = value; }
        }

        public Point P2
        {
            get { return _p2; }
            set { _p2 = value; }
        }

        public bool IsEmpty
        {
            get { return _p1 == _p2; }
        }

        public static bool Contains (Point p1, Point p2, Point point)
        {
            if (p1 == p2)
                return false;

            return Vector.CrossProduct (p1 - point, point - p2) == 0;
        }

        public bool Contains (Point point)
        {
            return Line.Contains (_p1, _p2, point);
        }


        public static bool Equals (Point p1, Point p2, Point p3, Point p4)
        {
            if (p3 == p4)
                return false;
            return Line.Contains (p1, p2, p3) && Line.Contains (p1, p2, p4);
        }

        public bool Equals (Line line)
        {
            return Line.Equals (_p1, _p2, line._p1, line._p2);
        }

        public override bool Equals (object obj)
        {
            if (null == obj || !(obj is Line))
                return false;
            return Equals ((Line)obj);
        }

        public static bool operator == (Line left, Line right)
        {
            return left.Equals (right);
        }

        public static bool operator != (Line left, Line right)
        {
            return !left.Equals (right);
        }

        public override int GetHashCode ()
        {
            //wrong 
            return _p1.GetHashCode () ^ _p2.GetHashCode ();
        }

        public static bool Parallels (Point p1, Point p2, Point p3, Point p4)
        {
            if (p1 == p2 || p3 == p4)
                return false;

            return Vector.CrossProduct (p1 - p2, p3 - p4) == 0;
        }

        public bool Parallels (Line line)
        {
            return Line.Parallels (_p1, _p2, line._p1, line._p2);
        }

        public bool Parallels (Point p1, Point p2)
        {
            return Line.Parallels (p1, p2, p1, p2);
        }

        public static bool Orthos (Point p1, Point p2, Point p3, Point p4)
        {
            if (p1 == p2 || p3 == p4)
                return false;

            return (p1 - p2) * (p3 - p4) == 0;
        }

        public bool Orthos (Line line)
        {
            return Line.Orthos (_p1, _p2, line._p1, line._p2);
        }

        public bool Orthos (Point p1, Point p2)
        {
            return Line.Orthos (p1, p2, p1, p2);
        }

        public static Point? Intersect (Point p1, Point p2, Point p3, Point p4)
        {
            if (p1 == p2 || p3 == p4)
                return null;
		
            double denominator = (p1.X - p2.X) * (p3.Y - p4.Y) - (p3.X - p4.X) * (p1.Y - p2.Y);

            if (denominator == 0 || double.IsInfinity (denominator) || double.IsNaN (denominator))
                return null;

            double x = ((p1.X * p2.Y - p2.X * p1.Y) * (p3.X - p4.X) - (p3.X * p4.Y - p4.X * p3.Y) * (p1.X - p2.X)) / denominator;
            double y = ((p1.X * p2.Y - p2.X * p1.Y) * (p3.Y - p4.Y) - (p3.X * p4.Y - p4.X * p3.Y) * (p1.Y - p2.Y)) / denominator;

            if (double.IsInfinity (x) || double.IsNaN (x) || double.IsInfinity (y) || double.IsNaN (y))
                return null;

            return new Point (x, y);
        }

        public static Point? Intersect (Line line1, Line line2)
        {
            return Line.Intersect (line1._p1, line1._p2, line2._p1, line2._p2);
        }

        public void Offset (int x, int y)
        {
            _p1.Offset (x, y);
            _p2.Offset (x, y);
        }

        public void Offset (Vector vector)
        {
            this.Offset ((int)vector.X, (int)vector.Y);
        }

        public static double DistanceBetween (Point p1, Point p2, Point point)
        {
            if (p1 == p2)
                return Point.DistanceBetween (p1, point);

            var v1 = point - p1;
            var v2 = p2 - p1;

            return Vector.CrossProduct (v1, v2) / v2.Length;
        }

        public static double DistanceBetween (Line line, Point point)
        {
            return Line.DistanceBetween (line._p1, line._p2, point);
        }

        public double DistanceBetween (Point point)
        {
            return Line.DistanceBetween (_p1, _p2, point);
        }
    }
}
