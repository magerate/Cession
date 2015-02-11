using System;
using CoreGraphics;
using Cession.Diagrams;

namespace Cession.Drawing
{
    public abstract class ShapeDrawer
    {
        protected ShapeDrawer ()
        {
        }

        public void Draw (DrawingContext context, Shape shape)
        {
            if (null == context)
                throw new ArgumentNullException ();	
            if (null == shape)
                throw new ArgumentNullException ();	

            DoDraw (context, shape);
        }

        public virtual void DrawSelected (DrawingContext context, Shape shape)
        {
            if (null == context)
                throw new ArgumentNullException ();	
            if (null == shape)
                throw new ArgumentNullException ();	

            DoDrawSelected (context, shape);
        }

        protected abstract void DoDraw (DrawingContext context, Shape shape);

        protected virtual void DoDrawSelected (DrawingContext context, Shape shape)
        {
        }
    }
}

