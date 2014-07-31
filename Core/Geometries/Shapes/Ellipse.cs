using System;

namespace Cession.Geometries.Shapes
{
	public class Ellipse:Figure
	{
		private Rect rect;
		public Ellipse (Rect rect)
		{
			this.rect = rect;
		}

		public override double GetArea ()
		{
			return Math.PI * (rect.Width / 2) * (rect.Height / 2);
		}

		public override double GetPerimeter ()
		{
			if (rect.Width == rect.Height)
				return Math.PI * rect.Width;

			var a = rect.Width / 2;
			var b = rect.Height / 2;
			return 2 * Math.PI * Math.Sqrt ((a * a + b * b) / 2);
		}

		public override bool Contains (Point2 point)
		{
			return false;
		}

		public override void Offset (int x, int y)
		{
			rect.Offset (x, y);
		}
	}
}

