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
            set
            { 
                if (value != _lastPoint)
                {
                    _lastPoint = value; 
                    var ea = new VertexChangedEventArgs (Segment.VertexChangeEvent, _segments.Last(),value);
                    ea.IsFirstVertex = false;
                    RaiseEvent (ea);
                }
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
                segment.Ability = ShapeAbility.CanHitTest | ShapeAbility.CanAssign;
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

        protected override Shape DoHitTest (Point point)
        {
            return base.HitTestAny (point);
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

