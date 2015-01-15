namespace Cession.Modeling
{
	using System.Collections;
	using System.Collections.Generic;

	using Cession.Geometries;

	public abstract class CompositeDiagram:Diagram,IEnumerable<Diagram>
	{
		protected CompositeDiagram ()
		{
		}

		protected CompositeDiagram(Diagram parent):base(parent)
		{
		}

		public abstract IEnumerator<Diagram> GetEnumerator ();

		IEnumerator IEnumerable.GetEnumerator(){
			return this.GetEnumerator ();
		}

		public override Diagram HitTest (Point2 point)
		{
			foreach (var diagram in this) {
				var hr = diagram.HitTest (point);
				if (null != hr)
					return hr;
			}
			return null;
		}

		public override Rect Bounds {
			get {
				Rect bounds = Rect.Empty;
				foreach (var diagram in this) {
					bounds = bounds.Union (diagram.Bounds);
				}
				return bounds;
			}
		}

		internal override void InternalOffset (int x, int y)
		{
			foreach (var diagram in this) {
				diagram.InternalOffset (x, y);
			}
		}
	}
}

