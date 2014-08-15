namespace Cession.Modeling
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;

	using Cession.Geometries;

	public class Room:CompositeDiagram
	{
		public Floor Floor{ get; private set; }
		public Ceiling Ceiling{ get; private set; }
		public WallCollection Walls{ get; private set; }

		public ClosedShapeDiagram Contour{ get; set; }
		public ClosedShapeDiagram OuterContour{ get; private set; }

		public Room (ClosedShapeDiagram contour)
		{
			this.Contour = contour;
			contour.Parent = this;
			Floor = new Floor (this);

			if (contour is PathDiagram)
				OuterContour = (contour as PathDiagram).OffsetPolygon (10 * 25);
			else if (contour is RectangleDiagram) {
				OuterContour = RectangleDiagram.Inflate (contour as RectangleDiagram, 10 * 25, 10 * 25);
			}
			OuterContour.Parent = this;
		}


		public override bool CanSelect {
			get {
				return true;
			}
		}

		public override IEnumerable<Diagram> Traverse ()
		{
			yield return Floor;

			if(Floor.Regions[0].Contour != Contour)
				yield return Contour;

			yield return OuterContour;
		}
	}
}

