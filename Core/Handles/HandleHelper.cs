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
                return CreatePolylineHandle (shape as Polyline);
            }
            else if (shape is Path)
            {
                return CreatePathHandle (shape as Path);
            }
            return null;
        }

        private static Handle[] CreatePolylineHandle(Polyline polyline)
        {
            Handle[] handles = new Handle[polyline.Segments.Count * 2 + 1];
            for (int i = 0; i < polyline.Segments.Count; i++)
            {
                LineSegment lineSegment = polyline.Segments [i] as LineSegment;
                handles [i] = new VertexHandle (polyline.Segments [i],true);
                handles [i+polyline.Segments.Count +1] = new LineHandle (lineSegment);
            }
            handles [polyline.Segments.Count] = new VertexHandle (polyline.Segments.Last(),false);
            return handles;
        }

        private static Handle[] CreatePathHandle(Path path)
        {
            Handle[] handles = new Handle[path.Segments.Count * 2];
            for (int i = 0; i < path.Segments.Count; i++)
            {
                LineSegment lineSegment = path.Segments [i] as LineSegment;
                handles [i] = new VertexHandle (path.Segments [i], true);
                handles [i+path.Segments.Count] = new LineHandle (lineSegment);
            }
            return handles;
        }
    }
}

