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
            var polyline = shape as Polyline;
            if (null == polyline)
                throw new ArgumentException ("shape");

            DrawPolyline (drawingContext, polyline);
        }

        private void DrawPolyline(DrawingContext drawingContext,Polyline polyline)
        {
            foreach (var s in polyline.Segments)
            {
                if (s is LineSegment)
                {
                    LineSegment ls = s as LineSegment;
                    drawingContext.StrokeLine (ls.Point1, ls.Point2);
                }
                else if(s is ArcSegment)
                {
                    ArcSegment arcs = s as ArcSegment;
                    drawingContext.StrokeArc (arcs.Point1, arcs.PointOnArc, arcs.Point2);
                }
            }
        }
    }
}

