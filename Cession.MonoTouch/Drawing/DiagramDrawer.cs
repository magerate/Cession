namespace Cession.Drawing
{
	using System;

	using MonoTouch.CoreGraphics;

	using Cession.Modeling;


	public abstract class DiagramDrawer 
	{
		protected DiagramDrawer ()
		{
		}

		public void Draw(CGContext context, Diagram diagram)
		{
			if(null == context )
				throw new ArgumentNullException();	
			if(null == diagram)
				throw new ArgumentNullException();	

			DoDraw (context, diagram);
		}

		public virtual void DrawSelected(CGContext context, Diagram diagram)
		{
			if(null == context )
				throw new ArgumentNullException();	
			if(null == diagram)
				throw new ArgumentNullException();	

			DoDrawSelected (context, diagram);
		}

		protected abstract void DoDraw(CGContext context,Diagram diagram);
		protected virtual void DoDrawSelected(CGContext context,Diagram diagram){
		}
	}
}

