using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Cession.Geometries;

namespace Cession.Diagrams
{
    public class Path:ClosedShape,IReadOnlyList<Point>
    {
        private List<Segment> _segments;

        public IReadOnlyList<Segment> Segments
        {
            get{ return _segments; }
        }

        public Path (IEnumerable<Segment> segments)
        {
            if (null == segments)
                throw new ArgumentNullException ();

            if (segments.Count () < 3)
                throw new ArgumentException ("segments");

            _segments = segments.ToList ();
        }

        public Path (IReadOnlyList<Point> points)
        {
            if (null == points)
                throw new ArgumentNullException ();

            if (points.Count < 3)
                throw new ArgumentException ();

            _segments = new List<Segment> (points.Count);

            for (int i = 0; i < points.Count; i++)
            {
                var segment = new LineSegment (points [i]);
                segment.Parent = this;
                segment.Ability = ShapeAbility.None;
                _segments.Add (segment);

                segment.VertexChanged += delegate
                {
                    OnContourChanged();
                };
                segment.Moved += delegate
                {
                    OnContourChanged();
                };
            }
        }

        public Segment GetNextSide (Segment segment)
        {
            if (null == segment)
                throw new ArgumentNullException ();

            if (!_segments.Contains (segment))
                return null;

            var index = (_segments.IndexOf (segment) + 1) % _segments.Count;
            return _segments [index];
        }

        public Segment GetPreviousSide (Segment segment)
        {
            if (null == segment)
                throw new ArgumentNullException ();

            if (!_segments.Contains (segment))
                return null;

            int index = _segments.IndexOf (segment) - 1;
            index = index >= 0 ? index : _segments.Count - 1;
            return _segments [index];
        }

        public int Count
        {
            get{ return _segments.Count; }
        }

        public Point this [int index]
        {
            get
            { 
                if (index < 0 || index >= _segments.Count)
                    throw new ArgumentOutOfRangeException ();
                return _segments [index].Point1; 
            }
        }

        public IEnumerator<Point> GetEnumerator ()
        {
            foreach (var s in _segments)
            {
                yield return s.Point1;
            }
        }

        IEnumerator IEnumerable.GetEnumerator ()
        {
            return GetEnumerator ();
        }

        public override double GetArea ()
        {
            return 0;
        }

        public override double GetPerimeter ()
        {
            return 0;
        }

        protected override Rect DoGetBounds ()
        {
            return Polygon.GetBounds (this);
        }

        protected override bool DoContains (Point point)
        {
            return Polygon.Contains (point, this);
        }

        internal override void DoOffset (double x, double y)
        {
            foreach (var segment in _segments)
            {
                segment.DoOffset (x, y);
            }
        }

        internal override void DoRotate (Point point, double radian)
        {
            foreach (var segment in _segments)
            {
                segment.DoRotate (point, radian);
            }
        }

        protected override Shape DoHitTest (Point point)
        {
            Shape shape = _segments.HitTestAny (point);
            if (null != shape)
                return shape;
           
            if (DoContains (point))
                return this;

            return null;
        }

        public override ClosedShape Inflate (double size)
        {
            return PolygonHelper.Inflate (this, size);
        }
    }
}

