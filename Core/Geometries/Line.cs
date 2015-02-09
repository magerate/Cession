﻿using System;

namespace Cession.Geometries
{
    public struct Line : IEquatable<Line>
    {
		private Point2 _p1;
		private Point2 _p2;

        public Line(Point2 p1, Point2 p2)
        {
            _p1 = p1;
            _p2 = p2;
        }

        public Point2 P1
        {
            get { return _p1; }
            set { _p1 = value; }
        }

        public Point2 P2
        {
            get { return _p2; }
            set { _p2 = value; }
        }

        public bool IsEmpty
        {
            get { return _p1 == _p2; }
        }

        public static bool Contains(Point2 p1, Point2 p2, Point2 point)
        {
            if (p1 == p2)
                return false;

            return Vector.CrossProduct(p1 - point, point - p2) == 0;
        }

        public bool Contains(Point2 point)
        {
            return Line.Contains(_p1, _p2, point);
        }

        public static bool AlmostContains(Point2 p1, Point2 p2, Point2 point)
        {
            if (p1 == p2)
                return false;

            var v1 = Vector.Normalize(p1 - point);
            var v2 = Vector.Normalize(point - p2);

            return Math.Abs(Vector.CrossProduct(v1, v2)) <= Constants.Epsilon;
        }

        public bool AlmostContains(Point2 point)
        {
            return Line.AlmostContains(_p1, _p2, point);
        }

        public static bool Equals(Point2 p1, Point2 p2, Point2 p3, Point2 p4)
        {
            if (p3 == p4)
                return false;
            return Line.Contains(p1, p2, p3) && Line.Contains(p1, p2, p4);
        }

        public bool Equals(Line line)
        {
            return Line.Equals(_p1, _p2, line._p1, line._p2);
        }

        public override bool Equals(object obj)
        {
            if (null == obj || !(obj is Line))
                return false;
            return Equals((Line)obj);
        }

        public static bool operator ==(Line left, Line right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Line left, Line right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            //wrong 
            return _p1.GetHashCode() ^ _p2.GetHashCode();
        }

        public static bool Parallels(Point2 p1, Point2 p2, Point2 p3, Point2 p4)
        {
            if (p1 == p2 || p3 == p4)
                return false;

            return Vector.CrossProduct(p1 - p2, p3 - p4) == 0;
        }

        public bool Parallels(Line line)
        {
            return Line.Parallels(_p1, _p2, line._p1, line._p2);
        }

        public bool Parallels(Point2 p1, Point2 p2)
        {
            return Line.Parallels(p1, p2, p1, p2);
        }

        public static bool AlmostParallels(Point2 p1, Point2 p2, Point2 p3, Point2 p4)
        {
            if (p1 == p2 || p3 == p4)
                return false;

            var v1 = p1 - p2;
            v1.Normalize();
            var v2 = p3 - p4;
            v2.Normalize();
            return Math.Abs(Vector.CrossProduct(v1, v2)) <= Constants.Epsilon;
        }

        public bool AlmostParallels(Line line)
        {
            return Line.AlmostParallels(_p1, _p2, _p1, _p2);
        }

        public bool AlmostParallels(Point2 p1,Point2 p2)
        {
            return Line.AlmostParallels(p1, p2, p1, p2);
        }

        public static bool Orthos(Point2 p1, Point2 p2, Point2 p3, Point2 p4)
        {
            if (p1 == p2 || p3 == p4)
                return false;

            return (p1 - p2) * (p3 - p4) == 0;
        }

        public bool Orthos(Line line)
        {
            return Line.Orthos(_p1, _p2, line._p1, line._p2);
        }

        public bool Orthos(Point2 p1,Point2 p2)
        {
            return Line.Orthos(p1, p2, p1, p2);
        }

        public static bool AlmostOrthos(Point2 p1, Point2 p2, Point2 p3, Point2 p4)
        {
            if (p1 == p2 || p3 == p4)
                return false;
            var v1 = Vector.Normalize(p1 - p2);
            var v2 = Vector.Normalize(p3 - p4);

            return v1 * v2 <= Constants.Epsilon;
        }

        public bool AlmostOrthos(Line line)
        {
            return Line.AlmostOrthos(_p1, _p2, line._p1, line._p2);
        }

        public bool AlmostOrthos(Point2 p1,Point2 p2)
        {
            return Line.AlmostOrthos(p1, p2, p1, p2);
        }

        public static Point2? Intersect(Point2 p1, Point2 p2, Point2 p3, Point2 p4)
        {
            if (p1 == p2 || p3 == p4 || Line.Parallels(p1, p2, p3, p4))
                return null;

			long x, y;
			checked{
				x = ((p1.X * (long)p2.Y - p2.X * (long)p1.Y) * (p3.X - p4.X) - (p3.X * (long)p4.Y - p4.X * (long)p3.Y) * (p1.X - p2.X)) /
					((p1.X - p2.X) * (long)(p3.Y - p4.Y) - (p3.X - p4.X) * (long)(p1.Y - p2.Y));
				y = ((p1.X * (long)p2.Y - p2.X * (long)p1.Y) * (p3.Y - p4.Y) - (p3.X * (long)p4.Y - p4.X * (long)p3.Y) * (p1.Y - p2.Y)) /
					((p1.X - p2.X) * (long)(p3.Y - p4.Y) - (p3.X - p4.X) * (long)(p1.Y - p2.Y));
			}

			if (x > int.MaxValue || y > int.MaxValue)
				return null;

			return new Point2((int)x, (int)y);
        }

        public static Point2? Intersect(Line line1, Line line2)
        {
            return Line.Intersect(line1._p1, line1._p2, line2._p1, line2._p2);
        }

        public void Offset(int x, int y)
        {
            _p1.Offset(x, y);
            _p2.Offset(x, y);
        }

        public void Offset(Vector vector)
        {
			this.Offset((int)vector.X, (int)vector.Y);
        }

        public static Point2 Project(Point2 p1, Point2 p2, Point2 point)
        {
            if (p1 == p2)
                return p1;

            var v1 = point - p1;
            var v2 = p2 - p1;
            return p1 + v1 * v2 / v2.LengthSquared * v2;
        }

        public static Point2 Project(Line line, Point2 point)
        {
            return Line.Project(line._p1, line._p2, point);
        }

        public Point2 Project(Point2 point)
        {
            return Line.Project(this, point);
        }

        public static double DistanceBetween(Point2 p1, Point2 p2, Point2 point)
        {
            if (p1 == p2)
                return Point2.DistanceBetween(p1, point);

            var v1 = point - p1;
            var v2 = p2 - p1;

			return Vector.CrossProduct (v1, v2) / v2.Length;
        }

        public static double DistanceBetween(Line line, Point2 point)
        {
            return Line.DistanceBetween(line._p1, line._p2, point);
        }

        public double DistanceBetween(Point2 point)
        {
            return Line.DistanceBetween(_p1, _p2,point);
        }
    }
}
