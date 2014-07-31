namespace Cession.Modeling
{
	using System.Collections.Generic;

	using Cession.Geometries;

	public abstract class CompositeDiagram:Diagram
	{
		protected CompositeDiagram ()
		{
		}

		protected CompositeDiagram(Diagram parent):base(parent)
		{
		}

		public abstract IEnumerable<Diagram> Traverse ();


		public override Diagram HitTest (Point2 point)
		{
			foreach (var diagram in Traverse ()) {
				var hr = diagram.HitTest (point);
				if (null != hr)
					return hr;
			}
			return null;
		}

		public override void Offset (int x, int y)
		{
			foreach (var diagram in Traverse ()) {
				diagram.Offset (x, y);
			}
		}
	}
}

