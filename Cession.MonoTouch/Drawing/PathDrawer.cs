using System;
using System.Collections.Generic;

using CoreGraphics;

using Cession.Diagrams;
using Cession.Geometries;

namespace Cession.Drawing
{
    public class PathDrawer:ShapeDrawer
    {
        protected override void DoDraw (DrawingContext drawingContext, Shape shape)
        {
            var path = shape as Path;
            if(null == path)
                throw new ArgumentException("shape");

            foreach (var s in path)
            {
                var ls = s as LineSegment;
                drawingContext.StrokeLine (ls.Point1, ls.Point2);
            }
        }
    }
}

