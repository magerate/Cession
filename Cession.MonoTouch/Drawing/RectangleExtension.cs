namespace Cession.Drawing
{
	using System;
	using System.Drawing;

	using Cession.Geometries;
	using Cession.Modeling;

	public static class RectangleExtension
	{
		public static RectangleF ToRect(this RectangleDiagram rect,Matrix transform)
		{
			var p1 = transform.Transform(rect.Rect.LeftTop).ToPointF();
			var p2 = transform.Transform(rect.Rect.RightBottom).ToPointF();

			return RectangleF.FromLTRB (p1.X, p1.Y, p2.X, p2.Y);
		}
	}
}

