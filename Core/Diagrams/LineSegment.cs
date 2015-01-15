using Cession.Geometries;

namespace Cession.Diagrams
{
	public class LineSegment:Segment
	{
		public Point2 Point2{
			get{ return Host.GetNextPoint (this); }
		}

		public LineSegment (Point2 point):base(point)
		{
		}

		protected override bool DoContains (Point2 point)
		{
			return Line.AlmostContains (Point1, Point2, point);
		}

		protected override Rect DoGetBounds ()
		{
			return Rect.FromPoints (Point1, Point2);
		}

		protected override Shape DoHitTest (Point2 point)
		{
			return this;
		}
	}
}

