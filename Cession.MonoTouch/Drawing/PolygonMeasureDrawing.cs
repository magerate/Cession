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
            drawingContext.DrawPolyline (polygonMeasurer.Points);
            for (int i = 0; i < polygonMeasurer.Points.Count -1; i++)
            {
                Point p1 = polygonMeasurer.Points [i];
                Point p2 = polygonMeasurer.Points [i+1];
                if(polygonMeasurer.ArcPoints.ContainsKey(p1))
                {
                    Point p3 = polygonMeasurer.ArcPoints [p1];
                    drawingContext.StrokeArc (p1,p3, p2);
                }
                else
                {
                    drawingContext.StrokeLine (p1, p2);
                }
            }
            if (polygonMeasurer.CurrentPoint != null)
            {
                Point p1 = polygonMeasurer.Points.Last ();
                Point p2 = polygonMeasurer.CurrentPoint.Value;
                drawingContext.StrokeLine (p1, p2);
//                drawingContext.DrawDimension (p1, p2);
            }
        }
    }
}

