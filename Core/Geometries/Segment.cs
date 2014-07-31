﻿namespace Cession.Geometries
{
    using System;

    public struct Segment : IEquatable<Segment>
    {
        private Point2d p1;
        private Point2d p2;

        public Point2d P1
        {
            get { return p1; }
            set { p1 = value; }
        }

        public Point2d P2
        {
            get { return p2; }
            set { p2 = value; }
        }

        public Segment(Point2d p1, Point2d p2)
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

        public static Point2d? CrossBetween(Point2d p1, Point2d p2, Point2d p3, Point2d p4)
        {
            var cross = Line.Intersect(p1, p2, p3, p4);
            if (cross.HasValue &&
                Range.Contains(p1.X, p2.X, cross.Value.X) &&
                Range.Contains(p3.X, p4.X, cross.Value.X))
                return cross;

            return null;
        }

        public static Point2d? CrossBetween(Segment segment1, Segment segment2)
        {
            return Segment.CrossBetween(segment1.p1, segment1.p2, segment2.p1, segment2.p2);
        }

        public static Point2d? CrossBetweenLine(Point2d p1, Point2d p2, Point2d p3, Point2d p4)
        {
            var cross = Line.Intersect(p1, p2, p3, p4);
            if (cross.HasValue &&
                    Range.Contains(p1.X, p2.X, cross.Value.X) &&
                    Range.Contains(p1.Y, p2.Y, cross.Value.Y))
                return cross;

            return null;
        }

        public static Point2d? CrossBetweenLine(Segment segment, Line line)
        {
            return Segment.CrossBetweenLine(segment.p1, segment.p2, line.P1, line.P2);
        }

        public static bool Contains(Point2d p1, Point2d p2, Point2d point)
        {
            return Range.Contains(p1.X, p2.X, point.X) &&
                Line.Contains(p1, p2, point);
        }

        public static bool Contains(Segment segment, Point2d point)
        {
            return Segment.Contains(segment.p1, segment.p2, point);
        }

        public static bool AlmostContains(Point2d p1, Point2d p2, Point2d point)
        {
            return Range.Contains(p1.X, p2.X, point.X, Constants.Epsilon) &&
                Line.AlmostContains(p1, p2, point);
        }

        public static bool AlmostContains(Segment segment, Point2d point)
        {
            return Segment.AlmostContains(segment.p1, segment.p2, point);
        }

    }
}
