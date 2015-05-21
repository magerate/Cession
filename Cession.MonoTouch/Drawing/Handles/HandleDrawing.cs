using System;
using System.Collections.Generic;

using Cession.Handles;
using Cession.Geometries;
using Cession.Diagrams;

using CoreGraphics;
using UIKit;

namespace Cession.Drawing.Handles
{
    public static class HandleDrawing
    {
        public static void Draw(this Handle handle,DrawingContext drawingContext)
        {
            if (null == handle)
                throw new ArgumentNullException ();
            if (null == drawingContext)
                throw new ArgumentNullException ();

            var rect = handle.Bounds.ToCGRect();
            var transform = handle.GetHanldeTransform ().ToCGAffineTransform ();

            CGContext context = drawingContext.CGContext;
            context.SaveState ();
            context.ConcatCTM (transform);
            context.SetLineWidth (2.0f);
            UIColor.Blue.SetFill ();
            context.AddRect (rect);
            context.DrawPath (CGPathDrawingMode.FillStroke);
            context.RestoreState ();
        }
    }
}

