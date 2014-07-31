namespace Cession.Modeling
{
	using System;
	using System.Collections.Generic;

	using Cession.Geometries;
	using Cession.Geometries.Shapes;

	public class Floor:CompositeDiagram
	{
		public List<RegionDiagram> Regions{get;private set;}

		public Floor (Room parent):base(parent)
		{
			Regions = new List<RegionDiagram> ();
			Regions.Add (new RegionDiagram (this, parent.Contour));
		}

		public override IEnumerable<Diagram> Traverse ()
		{
			return Regions;
		}

		public override bool CanSelect {
			get {
				return false;
			}
		}
	}
}

