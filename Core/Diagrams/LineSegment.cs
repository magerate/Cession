using System;
using Cession.Geometries;

namespace Cession.Diagrams
{
    public class LineSegment:Segment
    {
        public event EventHandler<EventArgs> Moved;

        //specifed in radian
        public double Angle
        {
            get{ return (Point2 - Point1).Angle; }
        }

        public Point Middle
        {
            get{ return new Point ((Point1.X + Point2.X) / 2, (Point1.Y + Point2.Y) / 2); }
        }

        public override double Length
        {
            get
            {
                return Point1.DistanceBetween (Point2);
            }
        }

        public LineSegment (Point point) : base (point)
        {
        }

        protected override bool DoContains (Point point)
        {
            return Line.Contains (Point1, Point2, point);
        }

        protected override Rect DoGetBounds ()
        {
            return Rect.FromPoints (Point1, Point2);
        }

        protected override Shape DoHitTest (Point point)
        {
            Layer layer = Owner as Layer;
            double delta = layer.ConvertToLogicalLength (24);
            if (Math.Abs (Line.DistanceBetween (Point1, Point2, point)) <= delta &&
                Range.Contains (Point1.X, Point2.X, point.X, delta) &&
                Range.Contains (Point1.Y, Point2.Y, point.Y, delta))
                return this;
            return null;
        }

        public void Move(Tuple<Point,Point> pointPair)
        {
            Point1 = pointPair.Item1;
           
            if (Previous != null)
            {
                Previous.OnLengthChanged ();
            }

            if (Next != null)
            {
                Next.Point1 = pointPair.Item2;
                Next.OnLengthChanged ();
            }
            else
                ((Polyline)Parent).LastPoint = pointPair.Item2;

            OnMove ();
            OnLengthChanged ();
        }

        private void OnMove()
        {
            Moved?.Invoke (this, EventArgs.Empty);
        }

        public override Segment Reverse ()
        {
            return new LineSegment (Point2);
        }
    }
}

