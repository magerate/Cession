namespace Cession.Geometries
{
    using System;
    using System.Collections.Generic;

	public struct RectD : IEquatable<RectD>
    {
        private double x;
        private double y;
        private double width;
        private double height;

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public double Left
        {
            get { return x; }
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public double Bottom
        {
            get { return y; }
        }

        public double Width
        {
            get { return width; }
            set { width = value; }
        }

        public double Right
        {
            get { return x + width; }
        }

        public double Height
        {
            get { return height; }
            set { height = value; }
        }

        public double Top
        {
            get { return y + height; }
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

        public RectD(double x, double y, double width, double height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public bool Contains(Point2 point)
        {
            return point.X >= x && point.X <= x + width &&
                point.Y >= y && point.Y <= y + height;
        }

        public bool Contains(RectD rect)
        {
            return Left <= rect.Left && Right >= rect.Right && Bottom <= rect.Bottom && Top >= rect.Top;
        }

        public bool Equals(RectD rect)
        {
            return this.x == rect.x && this.y == rect.y &&
                this.width == rect.width && this.height == rect.height;
        }

        public override bool Equals(object obj)
        {
            if (null == obj || !(obj is RectD))
                return false;
            return Equals((RectD)obj);
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() ^ width.GetHashCode() ^ height.GetHashCode();
        }

        public static bool operator ==(RectD left, RectD right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RectD left, RectD right)
        {
            return !left.Equals(right);
        }

        public void Offset(double x, double y)
        {
            this.x += x;
            this.y += y;
        }

        public void Offset(Vector vector)
        {
            this.Offset(vector.X, vector.Y);
        }

        public void Inflate(double width, double height)
        {
            this.x -= width;
            this.y -= height;
            this.width += 2 * width;
            this.height += 2 * height;
        }

        public void Scale(double scaleX, double scaleY)
        {
            x *= scaleX;
            y *= scaleY;
            width *= scaleX;
            height *= scaleY;
        }

        public RectD Union(RectD rect)
        {
            var left = Math.Min(this.Left, rect.Left);
            var bottom = Math.Min(this.Bottom, rect.Bottom);

            var right = Math.Max(this.Right, rect.Right);
            var top = Math.Max(this.Top, rect.Top);

            return new RectD(left, bottom, right - left, top - bottom);
        }

        public RectD? Intersect(RectD rect)
        {
            if (Range.Contains(this.Left, this.Right, rect.Left) ||
                Range.Contains(this.Left, this.Right, rect.Right))
            {
                var left = Math.Max(this.Left, rect.Left);
                var right = Math.Min(this.Right, rect.Right);
                var top = Math.Min(this.Top, rect.Top);
                var bottom = Math.Max(this.Bottom, rect.Bottom);

                return new RectD(left, bottom, right - left, top - bottom);
            }
            return null;
        }

        public List<Point2> CrossBetweenLine(Point2 p1, Point2 p2)
        {
            var points = new List<Point2>();
            CheckCross(points, this.LeftBottom, this.LeftTop, p1, p2);
            CheckCross(points, this.LeftTop, this.RightTop, p1, p2);
            if (points.Count == 2)
                return points;
            CheckCross(points, this.RightTop, this.RightBottom, p1, p2);
            if (points.Count == 2)
                return points;
            CheckCross(points, this.RightBottom, this.LeftBottom, p1, p2);
            return points;
        }

        private void CheckCross(List<Point2> points, Point2 p1, Point2 p2, Point2 p3, Point2 p4)
        {
            var cross = Segment.CrossBetweenLine(p1, p2, p3, p4);
            if (cross.HasValue)
                points.Add(cross.Value);
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", x.ToString(), y.ToString(), width.ToString(), height.ToString());
        }


		public static RectD FromLTRB(double left,double top,double right,double bottom)
		{
			return new RectD (left, top, right - left, bottom - top);
		}


		public static RectD From(Point2 p1,Point2 p2)
		{
			return FromLTRB (Math.Min (p1.X, p2.X), 
				Math.Min (p1.Y, p2.Y),
				Math.Max (p1.X, p2.X),
				Math.Max (p1.Y, p2.Y));
		}
    }
}
