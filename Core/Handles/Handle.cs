//namespace Cession.Handles
//{
//	using System;
//
//	using Cession.Geometries;
//	using Cession.Modeling;
//
//	public abstract class Handle
//	{
//		private Point2 location;
//		private Diagram diagram;
//
//		public Point2 Location{
//			get{ return location; }
//		}
//
//		public Diagram Diagram{
//			get{ return diagram; }
//		}
//
//		protected Handle (Point2 location,Diagram diagram)
//		{
//			this.location = location;
//			this.diagram = diagram;
//		}
//
//		protected Handle(Point2 location):this(location,null)
//		{
//		}
//
//		public abstract bool Contains(Point2 point);
//		public abstract Rect Bounds{ get; }
//	}
//}
//
