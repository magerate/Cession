using System;
using System.Linq;

using Cession.Geometries;
using Cession.Tools;

namespace Cession.Drawing
{
    public static class PolygonMeasureDrawing
    {
        public static void Draw(this PolygonMeasurer polygonMeasurer,DrawingContext drawingContext)
        {
            if (polygonMeasurer.Points.Count > 0)
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
                if (polygonMeasurer.CurrentPoint != null)
                {
                    drawingContext.AddLineToPoint (polygonMeasurer.CurrentPoint.Value);
                }
                drawingContext.CGContext.StrokePath ();
            }
            else
            {
            }
        }
    }
}

