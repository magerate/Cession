using System;
using System.Collections.Generic;
using Cession.Geometries;

namespace Cession.Diagrams
{
    public class FlowLayoutProvider:LayoutProvider
    {
        public override void Layout (ClosedShape contour, IEnumerable<WallSurface> walls)
        {
            double margin = 32;
            double maxWidth = 400;

            var bounds = contour.Bounds;

            double maxX = bounds.Right + margin + maxWidth;

            double ox = bounds.Right + margin;
            double oy = bounds.Y - margin;

            double tx = ox;
            double ty = oy;

            foreach (var w in walls)
            {
                Matrix m = new Matrix ();
                m.Translate (tx, ty);
                w.Transform = m;

                tx += w.Bounds.Width + margin;
                if (tx >= maxX)
                {
                    tx = ox;
                    ty += w.Height + margin;
                }
            }
        }
    }
}

