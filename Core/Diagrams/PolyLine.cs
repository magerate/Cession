using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Cession.Geometries;

namespace Cession.Diagrams
{
    public class Polyline:CompositeShape
    {
        private List<Segment> _segments;
        private Point _lastPoint;

        public IReadOnlyList<Segment> Segments
        {
            get{ return _segments; }
        }

        public Point LastPoint
        {
            get{ return _lastPoint; }
            internal set{ _lastPoint = value; }
        }

        public Point FirstPoint
        {
            get{ return _segments [0].Point1; }
        }

        public void MoveLastPoint(Point point)
        {
            if (point != _lastPoint)
            {
                _lastPoint = point; 
                var ea = new VertexChangedEventArgs (Segment.VertexChangeEvent, _segments.Last(),point);
                ea.IsFirstVertex = false;
                RaiseEvent (ea);
            }
        }

        public Polyline (IEnumerable<Segment> segments,Point lastPoint)
        {
            if (null == segments)
                throw new ArgumentNullException ();

            if (segments.Count () < 1)
                throw new ArgumentException ("segments");

            _segments = segments.ToList ();
            _lastPoint = lastPoint;
            foreach (var s in _segments)
            {
                s.Parent = this;
                s.Ability = ShapeAbility.None;
            }
        }

        public Polyline (IReadOnlyList<Point> points)
        {
            if (null == points)
                throw new ArgumentNullException ();

            if (points.Count < 2)
                throw new ArgumentException ();

            _segments = new List<Segment> (points.Count);

            for (int i = 0; i < points.Count - 1; i++)
            {
                var segment = new LineSegment (points [i]);
                segment.Parent = this;
                segment.Ability = ShapeAbility.None;
                _segments.Add (segment);
            }
            _lastPoint = points [points.Count - 1];
        }

        public IEnumerable<Point> GetVertices()
        {
            foreach (var s in _segments)
            {
                yield return s.Point1;
            }
            yield return _lastPoint;
        }

        public override IEnumerator<Shape> GetEnumerator ()
        {
            return _segments.GetEnumerator ();
        }

        public Segment GetNextSide (Segment segment)
        {
            if (null == segment)
                throw new ArgumentNullException ();

            if (!_segments.Contains (segment))
                return null;

            var index = _segments.IndexOf (segment) + 1;
            if (index < _segments.Count)
                return _segments [index];
            return null;
        }

        public Segment GetPreviousSide (Segment segment)
        {
            if (null == segment)
                throw new ArgumentNullException ();

            if (!_segments.Contains (segment))
                return null;

            var index = _segments.IndexOf (segment) - 1;
            if (index >= 0)
                return _segments [index];
            return null;
        }

        public Polyline Reverse()
        {
            Segment[] segments = new Segment[_segments.Count];
            for (int i = 0; i < _segments.Count; i++)
            {
                segments [i] = _segments [_segments.Count - i - 1].Reverse ();
            }
            return new Polyline (segments, _segments[0].Point1);
        }

        protected override bool DoContains (Point point)
        {
            return false;
        }

        internal override void DoOffset (double x, double y)
        {
            base.DoOffset (x, y);
            _lastPoint.Offset (x, y);
        }
    }
}

