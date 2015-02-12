using System;
using Cession.Geometries;

namespace Cession.Diagrams
{
    public class Rectangle:Shape
    {
        private Rect _rect;
        private Matrix _transform;

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
            var matrix = _transform;
            matrix.Invert ();
            var pp = matrix.Transform (point);
            return _rect.Contains (pp);
        }

        public double GetArea ()
        {
            return _rect.Width * _rect.Height;
        }

        public double GetPerimeter ()
        {
            return 2 * (_rect.Width + _rect.Height);
        }

        internal override void DoOffset (double x, double y)
        {
            _transform.Translate (x, y);
        }

        internal override void DoRotate (Point point, double radian)
        {
            _transform.RotateAt (radian, point.X, point.Y);
        }
    }
}

