namespace Cession.Geometries
{
	using System;
	using System.Collections.Generic;

	public static class Polygon
	{
		public static bool Contains(Point2 point, IList<Point2> polygon)
		{
			return ContainsPoint (point, polygon) != 0;
		}

		public static int ContainsPoint(Point2 point, IList<Point2> polygon)
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

