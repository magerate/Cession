namespace Cession.Modeling
{
	using Cession.Geometries;

	public abstract class ClosedShapeDiagram:Diagram
	{
		protected ClosedShapeDiagram ()
		{
		}

		public abstract double GetArea ();
		public abstract double GetPerimeter ();

		public abstract Segment this[int index]{
			get;
		}
	}
}

