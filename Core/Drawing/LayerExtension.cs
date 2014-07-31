namespace Cession.Drawing
{
	using System.Drawing;
	using Cession.Geometries;
	using Cession.Modeling;

	public static class LayerExtension
	{
		public static readonly int LogicalUnitPerPixel = 250;

		public static Point2 ConvertToLogicalPoint(this Layer layer,PointF point)
		{
			var newPoint = point.ToPoint2 ();
			var matrix = layer.GetTransform ();
			matrix.Invert ();

			return matrix.Transform (newPoint);
		}

		public static PointF ConvertToViewPoint(this Layer layer,Point2 point)
		{
			var matrix = layer.GetTransform ();
			var newPoint = matrix.Transform (point);
			return newPoint.ToPointF ();
		}

		public static Matrix GetTransform(this Layer layer)
		{
			var matrix = layer.Transform;
			matrix.ScalePrepend ((double)1/LogicalUnitPerPixel, (double)1/LogicalUnitPerPixel);
			return matrix;
		}
	}
}

