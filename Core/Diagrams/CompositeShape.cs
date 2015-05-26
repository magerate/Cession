using System;
using System.Collections;
using System.Collections.Generic;

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

        internal override void DoOffset (double x, double y)
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
            return this.GetBounds ();
        }

        protected override Shape DoHitTest (Point point, Func<Shape, bool> predicate)
        {
            return this.HitTestAny (point,predicate);
        }
    }
}

