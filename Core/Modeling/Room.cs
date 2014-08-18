namespace Cession.Modeling
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;

	using Cession.Geometries;

	public class Room:CompositeDiagram
	{
		public static int DefaultWallThickness{ get; set; }

		public Floor Floor{ get; private set; }
		public Ceiling Ceiling{ get; private set; }
		public WallCollection Walls{ get; private set; }

		public ClosedShapeDiagram Contour{ get; set; }
		public ClosedShapeDiagram OuterContour{ get; private set; }

		public Room (ClosedShapeDiagram contour)
		{
			DefaultWallThickness = 200;

			this.Contour = contour;
			contour.Parent = this;
			Floor = new Floor (this);

			RefreshOuterContour ();
			OuterContour.Parent = this;

			this.AddHandler (Diagram.ShapeChangeEvent, new RoutedEventHandler(ContourShapeChanged));
		}

		private void ContourShapeChanged(object sender,RoutedEventArgs e){
			RefreshOuterContour ();
		}

		private void RefreshOuterContour(){
			if (Contour is PathDiagram)
				OuterContour = (Contour as PathDiagram).OffsetPolygon (DefaultWallThickness);
			else if (Contour is RectangleDiagram) {
				OuterContour = RectangleDiagram.Inflate (Contour as RectangleDiagram, DefaultWallThickness, DefaultWallThickness);
			}
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

