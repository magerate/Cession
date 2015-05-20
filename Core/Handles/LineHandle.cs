using System;

using Cession.Geometries;
using Cession.Diagrams;
using D = Cession.Diagrams;
using G = Cession.Geometries;

namespace Cession.Handles
{
    public class LineHandle:Handle
    {
        public static Type TargetToolType{ get; set; }

        public override Type ToolType
        {
            get{ return TargetToolType; }
        }

        public LineSegment Line
        {
            get{ return Shape as LineSegment; }
        }

        public override Point Location
        {
            get{ return Line.Middle; }
        }

        public LineHandle (LineSegment line) : base (line)
        {
        }

        public override Matrix GetHanldeTransform ()
        {
            var point = Transform.Transform (Location);
            LineSegment line = Shape as LineSegment;
            Matrix m = Matrix.Identity;
            m.RotateAt (line.Angle, point.X, point.Y);
            return m;
        }

        public Tuple<Point,Point> MoveLine(Point point)
        {
            D.Segment segment = Line;
            D.Segment prevSegment = segment.Previous;
            D.Segment nextSegment = segment.Next;

            Point p = Point.Project (segment.Point1, segment.Point2, point);
            Vector v = point - p;

            Point p1 = segment.Point1 + v;
            Point p2 = segment.Point2 + v;

            if (prevSegment != null)
            {
                Point? ip = G.Line.Intersect (prevSegment.Point1, prevSegment.Point2, p1, p2);
                if (ip != null)
                    p1 = ip.Value;
            }

            if (nextSegment != null)
            {
                Point? ip = G.Line.Intersect (nextSegment.Point1, nextSegment.Point2, p1, p2);
                if (ip != null)
                    p2 = ip.Value;
            }

            return new Tuple<Point, Point> (p1, p2);
        }
    }
}

