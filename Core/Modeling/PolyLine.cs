namespace Cession.Modeling
{
	using System;
	using System.Linq;
	using System.Collections.Generic;

	using Cession.Geometries;

	public class PolyLine:Diagram
	{
		private List<Point2> points;

		public PolyLine (IEnumerable<Point2> points):this(points,null)
		{
		}


		public PolyLine (IEnumerable<Point2> points,Diagram parent)
		{
			this.points = points.ToList ();
			this.Parent = parent;
		}

		internal override void InternalOffset (int x, int y)
		{
			for (int i = 0; i < points.Count; i++) {
				points [i] = new Point2 (points [i].X + x, points [i].Y + y);
			}
		}

		public override Diagram HitTest (Point2 point)
		{
			return null;
		}
	}
}

