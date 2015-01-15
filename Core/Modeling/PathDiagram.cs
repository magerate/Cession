namespace Cession.Modeling
{
	using System;
	using System.Linq;
	using System.Collections.Generic;

	using Cession.Geometries;

	public class PathDiagram:ClosedShapeDiagram,IPolygonal
	{
		private List<Point2> points;

		public PathDiagram (IEnumerable<Point2> points):this(points,null)
		{
		}


		public PathDiagram (IEnumerable<Point2> points,Diagram parent)
		{
			this.points = points.ToList ();
			if (!Polygon.IsClockwise (this.points))
				this.points.Reverse ();

			this.Parent = parent;
		}

		public IList<Point2> Points
		{
			get{ return points; }
		}

		#region IPolygonal implementation

		public int SideCount{
			get{ return points.Count; }
		}

		public Segment this[int index] {
			get {
				if (index < 0 || index >= SideCount)
					throw new ArgumentOutOfRangeException ();

				var p2 = points [(index + 1) % SideCount];
				return new Segment (points [index], p2);
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

			var distance = Line.DistanceBetween (side.P1, side.P2, point);
			var vector = side.P2 - side.P1;
			vector.Normalize ();
			vector *= distance;
			vector.Rotate (-Math.PI / 2);

			var prevSide = GetPreviousSide (index).Value;
			var nextSide = GetNextSide (index).Value;

			var op1 = side.P1 + vector;
			var op2 = side.P2 + vector;
			var ip1 = Line.Intersect (prevSide.P1, prevSide.P2, op1, op2);
			var ip2 = Line.Intersect (nextSide.P1, nextSide.P2, op1, op2);
			return new Segment (ip1.Value, ip2.Value);
		}

		public void MoveSide(int index,Segment segment){
			var nextIndex = (index + 1 + SideCount) % SideCount;
			points [index] = segment.P1;
			points [nextIndex] = segment.P2;

			RaiseEvent(new RoutedEventArgs(Diagram.ShapeChangeEvent,this));
		}

		#endregion

		public override double GetArea ()
		{
			return 0;
		}

		public override double GetPerimeter ()
		{
			return 0;
		}

		internal override void InternalOffset (int x, int y)
		{
			for (int i = 0; i < points.Count; i++) {
				points [i] = new Point2 (points [i].X + x, points [i].Y + y);
			}
		}

		public override Rect Bounds {
			get {
				int left = int.MaxValue;
				int top = int.MaxValue;
				int right = int.MinValue;
				int bottom = int.MinValue;

				foreach (var p in points) {
					left = Math.Min (p.X, left);
					right = Math.Max (p.X, right);

					top = Math.Min (p.Y, top);
					bottom = Math.Max (p.Y, bottom);
				}
				return Rect.FromLTRB (left, top, right, bottom);
			}
		}

		public override Diagram HitTest (Point2 point)
		{
			if (Polygon.Contains (point, points))
				return this;

			return null;
		}


	}
}

