namespace Cession.Drawing
{
	using System.Drawing;
	using Cession.Geometries;

	public static class PointExtension
	{

		public static Point2 ToPoint2(this PointF point)
		{
			return new Point2 ((int)point.X, (int)point.Y);
		}

		public static PointF ToPointF(this Point2 point)
		{
			return new PointF ((float)point.X, (float)point.Y);
		}
	}
}

