using System.Collections.Generic;
using System.Collections;

using Cession.Geometries;

namespace Cession.Diagrams
{
    public abstract class CompositeShape:Shape,IEnumerable<Shape>
    {
        protected CompositeShape () : this (null)
        {
        }

        protected CompositeShape (Shape parent) : base (parent)
        {
        }

        public abstract IEnumerator<Shape> GetEnumerator ();

        IEnumerator IEnumerable.GetEnumerator ()
        {
            return this.GetEnumerator ();
        }

        internal override void DoOffset (int x, int y)
        {
            foreach (var s in this) {
                s.DoOffset (x, y);
            }
        }

        internal override void DoRotate (Point point, double radian)
        {
            foreach (var s in this) {
                s.DoRotate (point, radian);
            }
        }

        protected override Rect DoGetBounds ()
        {
            Rect bounds = Rect.Empty;
            foreach (var s in this) {
                bounds = bounds.Union (s.GetBounds ());
            }
            return bounds;
        }

        protected override Shape DoHitTest (Point point)
        {
            Shape shape = null;
            foreach (var s in this) {
                shape = s.HitTest (point);
                if (null != shape)
                    return shape;
            }
            return null;
        }
    }
}

