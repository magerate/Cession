using Cession.Geometries;

namespace Cession.Diagrams
{
	public class LineSegment:Segment
	{
		public Point Point{
			get{ 
				var segment = Next;
				if (segment != null)
					return segment.Point1;
				return ((PolyLine)Parent).LastPoint;
			}
		}

		public LineSegment (Point point):base(point)
		{
		}

		protected override bool DoContains (Point point)
		{
			return Line.Contains (Point1, Point, point);
		}

		protected override Rect DoGetBounds ()
		{
			return Rect.FromPoints (Point1, Point);
		}

		protected override Shape DoHitTest (Point point)
		{
			return this;
		}
	}
}

