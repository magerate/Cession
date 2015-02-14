using System;

using Cession.Geometries;
using Cession.Diagrams;

namespace Cession.Handles
{
    public abstract class Handle
    {
        private Shape _shape;
        private Point _location;

        public Shape Shape
        {
            get{ return _shape; }
            set{ _shape = value; }
        }

        public Point Location
        {
            get{ return _location; }
            set{ _location = value; }
        }

        protected Handle (Shape shape,Point location)
        {
            _shape = shape;
            _location = location;
        }

        public abstract Handle HitTest (Point point,Matrix transform);

        public virtual void Offset(double offsetX,double offsetY)
        {
            _location.Offset (offsetX, offsetY);
        }
    }
}

