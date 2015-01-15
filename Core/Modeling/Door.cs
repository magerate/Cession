namespace Cession.Modeling
{
	using System;
	using System.Collections.Generic;

	using Cession.Geometries;

	public class Door:Diagram
	{
		public static int DefaultWidth = 800;
		public static int DefaultHeight = 1000;

		private int index;

		private double distance;

		public double Width{ get; set; }
		public double Height{ get; set; }

		private IPolygonal Polygon{
			get{ return (Parent as Room).Contour as IPolygonal; }
		}

		private Segment Side{
			get{ return Polygon [index]; }
		}

		public Door (Room room,int index,double distance):base(room)
		{
			if (!(room.Contour is IPolygonal))
				throw new ArgumentException ("room");

			Width = DefaultWidth;
			Height = DefaultHeight;
			this.distance = distance;
			this.index = index;
		}

		public Rect OriginalBounds{
			get{ return new Rect (0, 0, (int)Width, (int)Room.DefaultWallThickness); }
		}

		public Matrix Transform{
			get{
				var matrix = Matrix.Identity;
				var vector = Side.P2 - Side.P1;
				vector = vector * distance / vector.Length;
				var point = Side.P1 + vector;
				matrix.Translate ((double)point.X, (double)point.Y - this.OriginalBounds.Height);
				var degree = (vector.Angle / Math.PI * 180);
				matrix.RotateAt (degree, point.X, point.Y);

				return matrix;
			}
		}

		public override void Offset (int x, int y)
		{
			throw new InvalidOperationException ();
		}

		internal override void InternalOffset (int x, int y)
		{
		}

		public override Rect Bounds {
			get {
				return OriginalBounds;
			}
		}

		public override Diagram HitTest (Point2 point)
		{
			var matrix = Transform;
			matrix.Invert ();
			point = matrix.Transform (point);
			if (OriginalBounds.Contains (point))
				return this;
			return null;
		}

		public bool IsValid{
			get{ return distance + Width < Side.Length; }
		}
	}
}

