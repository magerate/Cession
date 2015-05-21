﻿using System;
using Cession.Geometries;

namespace Cession.Diagrams
{
    public class WallSurface:Shape
    {
        private Matrix _transform;
        private double _height;
        private Shape _shape;
        private Rect _bounds;

        public Matrix Transform
        {
            get{ return _transform; }
            internal set
            {
                _transform = value;
            }
        }
        public double Height
        {
            get{ return _height; }
            set
            { 
                if (value != _height)
                {
                    _height = value; 
                    _bounds.Height = _height;
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
            Ability = ShapeAbility.CanAssign | ShapeAbility.CanHitTest | ShapeAbility.CanSelect;
            _shape = shape;
            _height = height;

            double length = 0;

            if (shape is Segment)
            {
                Segment.LengthChanged += delegate
                {
                    _bounds.Width = Segment.Length;
                };
                length = Segment.Length;
            }
            else if (shape is Circle)
            {
                length = Circle.GetPerimeter ();
                Circle.RadiusChanged += delegate
                {
                    _bounds.Width = Circle.GetPerimeter();
                };
            }
            _bounds = new Rect (0, 0, length, height);
        }

        protected override Rect DoGetBounds ()
        {
            return _bounds;
        }

        protected override bool DoContains (Point point)
        {
            Matrix m = _transform;
            m.Invert ();
            point = m.Transform (point);
            return _bounds.Contains (point);
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

