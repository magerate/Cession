namespace Cession.Geometries
{
    using System;

    public struct Segment : IEquatable<Segment>
    {
        private Point2 p1;
        private Point2 p2;

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

		public Point2 Center
		{
			get{ return new Point2 ((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2); }
		}


        public Segment(Point2 p1, Point2 p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        public bool Equals(Segment segment)
        {
            return this.p1 == segment.p1 && this.p2 == segment.p2;
        }

        public override bool Equals(object obj)
        {
            if (null == obj || !(obj is Segment))
                return false;
            return Equals((Segment)obj);
        }

        public override int GetHashCode()
        {
            return p1.GetHashCode() ^ p2.GetHashCode();
        }

		public override string ToString ()
		{
			return string.Format ("[Segment: P1={0}, P2={1}, Center={2}]", P1, P2, Center);
		}

		public double Length{
			get{ return (p1 - p2).Length; }
		}

//        public static Point2? Intersect(Point2 p1, Point2 p2, Point2 p3, Point2 p4)
//        {
//            var cross = Line.Intersect(p1, p2, p3, p4);
//            if (cross.HasValue &&
//                Range.Contains(p1.X, p2.X, cross.Value.X) &&
//                Range.Contains(p3.X, p4.X, cross.Value.X))
//                return cross;
//
//            return null;
//        }

//        public static Point2? Intersect(Segment segment1, Segment segment2)
//        {
//            return Segment.Intersect(segment1.p1, segment1.p2, segment2.p1, segment2.p2);
//        }

//        public static Point2? IntersectWithLine(Point2 p1, Point2 p2, Point2 p3, Point2 p4)
//        {
//            var cross = Line.Intersect(p1, p2, p3, p4);
//            if (cross.HasValue &&
//                    Range.Contains(p1.X, p2.X, cross.Value.X) &&
//                    Range.Contains(p1.Y, p2.Y, cross.Value.Y))
//                return cross;
//
//            return null;
//        }

//        public static Point2? IntersectWithLine(Segment segment, Line line)
//        {
//            return Segment.IntersectWithLine(segment.p1, segment.p2, line.P1, line.P2);
//        }

//        public static bool Contains(Point2 p1, Point2 p2, Point2 point)
//        {
//            return Range.Contains(p1.X, p2.X, point.X) &&
//                Line.Contains(p1, p2, point);
//        }

//        public static bool Contains(Segment segment, Point2 point)
//        {
//            return Segment.Contains(segment.p1, segment.p2, point);
//        }
//
//        public static bool AlmostContains(Point2 p1, Point2 p2, Point2 point)
//        {
//            return Range.Contains(p1.X, p2.X, point.X, Constants.Epsilon) &&
//                Line.AlmostContains(p1, p2, point);
//        }
//
//        public static bool AlmostContains(Segment segment, Point2 point)
//        {
//            return Segment.AlmostContains(segment.p1, segment.p2, point);
//        }

    }
}
