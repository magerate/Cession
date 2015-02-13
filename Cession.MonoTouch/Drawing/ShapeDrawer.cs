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

        public void Draw (DrawingContext drawingContext, Shape shape)
        {
            if (null == drawingContext)
                throw new ArgumentNullException ();	
            if (null == shape)
                throw new ArgumentNullException ();	

            DoDraw (drawingContext, shape);
        }

        public void DrawSelected (DrawingContext drawingContext, Shape shape)
        {
            if (null == drawingContext)
                throw new ArgumentNullException ();	
            if (null == shape)
                throw new ArgumentNullException ();	

            DoDrawSelected (drawingContext, shape);
        }

        protected abstract void DoDraw (DrawingContext drawingContext, Shape shape);

        protected virtual void DoDrawSelected (DrawingContext drawingContext, Shape shape)
        {
        }
    }
}

