namespace Cession.Diagrams
{
	using System;
	using Cession.Geometries;

	public abstract class HitTestProvider
	{
		public abstract Rect GetBounds (Shape shape);

		public bool Contains(Point2 point){
			if (!GetBounds().Contains (point))
				return false;
			return DoContains (point);
		}

		protected abstract bool DoContains(Point2 point);

		public Shape HitTest(Shape shape,Point2d point,int delta = 0){
			if (null == shape)
				throw new ArgumentNullException ();
		}

		protected abstract Shape DoHitTest (Shape shape, Point2d point, int delta);
	}
}

