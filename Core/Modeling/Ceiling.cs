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

		public override void Offset (int x, int y)
		{

		}
	}
}

