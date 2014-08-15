namespace Cession.Modeling
{
	using System;

	using Cession.Geometries;

	public class SideHandle:Handle
	{
		public int Size{ get; private set; }
		public int Index{ get; private set; }

		public IPolygonal Polygon{
			get{ return Parent as IPolygonal; }
		}
		public SideHandle (Point2 location,int index,ClosedShapeDiagram diagram):base(location,diagram)
		{
			if (!(diagram is IPolygonal))
				throw new ArgumentException ();
			Size = 32;
			this.Index = index;
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

		public Segment MoveSide(Point2 point){
			return Polygon.MoveSide (Index, point);
		}

		public Segment? PreviousSide{
			get{ return Polygon.GetPreviousSide (Index); }
		}

		public Segment? NextSide{
			get{ return Polygon.GetNextSide (Index); }
		}
	}
}

