using System;

using Cession.Geometries;

namespace GeometryTest
{
	public static class LineHelper
	{
		const double Epsilon = 1e-5;

		public static bool AlmostContains(Point p1,Point p2,Point point){
			if (p1 == p2)
				return false;

			var v1 = Vector.Normalize(p1 - point);
			var v2 = Vector.Normalize(point - p2);

			return Math.Abs(Vector.CrossProduct(v1, v2)) <= Epsilon;
		}
	}
}

