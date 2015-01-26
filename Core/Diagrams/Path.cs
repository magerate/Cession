using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Cession.Geometries;

namespace Cession.Diagrams
{
	public class Path:ClosedShape,ISegmentHost,IReadOnlyList<Point2>
	{
		private List<Segment> _segments;

		public Path (IEnumerable<Point2> points)
		{
			_segments = new List<Segment> (points.Count());
			foreach (var p in points) {
				var segment = new LineSegment (p);
				segment.Parent = this;
				_segments.Add (segment);
			}
		}

		Point2 ISegmentHost.GetNextPoint (Segment segment){
			return ((ISegmentHost)this).GetNextSide (segment).Point1;
		}

		Segment ISegmentHost.GetNextSide(Segment segment){
			var index = (_segments.IndexOf (segment) + 1) % _segments.Count;
			return _segments [index];
		}

		Segment ISegmentHost.GetPreviousSide(Segment segment){
			var index = (_segments.IndexOf (segment) - 1) % _segments.Count;
			return _segments [index];
		}

		public int Count{
			get{ return _segments.Count; }
		}

		public Point2 this[int index]{
			get{ 
				if (index < 0 || index >= _segments.Count)
					throw new ArgumentOutOfRangeException ();
				return _segments [index].Point1; 
			}
		}

		public IEnumerator<Point2> GetEnumerator(){
			foreach (var s in _segments) {
				yield return s.Point1;
			}
		}

		IEnumerator IEnumerable.GetEnumerator(){
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

		protected override bool DoContains (Point2 point)
		{
			return false;
		}

		internal override void DoOffset (int x, int y)
		{
			_segments.ForEach (s => s.DoOffset (x, y));
		}

		internal override void DoRotate (Point2 point, double radian)
		{
			_segments.ForEach (s => s.DoRotate (point,radian));
		}

	}
}

