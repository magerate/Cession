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

        public Point Location
        {
            get{ return new Point (_x, _y); }
        }

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
            set 
            { 
                if (value < 0)
                    throw new ArgumentOutOfRangeException ("value");
                _width = value; 
            }
        }

        public Size Size
        {
            get{ return new Size (_width, _height); }
        }

        public int Right
        {
            get { return _x + _width; }
        }

        public int Height
        {
            get { return _height; }
            set 
            { 
                if (value < 0)
                    throw new ArgumentOutOfRangeException ("value");
                _height = value; 
            }
        }

        public int Top
        {
            get { return _y; }
        }


        public Point LeftBottom
        {
            get { return new Point (Left, Bottom); }
        }

        public Point RightBottom
        {
            get { return new Point (Right, Bottom); }
        }

        public Point LeftTop
        {
            get { return new Point (Left, Top); }
        }

        public Point RightTop
        {
            get { return new Point (Right, Top); }
        }

        public Rect (int x, int y, int width, int height)
        {
            if (width < 0)
                throw new ArgumentOutOfRangeException ("width");
            if (height < 0)
                throw new ArgumentOutOfRangeException ("height");

            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }

        public Rect (Point location, Size size) : this (location.X, location.Y, size.Width, size.Height)
        {
        }

        public bool Contains (Point point)
        {
            return point.X >= _x && point.X <= _x + _width &&
                point.Y >= _y && point.Y <= _y + _height;
        }

        public bool Contains (Rect rect)
        {
            return rect.Left >= Left && rect.Right <= Right && rect.Top >= Top && rect.Bottom <= Bottom;
        }

        public bool Equals (Rect rect)
        {
            return _x == rect._x && _y == rect._y && _width == rect._width && _height == rect._height;
        }

        public override bool Equals (object obj)
        {
            if (null == obj || !(obj is Rect))
                return false;
            return Equals ((Rect)obj);
        }

        public override int GetHashCode ()
        {
            return _x.GetHashCode () ^ _y.GetHashCode () ^ _width.GetHashCode () ^ _height.GetHashCode ();
        }

        public static bool operator == (Rect left, Rect right)
        {
            return left.Equals (right);
        }

        public static bool operator != (Rect left, Rect right)
        {
            return !left.Equals (right);
        }

        public void Offset (int x, int y)
        {
            _x += x;
            _y += y;
        }

        public void Offset (Vector vector)
        {
            Offset (MathHelper.Round(vector.X), MathHelper.Round(vector.Y));
        }

        public void Inflate (int width, int height)
        {
            _x -= width;
            _y -= height;
            _width += 2 * width;
            _height += 2 * height;
        }

        public Rect Union (Rect rect)
        {
            if (this == Empty)
                return rect;
            if (rect == Empty)
                return this;

            int left = Math.Min (Left, rect.Left);
            int top = Math.Min (Top, rect.Top);

            int right = Math.Max (Right, rect.Right);
            int bottom = Math.Max (Bottom, rect.Bottom);

            return Rect.FromLTRB (left, top, right, bottom);
        }

        public Rect? Intersects (Rect rect)
        {
            int left = Math.Max (Left, rect.Left);
            int right = Math.Min (Right, rect.Right);
            int top = Math.Max (Top, rect.Top);
            int bottom = Math.Min (Bottom, rect.Bottom);

            if (left <= right && top <= bottom)
                return Rect.FromLTRB (left, top, right, bottom);

            return null;
        }

        public override string ToString ()
        {
            return string.Format ("{0},{1},{2},{3}", _x.ToString (), _y.ToString (), _width.ToString (), _height.ToString ());
        }


        public static Rect FromLTRB (int left, int top, int right, int bottom)
        {
            return new Rect (left, top, right - left, bottom - top);
        }


        public static Rect FromPoints (Point p1, Point p2)
        {
            return FromLTRB (Math.Min (p1.X, p2.X), 
                Math.Min (p1.Y, p2.Y),
                Math.Max (p1.X, p2.X),
                Math.Max (p1.Y, p2.Y));
        }
    }
}
