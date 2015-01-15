namespace Cession.Modeling
{
	using System;
	using Cession.Geometries;

	public class CircleDiagram:ClosedShapeDiagram
	{
		private Point2 center;
		private int radius;

		public Point2 Center
		{ 
			get{ return center; }
			set{ center = value; }
		}


		public int Radius
		{ 
			get{ return radius; }
			set{ radius = value; }
		}

		public CircleDiagram (Point2 center,int radius):this(center,radius,null)
		{
		}

		public CircleDiagram (Point2 center,int radius,Diagram parent)
		{
			this.center = center;
			this.radius = radius;
			this.Parent = parent;
		}


		public override Diagram HitTest (Point2 point)
		{
			if (point.DistanceBetween (Center) <= Radius)
				return this;
			return null;
		}

		public override Rect Bounds {
			get {
				return new Rect (center.X - radius, center.Y - radius, 2 * radius, 2 * radius);
			}
		}

		internal override void InternalOffset (int x, int y)
		{
			center.Offset (x, y);
		}

		public override double GetArea ()
		{
			return Math.PI * radius * radius;
		}

		public override double GetPerimeter ()
		{
			return 2 * Math.PI * radius;
		}
	}
}

