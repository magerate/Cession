using System;
using System.Linq;

using Cession.Geometries;
using Cession.Diagrams;

namespace Cession.Handles
{
    public static class HandleHelper
    {
        public static Handle[] CreateHandles(this Shape shape)
        {
            if (shape is Polyline)
            {
                var polyline = shape as Polyline;
                Handle[] handles = new Handle[polyline.Segments.Count + 1];
                for (int i = 0; i < polyline.Segments.Count; i++)
                {
                    handles [i] = new VertexHandle (polyline.Segments [i], polyline.Segments [i].Point1);
                }
                handles [polyline.Segments.Count] = new VertexHandle (polyline, polyline.LastPoint);
                return handles;
            }
            else if (shape is Path)
            {
                var path = shape as Path;
                Handle[] handles = new Handle[path.Segments.Count * 2];
                for (int i = 0; i < path.Segments.Count; i++)
                {
                    LineSegment lineSegment = path.Segments [i] as LineSegment;
                    handles [i] = new VertexHandle (path.Segments [i], lineSegment.Point1);
                    handles [i+path.Segments.Count] = new LineHandle (lineSegment, lineSegment.Middle);
                }
                return handles;
            }
            return null;
        }
    }
}

