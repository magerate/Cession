using System;
using Cession.Geometries;
using G = Cession.Geometries;

namespace Cession.Diagrams
{
    public class ArcSegment:Segment
    {
        private Point _pointOnArc;

        public Point PointOnArc
        {
            get{ return _pointOnArc; }
            set{ _pointOnArc = value; }
        }

        public Point GetMiddle()
        {
            Point center = GetCenter ();
            double r = center.DistanceBetween (Point1);

            Vector v1 = Point1 - center;
            Vector v2 = Point2 - center;

            double angle1 = (v1.Angle + v2.Angle) / 2;
            double angle2 = (angle1 + Math.PI) % (Math.PI * 2);

            Vector v3 = _pointOnArc - center;

            double angle;
            if (Math.Abs (angle1 - v3.Angle) <= Math.Abs (angle2 - v3.Angle))
                angle = angle1;
            else
                angle = angle2;

            Vector vv = new Vector (r * Math.Cos (angle), r * Math.Sin (angle));
            return center + vv;
        }

        public Point GetCenter()
        {
            return G.Circle.GetCenter (Point1, PointOnArc, Point2).Value;
        }

        public ArcSegment (Point startPoint,Point pointOnArc):base(startPoint)
        {
            _pointOnArc = pointOnArc;
        }

        protected override Rect DoGetBounds ()
        {
            Point center = GetCenter ();
            double r = center.DistanceBetween (Point1);
            return new Rect (center.X - r, center.Y - r, 2 * r, 2 * r);
        }

        protected override Shape DoHitTest (Point point)
        {
            Layer layer = Owner as Layer;
            double delta = layer.ConvertToLogicalLength (24);

            Point center = GetCenter ();
            double distance = center.DistanceBetween (point);
            double r = center.DistanceBetween (Point1);
            if (Math.Abs (distance - r) <= delta && IsClamped(point,center))
                return this;
            return null;
        }

        private bool IsClamped(Point point,Point center)
        {
            Vector v1 = center - Point1;
            Vector v2 = Point2 - center;

            Vector v11 = point - Point1;
            Vector v22 = point - center;

            return v1 * v11 >= 0 && v2 * v22 >= 0;
        }
    }
}

