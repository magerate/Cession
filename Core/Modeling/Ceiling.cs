namespace Cession.Modeling
{
	using Cession.Geometries;

	public class Ceiling:Diagram
	{
		public Ceiling (Room parent):base(parent)
		{
		}

		public override bool CanSelect {
			get {
				return false;
			}
		}

		public override Diagram HitTest (Point2 point)
		{
			return null;
		}

		public override Rect Bounds {
			get {
				throw new System.NotImplementedException ();
			}
		}

		internal override void InternalOffset (int x, int y)
		{
		}
	}
}

