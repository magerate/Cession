using System;
using System.Collections.Generic;
using Cession.Geometries;

namespace Cession.Diagrams
{
    public class FanLayoutProvider:LayoutProvider
    {
        public override void Layout (ClosedShape contour, IEnumerable<WallSurface> walls)
        {
            foreach (var w in walls)
            {
                LayoutWall (w);
            }
        }

        private void LayoutWall(WallSurface wall)
        {
            var lineSegment = wall.Segment as LineSegment;
            if (null != lineSegment)
            {
                double angle = lineSegment.Angle / Math.PI * 180;
                Matrix m = Matrix.Identity;
                m.Rotate (angle);

                Point p = lineSegment.Point1;

                Vector v = lineSegment.Point2 - lineSegment.Point1;
                v.Rotate (-Math.PI / 2);
                v.Normalize ();
                v *= wall.Height;

                p += v;
                m.Translate (p.X, p.Y);
                wall.Transform = m;
            }
        }
    }
}

