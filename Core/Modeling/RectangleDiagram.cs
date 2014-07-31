namespace Cession.Modeling
{
	using Cession.Geometries;

	public class RectangleDiagram:ClosedShapeDiagram
	{
		private Rect rect;

		public Rect Rect
		{ 
			get{ return rect; }
			set{ rect = value; }
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

		public static RectangleDiagram Inflate(RectangleDiagram rect,int width,int height)
		{
			var rf = rect.rect;
			rf.Inflate (width, height);

			return new RectangleDiagram (rf);
		}
	}
}

