using System;
using System.ComponentModel;
using System.Collections.Generic;
using Cession.Geometries;

namespace Cession.Diagrams
{
    [Description("Fan")]
    internal class FanLayoutProvider:LayoutProvider
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
            else if (null != wall.Circle)
            {
                Matrix m = Matrix.Identity;
                Rect bounds = wall.Circle.Bounds;
                m.Translate (bounds.Right + 16, bounds.Center.Y);
                wall.Transform = m;
            }
        }
    }
}

