using System;

namespace Cession.Geometries
{
    public struct Size:IEquatable<Size>
    {
        private double _width;
        private double _height;

        public Size (double width, double height)
        {
            this._width = width;
            this._height = height;
        }

        public double Width {
            get { return _width; }
            set { _width = value; }
        }

        public double Height {
            get { return _height; }
            set { _height = value; }
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
