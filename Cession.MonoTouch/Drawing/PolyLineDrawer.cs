﻿using System;
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
            var polyline = shape as PolyLine;
            if (null == polyline)
                throw new ArgumentException ("shape");

            DrawPolyline (drawingContext, polyline);
        }

        private void DrawPolyline(DrawingContext drawingContext,PolyLine polyline)
        {
            foreach (var s in polyline.Segments)
            {
                LineSegment ls = s as LineSegment;
                drawingContext.StrokeLine (ls.Point1, ls.Point2);
                drawingContext.DrawDimension (ls.Point1, ls.Point2);
            }
        }
    }
}

