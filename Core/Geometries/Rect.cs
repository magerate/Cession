namespace Cession.Geometries
{
	using System;
	using System.Collections.Generic;

	public struct Rect : IEquatable<Rect>
	{
		public static readonly Rect Empty = new Rect (0, 0, 0, 0);
		private int x;
		private int y;
		private int width;
		private int height;

		public int X
		{
			get { return x; }
			set { x = value; }
		}

		public int Left
		{
			get { return x; }
		}

		public int Y
		{
			get { return y; }
			set { y = value; }
		}

		public int Bottom
		{
			get { return y + height; }
		}

		public int Width
		{
			get { return width; }
			set { width = value; }
		}

		public int Right
		{
			get { return x + width; }
		}

		public int Height
		{
			get { return height; }
			set { height = value; }
		}

		public int Top
		{
			get { return y; }
		}

		public Point2 LeftBottom
		{
			get { return new Point2(this.Left, this.Bottom); }
		}

		public Point2 RightBottom
		{
			get { return new Point2(this.Right, this.Bottom); }
		}

		public Point2 LeftTop
		{
			get { return new Point2(this.Left, this.Top); }
		}

		public Point2 RightTop
		{
			get { return new Point2(this.Right, this.Top); }
		}

		public Rect(int x, int y, int width, int height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		public Rect(Point2 location,Size2 size):this(location.X,location.Y,size.Width,size.Height)
		{
		}

		public bool Contains(Point2 point)
		{
			return point.X >= x && point.X <= x + width &&
				point.Y >= y && point.Y <= y + height;
		}

		public bool Contains(Rect rect)
		{
			return Left <= rect.Left && Right >= rect.Right && Bottom <= rect.Bottom && Top >= rect.Top;
		}

		public bool Equals(Rect rect)
		{
			return this.x == rect.x && this.y == rect.y &&
				this.width == rect.width && this.height == rect.height;
		}

		public override bool Equals(object obj)
		{
			if (null == obj || !(obj is Rect))
				return false;
			return Equals((Rect)obj);
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ y.GetHashCode() ^ width.GetHashCode() ^ height.GetHashCode();
		}

		public static bool operator ==(Rect left, Rect right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Rect left, Rect right)
		{
			return !left.Equals(right);
		}

		public void Offset(int x, int y)
		{
			this.x += x;
			this.y += y;
		}

		public void Offset(Vector vector)
		{
			this.Offset((int)vector.X, (int)vector.Y);
		}

		public void Inflate(int width, int height)
		{
			this.x -= width;
			this.y -= height;
			this.width += 2 * width;
			this.height += 2 * height;

		}

		public void Scale(int scaleX, int scaleY)
		{
			x *= scaleX;
			y *= scaleY;
			width *= scaleX;
			height *= scaleY;
		}

		public Rect Union(Rect rect)
		{
			if (this == Empty)
				return rect;
			if (rect == Empty)
				return this;

			var left = Math.Min(this.Left, rect.Left);
			var top = Math.Min(this.Top, rect.Top);

			var right = Math.Max(this.Right, rect.Right);
			var bottom = Math.Max(this.Bottom, rect.Bottom);

			return Rect.FromLTRB (left, top, right, bottom);
		}

		public Rect? Intersect(Rect rect)
		{
			if (Range.Contains(this.Left, this.Right, rect.Left) ||
				Range.Contains(this.Left, this.Right, rect.Right))
			{
				var left = Math.Max(this.Left, rect.Left);
				var right = Math.Min(this.Right, rect.Right);
				var top = Math.Max(this.Top, rect.Top);
				var bottom = Math.Min(this.Bottom, rect.Bottom);

				return Rect.FromLTRB (left, top, right, bottom);
			}
			return null;
		}

//		public List<Point2d> CrossBetweenLine(Point2d p1, Point2d p2)
//		{
//			var points = new List<Point2d>();
//			CheckCross(points, this.LeftBottom, this.LeftTop, p1, p2);
//			CheckCross(points, this.LeftTop, this.RightTop, p1, p2);
//			if (points.Count == 2)
//				return points;
//			CheckCross(points, this.RightTop, this.RightBottom, p1, p2);
//			if (points.Count == 2)
//				return points;
//			CheckCross(points, this.RightBottom, this.LeftBottom, p1, p2);
//			return points;
//		}
//
//		private void CheckCross(List<Point2d> points, Point2d p1, Point2d p2, Point2d p3, Point2d p4)
//		{
//			var cross = Segment.CrossBetweenLine(p1, p2, p3, p4);
//			if (cross.HasValue)
//				points.Add(cross.Value);
//		}

		public override string ToString()
		{
			return string.Format("{0},{1},{2},{3}", x.ToString(), y.ToString(), width.ToString(), height.ToString());
		}


		public static Rect FromLTRB(int left,int top,int right,int bottom)
		{
			return new Rect (left, top, right - left, bottom - top);
		}


		public static Rect FromPoints(Point2 p1,Point2 p2)
		{
			return FromLTRB (Math.Min (p1.X, p2.X), 
				Math.Min (p1.Y, p2.Y),
				Math.Max (p1.X, p2.X),
				Math.Max (p1.Y, p2.Y));
		}
	}
}
