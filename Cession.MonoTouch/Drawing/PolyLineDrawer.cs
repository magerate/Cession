using System;
using System.Collections.Generic;

using CoreGraphics;

using Cession.Diagrams;
using Cession.Geometries;

namespace Cession.Drawing
{
    public class PolyLineDrawer:ShapeDrawer
    {
        protected override void DoDraw (DrawingContext drawingContext, Shape shape)
        {
            var polyLine = shape as PolyLine;
            if (null == polyLine)
                throw new ArgumentException ("shape");

            DrawPolyline (drawingContext, polyLine);
        }

        public static void DrawPolyline(DrawingContext drawingContext,IReadOnlyList<Point> polyline)
        {
            if (null == drawingContext)
                throw new ArgumentNullException ();

            if (null == polyline)
                throw new ArgumentNullException ();

            if (polyline.Count <= 1)
                return;

            BuildPolyLinePath (drawingContext, polyline);
            drawingContext.CGContext.StrokePath ();

            for (int i = 0; i < polyline.Count - 1; i++)
            {
                drawingContext.DrawDimension (polyline [i], polyline [i + 1]);
            }
        }

        private static void BuildPolyLinePath(DrawingContext drawingContext,IReadOnlyList<Point> polyline)
        {
            Point p1 = drawingContext.Transform.Transform (polyline [0]);
            CGContext context = drawingContext.CGContext;
            context.MoveTo ((nfloat)p1.X, (nfloat)p1.Y);
            for (int i = 1; i < polyline.Count; i++)
            {
                Point pi = drawingContext.Transform.Transform(polyline [i]);
                context.AddLineToPoint ((nfloat)pi.X, (nfloat)pi.Y);
            }
        }
    }
}

