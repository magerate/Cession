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

            for (int i = 0; i < points.Count; i++) 
            {
                var segment = new LineSegment (points [i]);
                segment.Parent = this;
                _segments.Add (segment);
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

            var index = (_segments.IndexOf (segment) - 1) % _segments.Count;
            return _segments [index];
        }

        public int Count {
            get{ return _segments.Count; }
        }

        public Point this [int index] {
            get { 
                if (index < 0 || index >= _segments.Count)
                    throw new ArgumentOutOfRangeException ();
                return _segments [index].Point1; 
            }
        }

        public IEnumerator<Point> GetEnumerator ()
        {
            foreach (var s in _segments) {
                yield return s.Point1;
            }
        }

        IEnumerator IEnumerable.GetEnumerator ()
        {
            return this.GetEnumerator ();
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
            return Rect.Empty;
        }

        protected override bool DoContains (Point point)
        {
            return false;
        }

        internal override void DoOffset (double x, double y)
        {
            _segments.ForEach (s => s.DoOffset (x, y));
        }

        internal override void DoRotate (Point point, double radian)
        {
            _segments.ForEach (s => s.DoRotate (point, radian));
        }

    }
}

