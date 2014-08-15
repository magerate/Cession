namespace Cession.Modeling
{
	using System.Collections.Generic;
	using System.Linq;

	using ClipperLib;

	using Cession.Geometries;

	public static class PolygonHelper
	{
		private static ClipperOffset offseter = new ClipperOffset();


//		public static List<IntPoint> ToIntPolygon(this Polygon polygon)
//		{
//			return polygon.Points.Select (p => new IntPoint (p.X, p.Y)).ToList ();
//		}
//
//		public static Polygon OffsetPolygon(this Polygon polygon,double delta)
//		{
//			offseter.Clear ();
//			offseter.AddPath (polygon.ToIntPolygon (),JoinType.jtMiter,EndType.etClosedLine);
//
//			var solution = new List<List<IntPoint>> ();
//			offseter.Execute (ref solution, delta);
//
//			var points = solution [0].Select (p => new Point2 (p.X, p.Y));
//			return new Polygon (points);
//		}

		public static List<IntPoint> ToIntPolygon(this PathDiagram polygon)
		{
			return polygon.Points.Select (p => new IntPoint (p.X, p.Y)).ToList ();
		}

		public static PathDiagram OffsetPolygon(this PathDiagram polygon,double delta)
		{
			offseter.Clear ();
			offseter.AddPath (polygon.ToIntPolygon (),JoinType.jtMiter,EndType.etClosedLine);

			var solution = new List<List<IntPoint>> ();
			offseter.Execute (ref solution, delta);

			var points = solution [0].Select (p => new Point2 (p.X, p.Y));
			return new PathDiagram (points);
		}
	}
}

