using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Cession.Geometries;

namespace Cession.Diagrams
{
    public class Region:CompositeShape
    {
        private ClosedShape _contour;
        private List<ClosedShape> _holes = null;

        public ClosedShape Contour 
        {
            get{ return _contour; }
        }

        public IList<ClosedShape> Holes 
        {
            get{ return _holes; }
        }

        public Region (ClosedShape contour)
        {
            if (null == contour)
                throw new ArgumentNullException ();

            _contour = contour;
        }

        public override IEnumerator<Shape> GetEnumerator ()
        {
            yield return _contour;

            if (null != _holes) {
                foreach (var h in _holes) {
                    yield return h;
                }
            }
        }

        internal override void DoOffset (double x, double y)
        {
            var shapes = this.Where (s => s.Parent == this);
            foreach (var s in shapes)
            {
                s.DoOffset (x, y);
            }
        }

        internal override void DoRotate (Point point, double radian)
        {
            var shapes = this.Where (s => s.Parent == this);
            foreach (var s in shapes)
            {
                s.DoRotate (point, radian);
            }
        }
    }
}

