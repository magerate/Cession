using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Cession.Geometries;

namespace Cession.Diagrams
{
    public class ShapeCollection:Collection<Shape>
    {
        public ShapeCollection ()
        {
        }
    }

    public static class ShapesExtensions
    {
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
                rect = rect.Union (shape.GetBounds ());
            }
            return rect;
        }
    }
}

