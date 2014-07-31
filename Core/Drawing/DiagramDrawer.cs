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

		public virtual void Draw(CGContext context, Diagram diagramElement)
		{
			if(null == context || null == diagramElement)
				throw new ArgumentNullException();			
		}

		public virtual void DrawSelected(CGContext context, Diagram diagramElement)
		{
			if(null == context || null == diagramElement)
				throw new ArgumentNullException();			
		}


	}
}

