using System;
using System.Collections.Generic;

using Cession.Geometries;

namespace Cession.Diagrams
{
    public class WallSurface:CompositeShape
    {
        private Shape _shape;

        private Rectangle _contour;

        public Matrix Transform
        {
            get{ return _contour.Transform; }
            internal set
            {
                _contour.Transform = value;
            }
        }

        public Rectangle Contour
        {
            get{ return _contour; }
        }

        public double Height
        {
            get{ return _contour.Rect.Height; }
            set
            { 
                if (value != Height)
                {
                    var rect = _contour.Rect;
                    rect.Height = value;
                    _contour.Rect = rect;
                }
            }
        }

        public Segment Segment
        {
            get{ return _shape as Segment; }
        }

        public Circle Circle
        {
            get{ return _shape as Circle; }
        }

        public WallSurface (Shape shape,double height)
        {
            Ability = ShapeAbility.CanSelect;
            _shape = shape;

            double length = 0;

            if (shape is Segment)
            {
                Segment.LengthChanged += delegate
                {
                    var rect = _contour.Rect;
                    rect.Width = Segment.Length;
                    _contour.Rect = rect;
                };
                length = Segment.Length;
            }
            else if (shape is Circle)
            {
                length = Circle.GetPerimeter ();
                Circle.RadiusChanged += delegate
                {
                    var rect = _contour.Rect;
                    rect.Width = Circle.GetPerimeter();
                    _contour.Rect = rect;
                };
            }

            Rect r = new Rect (0, 0, length, height);
            _contour = new Rectangle (r);
            _contour.Ability = ShapeAbility.None;
            _contour.Parent = this;
        }

        public override IEnumerator<Shape> GetEnumerator ()
        {
            yield return _contour;
        }

        protected override Rect DoGetBounds ()
        {
            return _contour.Bounds;
        }

        protected override bool DoContains (Point point)
        {
            return _contour.Contains (point);
        }

        internal override void DoOffset (double x, double y)
        {
            throw new NotSupportedException ();
        }

        internal override void DoRotate (Point point, double radian)
        {
            throw new NotSupportedException ();
        }
    }
}

