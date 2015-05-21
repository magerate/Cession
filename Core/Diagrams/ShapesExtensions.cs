using System;
using System.Linq;
using System.Collections.Generic;
using Cession.Geometries;

namespace Cession.Diagrams
{
    public static class ShapesExtensions
    {
        public static bool CanOffset(this IEnumerable<Shape> shapes)
        {
            return shapes.All (s => s.CanOffset);
        }

        public static Shape HitTest(this IEnumerable<Shape> shapes,Point point)
        {
            if (null == shapes)
                throw new ArgumentNullException ();

            foreach (var shape in shapes) 
            {
                var hs = shape.HitTest (point);
                if (null != hs)
                    return hs;
            }
            return null;
        }

        public static Rect GetBounds(this IEnumerable<Shape> shapes)
        {
            if (null == shapes)
                throw new ArgumentNullException ();

            var rect = Rect.Empty;
            foreach (var shape in shapes) 
            {
                rect = rect.Union (shape.Bounds);
            }
            return rect;
        }
    }
}

