namespace Cession.Modeling
{
	using System;

	using Cession.Geometries;

	public class SideHandle:Handle
	{
		public int Size{ get; private set; }
		public int Index{ get; private set; }

		private ClosedShapeDiagram Path{
			get{ return Parent as ClosedShapeDiagram; }
		}

		public Segment Side
		{ 
			get{ return Path[Index]; }
		}

		public Segment PreviousSide{
			get{
				var count = GetSideCount();

				var index = (Index - 1 + count) % count;
				return Path [index];
			}
		}

		public Segment NextSide{
			get{
				var count = GetSideCount();

				var index = (Index + 1) % count;
				return Path [index];
			}
		}


		public SideHandle (Point2 location,int index,ClosedShapeDiagram diagram):base(location,diagram)
		{
			Size = 32;
			this.Index = index;
		}

		private int GetSideCount(){
			if (Parent is RectangleDiagram)
				return 4;
			return (Parent as PathDiagram).Points.Count;
		}

		public override Diagram HitTest (Point2 point)
		{
			if (
				point.X >= Location.X - Size / 2  &&
				point.X <= Location.X + Size / 2  &&
				point.Y >= Location.Y - Size / 2  &&
				point.Y <= Location.Y + Size / 2)
				return this;
			return null;
		}

		public Segment Move(Point2 point){
			return MoveRectSide (point);
		}

		private Segment MoveRectSide(Point2 point){
			if (Side.P1.X == Side.P2.X) {
				return new Segment (new Point2 (point.X, Side.P1.Y),
					new Point2 (point.X, Side.P2.Y));
			}
			return new Segment (new Point2 (Side.P1.X, point.Y),
				new Point2 (Side.P2.X, point.Y));
		}
	}
}

