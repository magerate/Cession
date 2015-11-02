using System;
using System.Collections.Generic;

using Cession.Geometries;

namespace Cession.Diagrams
{
    public class WallSurface:CompositeShape,IFloatable
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

        public double Height
        {
            get{ return _contour.Height; }
            set{ _contour.Height = value; }
        }

        public Rectangle Contour
        {
            get{ return _contour; }
        }

        public Segment Segment
        {
            get{ return _shape as Segment; }
        }

        public Circle Circle
        {
            get{ return _shape as Circle; }
        }

        public IFloatableHost Host
        {
            get{ return Parent as IFloatableHost; }
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
                    _contour.Width = Segment.Length;
                };
                length = Segment.Length;
            }
            else if (shape is Circle)
            {
                length = Circle.GetPerimeter ();
                Circle.RadiusChanged += delegate
                {
                    _contour.Width = Circle.GetPerimeter();
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

