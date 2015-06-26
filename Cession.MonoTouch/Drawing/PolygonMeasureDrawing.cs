using System;
using System.Linq;

using Cession.Geometries;
using Cession.Tools;

using CoreGraphics;
using UIKit;

namespace Cession.Drawing
{
    public static class PolygonMeasureDrawing
    {
        private static void DrawCircle(this DrawingContext drawingContext,Point point,double radius,UIColor color)
        {
            drawingContext.AddCircle (point, radius);
            drawingContext.SaveState ();
            color.SetFill ();
            drawingContext.CGContext.DrawPath (CGPathDrawingMode.FillStroke);
            drawingContext.RestoreState ();
        }

        private static void DrawCircle(this DrawingContext drawingContext,Point point)
        {
            double radius = drawingContext.ConvertToLogicalSize(6);
            drawingContext.DrawCircle (point, radius, UIColor.Blue);
        }

        private static void DrawLastPoint(this PolygonMeasurer polygonMeasurer,DrawingContext drawingContext)
        {
            if (polygonMeasurer.Points.Count > 0)
            {
                drawingContext.DrawCircle (polygonMeasurer.Points.Last ());
            }
        }

        private static void DrawAddedLines(this PolygonMeasurer polygonMeasurer,DrawingContext drawingContext)
        {
            if (polygonMeasurer.Points.Count > 1)
            {
                drawingContext.MoveToPoint (polygonMeasurer.Points [0]);
                for (int i = 0; i < polygonMeasurer.Points.Count - 1; i++)
                {
                    Point p1 = polygonMeasurer.Points [i];
                    Point p2 = polygonMeasurer.Points [i + 1];
                    if (polygonMeasurer.ArcPoints.ContainsKey (p1))
                    {
                        Point p3 = polygonMeasurer.ArcPoints [p1];
                        drawingContext.AddArc (p1, p3, p2);
                    }
                    else
                    {
                        drawingContext.AddLineToPoint (p2);
                    }
                }
                drawingContext.CGContext.StrokePath ();
            }
        }

        private static void DrawCurrentLine(this PolygonMeasurer polygonMeasurer,DrawingContext drawingContext)
        {
            if (polygonMeasurer.CurrentPoint == null)
                return;

            if (polygonMeasurer.Points.Count == 0)
            {
                drawingContext.DrawCircle (polygonMeasurer.CurrentPoint.Value);
            }
            else
            {
                drawingContext.StrokeLine (polygonMeasurer.Points.Last (), 
                    polygonMeasurer.CurrentPoint.Value);
            }
        }

        public static void Draw(this PolygonMeasurer polygonMeasurer,DrawingContext drawingContext)
        {
            DrawAddedLines (polygonMeasurer, drawingContext);
            DrawLastPoint (polygonMeasurer, drawingContext);
            DrawCurrentLine (polygonMeasurer, drawingContext);
        }
    }
}

