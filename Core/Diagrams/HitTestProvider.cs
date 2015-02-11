using System;
using Cession.Geometries;

namespace Cession.Diagrams
{
    public abstract class HitTestProvider
    {
        public Rect GetBounds (Shape shape)
        {
            if (null == shape)
                throw new ArgumentNullException ();
            return DoGetBounds (shape);
        }

        public bool Contains (Shape shape, Point point)
        {
            if (null == shape)
                throw new ArgumentNullException ();

            return DoContains (shape, point);
        }


        public Shape HitTest (Shape shape, Point point)
        {
            if (null == shape)
                throw new ArgumentNullException ();
            return DoHitTest (shape, point);
        }

        protected abstract Rect DoGetBounds (Shape shape);

        protected virtual bool DoContains (Shape shape, Point point)
        {
            return DoGetBounds (shape).Contains (point);
        }

        protected virtual Shape DoHitTest (Shape shape, Point point)
        {
            if (Contains (shape, point))
                return shape;
            return null;
        }
    }
}

