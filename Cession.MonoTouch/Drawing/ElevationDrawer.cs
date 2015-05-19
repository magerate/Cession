using System;
using System.Collections.Generic;

using CoreGraphics;

using Cession.Diagrams;
using Cession.Geometries;
using D = Cession.Diagrams;

namespace Cession.Drawing
{
    public class ElevationDrawer:ShapeDrawer
    {
        protected override void DoDraw (DrawingContext drawingContext, Shape shape)
        {
            var elevation = shape as Elevation;
            if (null == elevation)
                throw new ArgumentException ("shape");

            drawingContext.StrokeCloseShape (elevation.Contour);
            drawingContext.DrawDimension (elevation.Contour);

            if (!string.IsNullOrEmpty (elevation.Name))
            {
                drawingContext.DrawString (elevation.Name, elevation.Center);
            }
        }
    }
}

