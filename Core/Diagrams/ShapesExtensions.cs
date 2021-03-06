﻿using System;
using System.Linq;
using System.Collections.Generic;
using Cession.Geometries;

namespace Cession.Diagrams
{
    public static class ShapesExtensions
    {
        public static void ForEach(this IEnumerable<Shape> shapes,Action<Shape> action)
        {
            if (null == action)
                throw new ArgumentNullException ();
            
            foreach (var s in shapes)
            {
                action (s);
            }
        }

        public static T Any<T>(this IEnumerable<Shape> shapes,Func<Shape,T> func) where T:Shape
        {
            if (func == null)
                throw new ArgumentNullException ();
            
            foreach (var shape in shapes) 
            {
                T result = func (shape);
                if (null != result)
                    return result;
            }
            return null;
        }

        public static bool CanOffset(this IEnumerable<Shape> shapes)
        {
            return shapes.All (s => s.CanOffset);
        }

        public static Shape HitTestAny(this IEnumerable<Shape> shapes,Point point, Func<Shape,bool> predicate = null)
        {
            return shapes.Any<Shape> (s => s.HitTest (point,predicate));
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

