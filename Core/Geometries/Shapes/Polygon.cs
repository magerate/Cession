using System;
using System.Linq;
using System.Collections.Generic;

namespace Cession.Geometries.Shapes
{
	public class Polygon:Figure
	{
		private List<Point2> points;

		public Polygon (IEnumerable<Point2> points)
		{
			this.points = points.ToList ();
		}

		public override double GetArea ()
		{
			return 0;
		}

		public override double GetPerimeter ()
		{
			return 0;
		}

		public IList<Point2> Points
		{
			get{ return points; }
		}

		public override bool Contains (Point2 point)
		{
			return Contains (point, points) != 0;
		}

		public override void Offset (int x, int y)
		{
			for (int i = 0; i < points.Count; i++) {
				points [i] = new Point2 (points [i].X + x, points [i].Y + y);
			}
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

