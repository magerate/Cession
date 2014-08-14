namespace Cession.Modeling
{
	using System;
	using System.Linq;
	using System.Collections.Generic;

	using Cession.Geometries;

	public class PathDiagram:ClosedShapeDiagram
	{
		private List<Point2> points;

		public PathDiagram (IEnumerable<Point2> points):this(points,null)
		{
		}


		public PathDiagram (IEnumerable<Point2> points,Diagram parent)
		{
			this.points = points.ToList ();
			this.Parent = parent;
		}

		public IList<Point2> Points
		{
			get{ return points; }
		}

		public override Segment this[int index] {
			get {
				if (index < 0 || index >= points.Count)
					throw new ArgumentOutOfRangeException ();

				var p2 = (index + 1 == points.Count) ? points [0] : points [index + 1];
				return new Segment (points [index], p2);
			}
		}

		public override double GetArea ()
		{
			return 0;
		}

		public override double GetPerimeter ()
		{
			return 0;
		}

		public override void Offset (int x, int y)
		{
			for (int i = 0; i < points.Count; i++) {
				points [i] = new Point2 (points [i].X + x, points [i].Y + y);
			}
		}

		public override Diagram HitTest (Point2 point)
		{
			if (PathDiagram.Contains (point, points) != 0)
				return this;
			return null;
		}

		public static int Contains(Point2 point, IList<Point2> polygon)
		{
			//returns 0 if false, +1 if true, -1 if pt ON polygon boundary
			//http://citeseerx.ist.psu.edu/viewdoc/download?doi=10.1.1.88.5498&rep=rep1&type=pdf
			int result = 0, cnt = polygon.Count;
			if (cnt < 3) return 0;
			var ip = polygon[0];
			for (int i = 1; i <= cnt; ++i)
			{
				var ipNext = (i == cnt ? polygon[0] : polygon[i]);
				if (ipNext.Y == point.Y)
				{
					if ((ipNext.X == point.X) || (ip.Y == point.Y &&
						((ipNext.X > point.X) == (ip.X < point.X)))) return -1;
				}
				if ((ip.Y < point.Y) != (ipNext.Y < point.Y))
				{
					if (ip.X >= point.X)
					{
						if (ipNext.X > point.X) result = 1 - result;
						else
						{
							double d = (double)(ip.X - point.X) * (ipNext.Y - point.Y) -
								(double)(ipNext.X - point.X) * (ip.Y - point.Y);
							if (d == 0) return -1;
							else if ((d > 0) == (ipNext.Y > ip.Y)) result = 1 - result;
						}
					}
					else
					{
						if (ipNext.X > point.X)
						{
							double d = (double)(ip.X - point.X) * (ipNext.Y - point.Y) -
								(double)(ipNext.X - point.X) * (ip.Y - point.Y);
							if (d == 0) return -1;
							else if ((d > 0) == (ipNext.Y > ip.Y)) result = 1 - result;
						}
					}
				}
				ip = ipNext;
			}
			return result;
		}
	}
}

