//namespace Cession.Handles
//{
//	using System;
//
//	using Cession.Geometries;
//	using Cession.Modeling;
//
//	public class SideHandle:Handle
//	{
//		public int Size{ get; private set; }
//		public int Index{ get; private set; }
//
//		public IPolygonal Polygon{
//			get{ return Diagram as IPolygonal; }
//		}
//
//		public Segment Side{
//			get{ return Polygon [Index]; }
//		}
//
//		public SideHandle (Point2 location,int index,ClosedShapeDiagram diagram):base(location,diagram)
//		{
//			if (!(diagram is IPolygonal))
//				throw new ArgumentException ();
//			Size = 32;
//			this.Index = index;
//		}
//
//		public override Rect Bounds {
//			get {
//				return new Rect(Location.X - Size / 2,
//					Location.Y - Size / 2,
//					Size,
//					Size);
//			}
//		}
//
//		public override bool Contains (Point2 point)
//		{
//			return Bounds.Contains (point);
//		}
//
//		public Segment MoveSide(Point2 point){
//			return Polygon.MoveSide (Index, point);
//		}
//
//		public Segment? PreviousSide{
//			get{ return Polygon.GetPreviousSide (Index); }
//		}
//
//		public Segment? NextSide{
//			get{ return Polygon.GetNextSide (Index); }
//		}
//	}
//}
//
