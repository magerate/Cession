using System;
using System.Collections.Generic;

using CoreGraphics;
using UIKit;

using Cession.Diagrams;
using Cession.Geometries;
using D = Cession.Diagrams;

namespace Cession.Drawing
{
    public class WallSurfaceDrawer:ShapeDrawer
    {
        protected override void DoDraw (DrawingContext drawingContext, Shape shape)
        {
            var wallSurface = shape as WallSurface;
            if (null == wallSurface)
                throw new ArgumentException ("shape");

            drawingContext.PushTransform (wallSurface.Transform);
            drawingContext.StrokeRect (wallSurface.Bounds);
            drawingContext.PopTransform ();
        }

        protected override void DoDrawSelected (DrawingContext drawingContext, Shape shape)
        {
            var wallSurface = shape as WallSurface;
            if (null == wallSurface)
                throw new ArgumentException ("shape");

            drawingContext.PushTransform (wallSurface.Transform);
            UIColor.Blue.SetStroke ();
            drawingContext.CGContext.SetLineWidth (4.0f);
            drawingContext.StrokeRect (wallSurface.Bounds);
            drawingContext.PopTransform ();
        }
    }
}

