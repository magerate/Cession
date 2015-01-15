namespace Cession.Modeling
{
	using System;

	using Cession.Geometries;

	public abstract class HitTestProvider
	{
		public virtual Diagram HitTest (Diagram diagram, Point2 point){
			if (null == diagram)
				throw new ArgumentNullException ("diagram");
			return DoHitTest (diagram, point);
		}

		protected virtual Diagram DoHitTest (Diagram diagram, Point2 point){
			var bounds = GetBounds (diagram);
			if (bounds.Contains (point))
				return diagram;
			return null;
		}

		public Rect GetBounds(Diagram diagram){
			if(null == diagram)
				throw new ArgumentNullException("diagram");
			return DoGetBounds(diagram);
		}

		protected abstract Rect DoGetBounds (Diagram diagram);
	}
}

