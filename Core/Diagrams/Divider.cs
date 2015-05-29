using System;
using Cession.Geometries;

namespace Cession.Diagrams
{
    public class Divider:Shape
    {
        private ClosedShape _shape1;
        private ClosedShape _shape2;

        private Polyline _polyline;

        public Divider (ClosedShape contour,Polyline polyline)
        {
            _polyline = polyline;
            _polyline.Ability = ShapeAbility.None;
            _polyline.Parent = this;
        }

        internal override void DoOffset (double x, double y)
        {
            _polyline.DoOffset (x, y);
        }

        internal override void DoRotate (Point point, double radian)
        {
            _polyline.DoRotate (point, radian);
        }

        protected override Rect DoGetBounds ()
        {
            return _polyline.GetBounds ();
        }

        protected override Shape DoHitTest (Point point)
        {
            return _polyline.HitTest (point);
        }
    }
}

