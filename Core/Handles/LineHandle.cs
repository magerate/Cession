using System;

using Cession.Geometries;
using Cession.Diagrams;
using D = Cession.Diagrams;
using G = Cession.Geometries;

namespace Cession.Handles
{
    public class LineHandle:Handle
    {
        public static double Size{ get; set; }

        static LineHandle ()
        {
            Size = 24;
        }

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

        public override bool Contains (Point point, Matrix transform)
        {
            double size = Size / transform.M11;
            Rect rect = new Rect (0, 0, size, size);

            LineSegment line = Shape as LineSegment;

            Matrix m = Matrix.Identity;
            m.RotateAt (line.Angle, size / 2, size / 2);
            m.Translate (Location.X - size / 2, Location.Y - size / 2);

            m.Invert ();

            Point ip = m.Transform (point);

            return rect.Contains (ip);
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

