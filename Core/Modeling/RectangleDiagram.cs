namespace Cession.Modeling
{
	using System;
	using Cession.Geometries;

	public class RectangleDiagram:ClosedShapeDiagram
	{
		private Rect rect;

		public Rect Rect
		{ 
			get{ return rect; }
			set{ rect = value; }
		}

		public override Segment this[int index] {
			get {
				if (index < 0 || index > 3)
					throw new ArgumentOutOfRangeException ();

				if (index == 0)
					return new Segment (rect.LeftTop, rect.RightTop);
				if (index == 1)
					return new Segment (rect.RightTop, rect.RightBottom);
				if (index == 2)
					return new Segment (rect.RightBottom, rect.LeftBottom);

				return new Segment (rect.LeftBottom, rect.LeftTop);
			}
		}

		public RectangleDiagram (Rect rect):this(rect,null)
		{
		}

		public RectangleDiagram(Rect rect,Diagram parent)
		{
			this.rect = rect;
			this.Parent = parent;
		}


		public override Diagram HitTest (Point2 point)
		{
			if (Rect.Contains (point))
				return this;
			return null;
		}

		public override void Offset (int x, int y)
		{
			rect.Offset (x, y);
		}

		public override double GetArea ()
		{
			return rect.Width * rect.Height;
		}

		public override double GetPerimeter ()
		{
			return (rect.Width + rect.Height) * 2;
		}

		public Rect MoveSide(int index,int offsetX,int offsetY){

			var newRect = rect;
			var sideCount = 4;
			var side = this [index];
			var nextSide = this [(index + 2 + sideCount) % sideCount];
			if (side.P1.X == side.P2.X) {
				newRect.Width += offsetX;

				if (side.P1.X < nextSide.P1.X)
					newRect.X += offsetX;

			}else {
				newRect.Height += offsetY;
				if (side.P1.Y < nextSide.P1.Y)
					newRect.Y += offsetY;
			}

			return newRect;
		}

		public static RectangleDiagram Inflate(RectangleDiagram rect,int width,int height)
		{
			var rf = rect.rect;
			rf.Inflate (width, height);

			return new RectangleDiagram (rf);
		}
	}
}

