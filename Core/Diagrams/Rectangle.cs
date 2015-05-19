using System;
using Cession.Geometries;

namespace Cession.Diagrams
{
    public class Rectangle:ClosedShape
    {
        private Rect _rect;
        private Matrix _transform;

        public Rect Rect
        {
            get{ return _rect; }
        }

        public Rectangle (Rect rect)
        {
            _rect = rect;
            _transform = Matrix.Identity;
        }

        protected override Rect DoGetBounds ()
        {
            return _rect;
        }

        protected override bool DoContains (Point point)
        {
            Matrix matrix = _transform;
            matrix.Invert ();
            var pp = matrix.Transform (point);
            return _rect.Contains (pp);
        }

        public override double GetArea ()
        {
            return _rect.Width * _rect.Height;
        }

        public override double GetPerimeter ()
        {
            return 2 * (_rect.Width + _rect.Height);
        }

        internal override void DoOffset (double x, double y)
        {
            _rect.Offset (x, y);
        }

        internal override void DoRotate (Point point, double radian)
        {
            _transform.RotateAt (radian, point.X, point.Y);
        }
    }
}

