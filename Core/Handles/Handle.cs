using System;

using Cession.Geometries;
using Cession.Diagrams;

namespace Cession.Handles
{
    public abstract class Handle
    {
        private Shape _shape;

        public Shape Shape
        {
            get{ return _shape; }
        }


        public abstract Point Location{ get; }
        public abstract Type ToolType{ get; }

        //handle size is specified in device coordiante space
        public virtual double Size
        {
            get{ return 18; }
        }

        //bounds of handles is specified in device coordiante space
        //then handle won't scale when diagram scaled
        public Rect Bounds
        {
            get
            {
                var point = Transform.Transform (Location);
                return new Rect (point.X - Size / 2, point.Y - Size / 2, Size, Size);
            }
        }

        protected Matrix Transform
        {
            get
            { 
                var layer = Shape.Owner as Layer;
                return layer.Transform;
            }
        }

        protected Handle (Shape shape)
        {
            if (null == shape)
                throw new ArgumentNullException ("shape");
            _shape = shape;
        }

        public virtual Matrix GetHanldeTransform()
        {
            return Matrix.Identity;
        }

        public bool Contains (Point point)
        {
            Rect bounds = Bounds;
            point = Transform.Transform (point);

            var matrix = GetHanldeTransform ();
            matrix.Invert ();

            point = matrix.Transform (point);

            return bounds.Contains (point);
        }
    }
}

