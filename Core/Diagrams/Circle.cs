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

    }
}

