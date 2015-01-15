using System;

namespace Cession.Geometries
{
    public struct Size2:IEquatable<Size2>
    {
		private int _width;
		private int _height;

        public Size2(int width,int height)
        {
            this._width = width;
            this._height = height;
        }

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public bool Equals(Size2 size)
        {
            return this._width == size._width && this._height == size._height;
        }

        public override bool Equals(object obj)
        {
            if (null == obj || !(obj is Size2))
                return false;
            return Equals((Size2)obj);
        }

        public static bool operator ==(Size2 left,Size2 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Size2 left,Size2 right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            return this._width.GetHashCode() ^ this._height.GetHashCode();
        }
    }
}
