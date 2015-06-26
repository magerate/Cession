using System;
using Cession.Geometries;

namespace Cession.Diagrams
{
    public class Rectangle:ClosedShape
    {
        private Rect _rect;
        private Matrix _transform;

        public Matrix Transform
        {
            get{ return _transform; }
            set{ _transform = value; }
        }

        public Rect Rect
        {
            get{ return _rect; }
            set{ _rect = value; }
        }

        public Rectangle (Rect rect)
        {
            _rect = rect;
            _transform = Matrix.Identity;
        }

        public double Width
        {
            get{ return _rect.Width; }
            set
            { 
                if(value != _rect.Width)
                    _rect.Width = value; 
            }
        }

        public double Height
        {
            get{ return _rect.Height; }
            set
            { 
                if(value != _rect.Height)
                    _rect.Height = value; 
            }
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
            throw new NotSupportedException ();
//            _transform.RotateAt (radian, point.X, point.Y);
        }

        public override ClosedShape Inflate (double size)
        {
            Rect rc = _rect;
            rc.Inflate (size, size);
            return new Rectangle (rc);
        }

        public override Tuple<ClosedShape, ClosedShape> Split (Polyline polyline)
        {
            return null;
        }
    }
}

