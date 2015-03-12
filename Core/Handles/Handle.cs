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

        protected Handle (Shape shape)
        {
            if (null == shape)
                throw new ArgumentNullException ("shape");
            _shape = shape;
        }

        public abstract bool Contains (Point point, Matrix transform);

//        public virtual void Offset(double offsetX,double offsetY)
//        {
//            _location.Offset (offsetX, offsetY);
//        }
    }
}

