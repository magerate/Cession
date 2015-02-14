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
            return null;
        }
    }
}

