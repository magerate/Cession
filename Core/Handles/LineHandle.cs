using System;

using Cession.Geometries;
using Cession.Diagrams;

namespace Cession.Handles
{
    public class LineHandle:Handle
    {
        public static double Size{ get; set;}

        static LineHandle()
        {
            Size = 24;
        }

        public static Type TargetToolType{ get; set;}

        public override Type ToolType
        {
            get{return TargetToolType;}
        }

        public LineHandle (LineSegment line):base(line,line.Middle)
        {
        }

        public override bool Contains (Point point, Matrix transform)
        {
//            double dx = Math.Abs (point.X - Location.X);
//            double dy = Math.Abs (point.Y - Location.Y);

            double size = Size / transform.M11;
            Rect rect = new Rect (0, 0, size, size);

            LineSegment line = Shape as LineSegment;

            Matrix m = Matrix.Identity;
            m.RotateAt (line.Angle, size / 2, size / 2);
            m.Translate (Location.X - size / 2, Location.Y - size / 2);

            m.Invert ();

            Point ip = m.Transform (point);

            return rect.Contains (ip);
//            return dx <= delta && dy <= delta;
        }
    }
}

