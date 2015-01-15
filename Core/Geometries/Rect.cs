using System;

namespace Cession.Geometries
{
	public struct Rect : IEquatable<Rect>
	{
		public static readonly Rect Empty = new Rect (0, 0, 0, 0);
		private int _x;
		private int _y;
		private int _width;
		private int _height;

		public int X
		{
			get { return _x; }
			set { _x = value; }
		}

		public int Left
		{
			get { return _x; }
		}

		public int Y
		{
			get { return _y; }
			set { _y = value; }
		}

		public int Bottom
		{
			get { return _y + _height; }
		}

		public int Width
		{
			get { return _width; }
			set { _width = value; }
		}

		public int Right
		{
			get { return _x + _width; }
		}

		public int Height
		{
			get { return _height; }
			set { _height = value; }
		}

		public int Top
		{
			get { return _y; }
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
			_x = x;
			_y = y;
			_width = width;
			_height = height;
		}

		public Rect(Point2 location,Size2 size):this(location.X,location.Y,size.Width,size.Height)
		{
		}

		public bool Contains(Point2 point)
		{
			return point.X >= _x && point.X <= _x + _width &&
				point.Y >= _y && point.Y <= _y + _height;
		}

		public bool Contains(Rect rect)
		{
			return Left <= rect.Left && Right >= rect.Right && Bottom <= rect.Bottom && Top >= rect.Top;
		}

		public bool Equals(Rect rect)
		{
			return this._x == rect._x && this._y == rect._y &&
				this._width == rect._width && this._height == rect._height;
		}

		public override bool Equals(object obj)
		{
			if (null == obj || !(obj is Rect))
				return false;
			return Equals((Rect)obj);
		}

		public override int GetHashCode()
		{
			return _x.GetHashCode() ^ _y.GetHashCode() ^ _width.GetHashCode() ^ _height.GetHashCode();
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
			this._x += x;
			this._y += y;
		}

		public void Offset(Vector vector)
		{
			this.Offset((int)vector.X, (int)vector.Y);
		}

		public void Inflate(int width, int height)
		{
			this._x -= width;
			this._y -= height;
			this._width += 2 * width;
			this._height += 2 * height;

		}

//		public void Scale(int scaleX, int scaleY)
//		{
//			_x *= scaleX;
//			_y *= scaleY;
//			_width *= scaleX;
//			_height *= scaleY;
//		}

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
			return string.Format("{0},{1},{2},{3}", _x.ToString(), _y.ToString(), _width.ToString(), _height.ToString());
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
