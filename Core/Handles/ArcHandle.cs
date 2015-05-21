using System;

using Cession.Geometries;
using Cession.Diagrams;
using D = Cession.Diagrams;
using G = Cession.Geometries;

namespace Cession.Handles
{
    public class ArcHandle:Handle
    {
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

        public override Matrix GetHanldeTransform ()
        {
            var point = Transform.Transform (Location);
            double angle = GetMiddleTangentAngle () / Math.PI * 180;

            Matrix m = Matrix.Identity;
            m.RotateAt (angle, point.X, point.Y);
            return m;
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

