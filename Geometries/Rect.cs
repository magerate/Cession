using System;

namespace Cession.Geometries
{
    public struct Rect : IEquatable<Rect>
    {
        public static readonly Rect Empty = new Rect (0, 0, 0, 0);
        private double _x;
        private double _y;
        private double _width;
        private double _height;

        public Point Location
        {
            get{ return new Point (_x, _y); }
        }

        public double X
        {
            get { return _x; }
            set { _x = value; }
        }

        public double Left
        {
            get { return _x; }
        }

        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public double Bottom
        {
            get { return _y + _height; }
        }

        public double Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public Size Size
        {
            get{ return new Size (_width, _height); }
        }

        public double Right
        {
            get { return _x + _width; }
        }

        public double Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public double Top
        {
            get { return _y; }
        }

        public Point Center
        {
            get{ return new Point (_x + _width / 2, _y + Height / 2); }
        }


        public Point LeftBottom
        {
            get { return new Point (this.Left, this.Bottom); }
        }

        public Point RightBottom
        {
            get { return new Point (this.Right, this.Bottom); }
        }

        public Point LeftTop
        {
            get { return new Point (this.Left, this.Top); }
        }

        public Point RightTop
        {
            get { return new Point (this.Right, this.Top); }
        }

        public Rect (double x, double y, double width, double height)
        {
            if (width < 0)
                throw new ArgumentException ();
            if (height < 0)
                throw new ArgumentException ();

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
            return this._x == rect._x && this._y == rect._y &&
            this._width == rect._width && this._height == rect._height;
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

        public void Offset (double x, double y)
        {
            this._x += x;
            this._y += y;
        }

        public void Offset (Vector vector)
        {
            this.Offset (vector.X, vector.Y);
        }

        public void Inflate (double width, double height)
        {
            this._x -= width;
            this._y -= height;
            this._width += 2 * width;
            this._height += 2 * height;

        }

        public Rect Union (Rect rect)
        {
            if (this == Empty)
                return rect;
            if (rect == Empty)
                return this;

            var left = Math.Min (this.Left, rect.Left);
            var top = Math.Min (this.Top, rect.Top);

            var right = Math.Max (this.Right, rect.Right);
            var bottom = Math.Max (this.Bottom, rect.Bottom);

            return Rect.FromLTRB (left, top, right, bottom);
        }

        public Rect? Intersects (Rect rect)
        {
            var left = Math.Max (this.Left, rect.Left);
            var right = Math.Min (this.Right, rect.Right);
            var top = Math.Max (this.Top, rect.Top);
            var bottom = Math.Min (this.Bottom, rect.Bottom);

            if (left <= right && top <= bottom)
                return Rect.FromLTRB (left, top, right, bottom);

            return null;
        }

        public override string ToString ()
        {
            return string.Format ("{0},{1},{2},{3}", _x.ToString (), _y.ToString (), _width.ToString (), _height.ToString ());
        }


        public static Rect FromLTRB (double left, double top, double right, double bottom)
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
