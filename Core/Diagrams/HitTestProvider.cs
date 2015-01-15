namespace Cession.Diagrams
{
	using System;
	using Cession.Geometries;

	public abstract class HitTestProvider
	{
		public abstract Rect GetBounds (Shape shape);

		public bool Contains(Shape shape,Point2 point){
			if (!GetBounds(shape).Contains (point))
				return false;
			return DoContains (shape,point);
		}

		protected abstract bool DoContains(Shape shape,Point2 point);

		public Shape HitTest(Shape shape,Point2 point,int delta = 0){
			if (null == shape)
				throw new ArgumentNullException ();
			return null;
		}

		protected abstract Shape DoHitTest (Shape shape, Point2 point, int delta);
	}
}

