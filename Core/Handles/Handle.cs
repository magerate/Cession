namespace Cession.Modeling
{
	using System;

	using Cession.Geometries;

	public abstract class Handle:Diagram
	{
		private Point2 location;

		public Point2 Location{
			get{ return location; }
		}

		protected Handle (Point2 location,Diagram parent):base(parent)
		{
			this.location = location;
		}

		protected Handle(Point2 location):this(location,null)
		{
		}

		public override void Offset (int x, int y)
		{
			throw new NotImplementedException ();
		}
	}
}

