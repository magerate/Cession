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

            drawingContext.StrokePath (path);
        }
    }
}

