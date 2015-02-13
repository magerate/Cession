using System;
using System.Collections.Generic;

using CoreGraphics;

using Cession.Diagrams;
using Cession.Geometries;

namespace Cession.Drawing
{
    public class RectangleDrawer:ShapeDrawer
    {
        protected override void DoDraw (DrawingContext drawingContext, Shape shape)
        {
            var rect = shape as Rectangle;
            if (null == rect)
                throw new ArgumentException ("shape");
            drawingContext.StrokeRect (rect.Rect);
        }
    }
}

