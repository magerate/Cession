using System;

namespace Cession.Geometries
{
    public struct Size:IEquatable<Size>
    {
        public static readonly Size Empty = new Size (0, 0);

        private int _width;
        private int _height;

        public Size (int width, int height)
        {
            if (width < 0)
                throw new ArgumentOutOfRangeException ("width");
            if (height < 0)
                throw new ArgumentOutOfRangeException ("height");

            _width = width;
            _height = height;
        }

        public bool IsEmpty
        {
            get{ return _width == 0 && _height == 0; }
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

        public bool Equals (Size size)
        {
            return this._width == size._width && this._height == size._height;
        }

        public override bool Equals (object obj)
        {
            if (null == obj || !(obj is Size))
                return false;
            return Equals ((Size)obj);
        }

        public static bool operator == (Size left, Size right)
        {
            return left.Equals (right);
        }

        public static bool operator != (Size left, Size right)
        {
            return !left.Equals (right);
        }

        public override int GetHashCode ()
        {
            return _width.GetHashCode () ^ _height.GetHashCode ();
        }
    }
}
