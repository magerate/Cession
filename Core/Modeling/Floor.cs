namespace Cession.Modeling
{
	using System;
	using System.Collections.Generic;

	using Cession.Geometries;

	public class Floor:CompositeDiagram
	{
		public List<RegionDiagram> Regions{get;private set;}

		public Floor (Room parent):base(parent)
		{
			Regions = new List<RegionDiagram> ();
			Regions.Add (new RegionDiagram (this, parent.Contour));
		}

		public override IEnumerator<Diagram> GetEnumerator ()
		{
			return Regions.GetEnumerator();
		}

		public override bool CanSelect {
			get {
				return false;
			}
		}
	}
}

