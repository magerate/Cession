using System;

namespace Cession.Geometries
{
	public struct Point2:IEquatable<Point2>
	{
		public static readonly Point2 Empty = new Point2(0, 0);

		public int X, Y;

		public Point2(int x,int y)
		{
			X = x;
			Y = y;
		}

		public bool Equals(Point2 p)
		{
			return X == p.X && Y == p.Y;
		}

		public override bool Equals(object obj)
		{
			if (null == obj || !(obj is Point2))
				return false;
			return this.Equals((Point2)obj);
		}

		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("({0},{1})", X.ToString(), Y.ToString());
		}

		public static bool operator ==(Point2 p1,Point2 p2)
		{
			return p1.Equals(p2);
		}

		public static bool operator !=(Point2 p1,Point2 p2)
		{
			return !p1.Equals(p2);
		}

		public static Vector operator -(Point2 p1,Point2 p2)
		{
			return new Vector(p1.X - p2.X, p1.Y - p2.Y);
		}

		public static Point2 operator -(Point2 p,Vector v)
		{
			return new Point2((int)(p.X - v.X), (int)(p.Y - v.Y));
		}

		public static Point2 operator +(Point2 p,Vector v)
		{
			return new Point2((int)(p.X + v.X), (int)(p.Y + v.Y));
		}

		public void Offset(int x,int y)
		{
			this.X += x;
			this.Y += y;
		}

		public static double DistanceBetween(Point2 p1,Point2 p2)
		{
			return (p1 - p2).Length;
		}

		public double DistanceBetween(Point2 point)
		{
			return Point2.DistanceBetween(this, point);
		}
	}
}
