using System.Linq;
using System.Collections.Generic;

using Cession.Geometries;

namespace Cession.Diagrams
{
	public class Path:CompositeShape,ISegmentHost
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

		public override IEnumerator<Shape> GetEnumerator ()
		{
			return _segments.GetEnumerator ();
		}

		protected override bool DoContains (Point2 point)
		{
			return false;
		}
	}
}

