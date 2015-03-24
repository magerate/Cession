using System;

using Cession.Geometries;
using Cession.Diagrams;
using Cession.Drawing;

using D = Cession.Diagrams;

namespace Cession.Tools
{
    public static class ToolDrawHelper
    {
        public static void DrawSegment(this DrawingContext drawingContext,D.Segment segment,Point point,bool isFirst)
        {
            if (segment is LineSegment)
            {
                if(isFirst)
                    drawingContext.StrokeLine (segment.Point1, point);
                else
                    drawingContext.StrokeLine (point,segment.Point2);
            }
            else if(segment is ArcSegment)
            {
                ArcSegment arcSegment = segment as ArcSegment;
                if(isFirst)
                    drawingContext.StrokeArc (segment.Point1, arcSegment.PointOnArc,point);
                else
                    drawingContext.StrokeArc (segment.Point2, arcSegment.PointOnArc,point);
            }
        }
    }
}

