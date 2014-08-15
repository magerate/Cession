namespace Cession.Geometries
{
    using System;

    public struct Line : IEquatable<Line>
    {
        private Point2 p1;
        private Point2 p2;

        public Line(Point2 p1, Point2 p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        public Point2 P1
        {
            get { return p1; }
            set { p1 = value; }
        }

        public Point2 P2
        {
            get { return p2; }
            set { p2 = value; }
        }

        public bool IsEmpty
        {
            get { return p1 == p2; }
        }

        public static bool Contains(Point2 p1, Point2 p2, Point2 point)
        {
            if (p1 == p2)
                return false;

            return Vector.CrossProduct(p1 - point, point - p2) == 0;
        }

        public bool Contains(Point2 point)
        {
            return Line.Contains(p1, p2, point);
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
            return Line.AlmostContains(p1, p2, point);
        }

        public static bool Equals(Point2 p1, Point2 p2, Point2 p3, Point2 p4)
        {
            if (p3 == p4)
                return false;
            return Line.Contains(p1, p2, p3) && Line.Contains(p1, p2, p4);
        }

        public bool Equals(Line line)
        {
            return Line.Equals(p1, p2, line.p1, line.p2);
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
            return p1.GetHashCode() ^ p2.GetHashCode();
        }

        public static bool Parallels(Point2 p1, Point2 p2, Point2 p3, Point2 p4)
        {
            if (p1 == p2 || p3 == p4)
                return false;

            return Vector.CrossProduct(p1 - p2, p3 - p4) == 0;
        }

        public bool Parallels(Line line)
        {
            return Line.Parallels(p1, p2, line.p1, line.p2);
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
            return Line.AlmostParallels(p1, p2, p1, p2);
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
            return Line.Orthos(p1, p2, line.p1, line.p2);
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
            return Line.AlmostOrthos(p1, p2, line.p1, line.p2);
        }

        public bool AlmostOrthos(Point2 p1,Point2 p2)
        {
            return Line.AlmostOrthos(p1, p2, p1, p2);
        }

        public static Point2? Intersect(Point2 p1, Point2 p2, Point2 p3, Point2 p4)
        {
            if (p1 == p2 || p3 == p4 || Line.Parallels(p1, p2, p3, p4))
                return null;

			long x = (((long)p1.X * p2.Y - (long)p2.X * p1.Y) * (p3.X - p4.X) - ((long)p3.X * p4.Y - (long)p4.X * p3.Y) * (p1.X - p2.X)) /
				(((long)p1.X - p2.X) * (p3.Y - p4.Y) - ((long)p3.X - p4.X) * (p1.Y - p2.Y));
			long y = (((long)p1.X * p2.Y - (long)p2.X * p1.Y) * (p3.Y - p4.Y) - ((long)p3.X * p4.Y - (long)p4.X * p3.Y) * (p1.Y - p2.Y)) /
				(((long)p1.X - p2.X) * (p3.Y - p4.Y) - ((long)p3.X - p4.X) * (p1.Y - p2.Y));

			return new Point2((int)x, (int)y);
        }

        public static Point2? Intersect(Line line1, Line line2)
        {
            return Line.Intersect(line1.p1, line1.p2, line2.p1, line2.p2);
        }

        public void Offset(int x, int y)
        {
            p1.Offset(x, y);
            p2.Offset(x, y);
        }

        public void Offset(Vector vector)
        {
			this.Offset((int)vector.X, (int)vector.Y);
        }

//        public static Line Rotate(Line line, Point2 point, double angle)
//        {
//            var p1 = Point2.Rotate(line.p1, point, angle);
//            var p2 = Point2.Rotate(line.p2, point, angle);
//            return new Line(p1, p2);
//        }

//        public void Rotate(Point2 point, double angle)
//        {
//            p1.Rotate(point, angle);
//            p2.Rotate(point, angle);
//        }

//        public void Rotate(double angle)
//        {
//            this.Rotate(Point2.Empty, angle);
//        }

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
            return Line.Project(line.p1, line.p2, point);
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

            return Vector.CrossProduct(v1, v2) / v2.Length;
        }

        public static double DistanceBetween(Line line, Point2 point)
        {
            return Line.DistanceBetween(line.p1, line.p2, point);
        }

        public double DistanceBetween(Point2 point)
        {
            return Line.DistanceBetween(this, point);
        }
    }
}
