using System;
using Cession.Geometries;

namespace Cession.Diagrams
{
    public class Circle:ClosedShape
    {
        private Point _center;
        private double _radius;

        public event EventHandler<EventArgs> RadiusChanged;

        public Circle (Point center, double radius)
        {
            _center = center;
            _radius = radius;
        }

        public override Point Center
        {
            get{ return _center; }
        }

        public double Radius
        {
            get{ return _radius; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException ();
                
                if (value != _radius)
                {
                    _radius = value;
                    RadiusChanged?.Invoke (this,EventArgs.Empty);
                    OnContourChanged ();
                }
            }
        }

        protected override Rect DoGetBounds ()
        {
            return CalcBounds (_center, _radius);
        }

        public static Rect CalcBounds(Point center,double radius)
        {
            return new Rect (center.X - radius, center.Y - radius, 2 * radius, 2 * radius);
        }

        protected override bool DoContains (Point point)
        {
            return point.DistanceBetween (_center) <= _radius;
        }

        internal override void DoOffset (double x, double y)
        {
            _center.Offset (x, y);
        }

        internal override void DoRotate (Point point, double radian)
        {
            if (point == _center)
                return;

            _center.Rotate (point, radian);
        }

        public override double GetArea ()
        {
            return Math.PI * _radius * _radius;
        }

        public override double GetPerimeter ()
        {
            return 2 * Math.PI * _radius;
        }

        public override ClosedShape Inflate (double size)
        {
            return new Circle (_center, _radius + size);
        }

        public override Tuple<ClosedShape, ClosedShape> Split (Polyline polyline)
        {
            Point point1 = polyline.Segments [0].Point1;
            Point point2 = polyline.LastPoint;

            Vector v = point2 - point1;
            Vector v1 = v;
            v1.Normalize ();
            v1.Rotate (Math.PI / 2);
            v1 *= _radius;

            Vector v2 = v;
            v2.Normalize ();
            v2.Rotate (-Math.PI / 2);
            v2 *= _radius;

            Point p4 = _center + v2;

            return null;
        }
    }
}

