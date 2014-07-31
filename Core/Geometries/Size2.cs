// -----------------------------------------------------------------------
// <copyright file="Size2.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Cession.Geometries
{
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public struct Size2:IEquatable<Size2>
    {
        private double width;
        private double height;

        public Size2(double width,double height)
        {
            this.width = width;
            this.height = height;
        }

        public double Width
        {
            get { return width; }
            set { width = value; }
        }

        public double Height
        {
            get { return height; }
            set { height = value; }
        }

        public bool Equals(Size2 size)
        {
            return this.width == size.width && this.height == size.height;
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
            return this.width.GetHashCode() ^ this.height.GetHashCode();
        }
    }
}
