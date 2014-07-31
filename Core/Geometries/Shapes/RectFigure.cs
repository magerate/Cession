using System;

namespace Cession.Geometries.Shapes
{
	public class RectFigure:Figure
	{
		private Rect rect;

		public Rect Rectangle
		{
			get{ return rect; }
			set{ rect = value; }
		}

		public RectFigure (Rect rect)
		{
			this.rect = rect;
		}

		public override double GetArea ()
		{
			return rect.Width * rect.Height;
		}

		public override double GetPerimeter ()
		{
			return (rect.Width + rect.Height) * 2;
		}

		public override bool Contains (Point2 point)
		{
			return rect.Contains (point);
		}

		public override void Offset (int x, int y)
		{
			rect.Offset (x, y);
		}

		public static RectFigure Inflate(RectFigure rect,int width,int height)
		{
			var rf = rect.Rectangle;
			rf.Inflate (width, height);

			return new RectFigure (rf);
		}
	}
}

