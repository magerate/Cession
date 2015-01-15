namespace Cession.Modeling
{
	using System.Collections.Generic;
	using System.Linq;

	using ClipperLib;

	using Cession.Geometries;

	public static class PolygonHelper
	{
		private static ClipperOffset offseter = new ClipperOffset();
		private static Clipper clipper = new Clipper ();

		public static List<IntPoint> ToIntPolygon(this PathDiagram polygon)
		{
			return polygon.Points.Select (p => new IntPoint (p.X, p.Y)).ToList ();
		}

		private static List<IntPoint> ToIntPolygon(this IEnumerable<Point2> polygon){
			return polygon.Select (p => new IntPoint (p.X, p.Y)).ToList ();
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

		public static IEnumerable<Point2> Union(IEnumerable<IEnumerable<Point2>> polygons){
			clipper.Clear ();
			foreach (var p in polygons) {
				clipper.AddPath (p.ToIntPolygon (), PolyType.ptClip, true);
			}
			var solution = new List<List<IntPoint>> ();
			clipper.Execute (ClipType.ctUnion,solution);
			var points = solution [0].Select (p => new Point2 (p.X, p.Y));
			return points;
		}
	}
}

