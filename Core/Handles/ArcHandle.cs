using System;

using Cession.Geometries;
using Cession.Diagrams;
using D = Cession.Diagrams;
using G = Cession.Geometries;

namespace Cession.Handles
{
    public class ArcHandle:Handle
    {
        public static double Size{ get; set; }
        static ArcHandle ()
        {
            Size = 24;
        }

        public static Type TargetToolType{ get; set; }

        public override Type ToolType
        {
            get{ return TargetToolType; }
        }

        public ArcSegment ArcSegment
        {
            get{ return Shape as ArcSegment; }
        }

        public override Point Location
        {
            get{ return ArcSegment.GetMiddle(); }
        }

        public ArcHandle (ArcSegment arcSegment):base(arcSegment)
        {
        }

        public override bool Contains (Point point, Matrix transform)
        {
            double size = Size / transform.M11;
            Rect rect = new Rect (0, 0, size, size);

            double angle = GetMiddleTangentAngle ();
            Matrix m = Matrix.Identity;
            m.RotateAt (angle, size / 2, size / 2);
            m.Translate (Location.X - size / 2, Location.Y - size / 2);

            m.Invert ();

            Point ip = m.Transform (point);

            return rect.Contains (ip);
        }

        public double GetMiddleTangentAngle()
        {
            Point center = ArcSegment.GetCenter ();
            Point middle = ArcSegment.GetMiddle ();
            Vector v = middle - center;
            return v.Angle + Math.PI / 2;
        }

    }
}

