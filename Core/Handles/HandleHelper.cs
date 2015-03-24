using System;
using System.Collections.Generic;
using System.Linq;

using Cession.Geometries;
using Cession.Diagrams;
using D = Cession.Diagrams;

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
            List<Handle> handles = new List<Handle> ();
            foreach (var segment in polyline.Segments)
            {
                AppendHandle (segment, handles);
            }
            handles.Add (new VertexHandle (polyline.Segments.Last (), false));
            return handles.ToArray ();
        }

        private static Handle[] CreatePathHandle(Path path)
        {
            List<Handle> handles = new List<Handle> ();

            foreach (var segment in path.Segments)
            {
                AppendHandle (segment, handles);
            }
            return handles.ToArray ();
        }

        private static void AppendHandle(D.Segment segment,IList<Handle> handles)
        {
            if (segment is LineSegment)
            {
                var lineSegment = segment as LineSegment;
                var lineHandle = new LineHandle (lineSegment);
                handles.Add (lineHandle);
                var vertexHandle = new VertexHandle (lineSegment, true);
                handles.Add (vertexHandle);
            }
            else if (segment is ArcSegment)
            {
                var arcSegment = segment as ArcSegment;
                var arcHandle = new ArcHandle (arcSegment);
                handles.Add (arcHandle);
                var vertexHandle = new VertexHandle (arcSegment, true);
                handles.Add (vertexHandle);
            }
        }
    }
}

