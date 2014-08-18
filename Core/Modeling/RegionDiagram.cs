namespace Cession.Modeling
{
	using System;
	using System.Collections.Generic;

	using Cession.Geometries;

	public class RegionDiagram:CompositeDiagram
	{
		public ClosedShapeDiagram Contour{ get; set; }
		public List<ClosedShapeDiagram> Holes{ get; private set; }

		public RegionDiagram (Diagram parent,ClosedShapeDiagram contour):base(parent)
		{
			this.Contour = contour;
			Holes = new List<ClosedShapeDiagram> ();
		}

		public override IEnumerator<Diagram> GetEnumerator()
		{
			foreach (var hole in Holes) {
				yield return hole;
			}
			yield return Contour;
		}
	}
}

