namespace Cession.Modeling
{
	using System;
	using Cession.Geometries;

	public interface IPolygonal
	{
		int SideCount{ get; }
		Segment this [int index]{ get; }

		Segment? GetPreviousSide (int index);
		Segment? GetNextSide (int index);

		Segment MoveSide (int index, Point2 point);
	}

	public class RectangleDiagram:ClosedShapeDiagram,IPolygonal
	{
		private Rect rect;

		public Rect Rect
		{ 
			get{ return rect; }
			set
			{ 
				if(value != rect){
					rect = value; 
					RaiseEvent (new RoutedEventArgs (Diagram.ShapeChangeEvent, this));
				}
			}
		}

		#region polygonal implementation

		public int SideCount{
			get{ return 4; }
		}

		public Segment this[int index] {
			get {
				if (index < 0 || index >= SideCount)
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

		public Segment? GetPreviousSide(int index){
			if (index < 0 || index >= SideCount)
				throw new ArgumentOutOfRangeException ();

			return this [(index - 1 + SideCount) % SideCount];
		}

		public Segment? GetNextSide(int index){
			if (index < 0 || index >= SideCount)
				throw new ArgumentOutOfRangeException ();

			return this [(index + 1)  % SideCount];
		}

		public Segment MoveSide(int index,Point2 point){
			if (index < 0 || index >= SideCount)
				throw new ArgumentOutOfRangeException ();

			var side = this [index];
			if (side.P1.X == side.P2.X) {
				return new Segment (new Point2 (point.X, side.P1.Y),
					new Point2 (point.X, side.P2.Y));
			}
			return new Segment (new Point2 (side.P1.X, point.Y),
				new Point2 (side.P2.X, point.Y));
		}

		#endregion

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
				if (side.P1.X < nextSide.P1.X) {
					newRect.X += offsetX;
					newRect.Width -= offsetX;
				} else {
					newRect.Width += offsetX;
				}
			}else {
				if (side.P1.Y < nextSide.P1.Y) {
					newRect.Y += offsetY;
					newRect.Height -= offsetY;
				} else {
					newRect.Height += offsetY;
				}
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

