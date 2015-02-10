using System;
using System.Collections;
using System.Collections.Generic;

using Cession.Geometries;

namespace Cession.Diagrams
{
	public class PolyLine:Shape,IReadOnlyList<Point>
	{
		private List<Segment> _segments;
		private Point _lastPoint;

		public Point LastPoint{
			get{ return _lastPoint; }
		}

		public PolyLine (IReadOnlyList<Point> points)
		{
			if (null == points)
				throw new ArgumentNullException ();

			if (points.Count < 2)
				throw new ArgumentException ();

			for (int i = 0; i < points.Count - 1; i++) {
				var segment = new LineSegment (points [i]);
				segment.Parent = this;
				_segments.Add (segment);
			}
			_lastPoint = points [points.Count - 1];
		}

		public int Count{
			get{ return _segments.Count + 1; }
		}

		public Point this[int index]{
			get{
				if (index < 0 || index >= Count)
					throw new ArgumentOutOfRangeException ();

				if (index == Count - 1)
					return _lastPoint;
				return _segments [index].Point1;
			}
		}

		public IEnumerator<Point> GetEnumerator(){
			foreach (var s in _segments) {
				yield return s.Point1;
			}
			yield return _lastPoint;
		}

		IEnumerator IEnumerable.GetEnumerator(){
			return this.GetEnumerator ();
		}

		internal override void DoOffset (int x, int y)
		{
			_segments.ForEach (s => s.DoOffset (x, y));
		}

		internal override void DoRotate (Point point, double radian)
		{
			_segments.ForEach(s => s.DoRotate(point,radian));
		}


		public Segment GetNextSide(Segment segment){
			if (null == segment)
				throw new ArgumentNullException ();

			if (!_segments.Contains (segment))
				return null;

			var index = _segments.IndexOf (segment) + 1;
			if(index < _segments.Count)
				return _segments [index];
			return null;
		}

		public Segment GetPreviousSide(Segment segment){
			if (null == segment)
				throw new ArgumentNullException ();

			if (!_segments.Contains (segment))
				return null;

			var index = _segments.IndexOf (segment) - 1;
			if(index >= 0)
				return _segments [index];
			return null;
		}
	}
}

