namespace Cession.Modeling
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;

	using Cession.Geometries;

	public class Room:CompositeDiagram
	{
		public static readonly RoutedEvent DoorRemovedEvent = new RoutedEvent ("DoorRemoved",
			                                      typeof(EventHandler<DoorRemovedEventArgs>),
			                                      typeof(Room));


		public static int DefaultWallThickness{ get; set; }

		public Floor Floor{ get; private set; }
		public Ceiling Ceiling{ get; private set; }
		public WallCollection Walls{ get; private set; }

		public ClosedShapeDiagram Contour{ get; set; }
		public ClosedShapeDiagram OuterContour{ get; private set; }

		public List<Door> Doors = new List<Door>();

		static Room(){
			DefaultWallThickness = 200;
		}

		public Room (ClosedShapeDiagram contour):this(contour,null)
		{
		}

		public Room(ClosedShapeDiagram contour,Diagram parent):base(parent){
			this.Contour = contour;
			contour.Parent = this;
			Floor = new Floor (this);

			RefreshOuterContour ();
			OuterContour.Parent = this;

			AddHandler (Diagram.ShapeChangeEvent, new RoutedEventHandler(ContourShapeChanged));
			AddHandler (Diagram.MoveEvent, new RoutedEventHandler (OnChildMoved));
		}

		private void OnChildMoved(object sender,RoutedEventArgs e){
			if (e.Source == Contour ||
			   e.Source == OuterContour ||
			   e.Source == Ceiling ||
			   e.Source == Floor) {
				var message = string.Format ("Can't move {0} individualy, use Move method of Room instead", e.Source);
				throw new InvalidOperationException (message);
			}
		}

		private void ContourShapeChanged(object sender,RoutedEventArgs e){
			RefreshOuterContour ();

			var invalidDoors = Doors.Where (d => !d.IsValid).ToArray ();
			foreach (var d in invalidDoors) {
				RemoveDoor (d, true);
			}
		}

		private void RefreshOuterContour(){
			if (Contour is PathDiagram)
				OuterContour = (Contour as PathDiagram).OffsetPolygon (DefaultWallThickness);
			else if (Contour is RectangleDiagram) {
				OuterContour = RectangleDiagram.Inflate (Contour as RectangleDiagram, 
					DefaultWallThickness, 
					DefaultWallThickness);
			}
		}


		public override bool CanSelect {
			get {
				return true;
			}
		}

		public override IEnumerator<Diagram> GetEnumerator ()
		{
			foreach (var door in Doors) {
				yield return door;
			}

			yield return Floor;

			if(Floor.Regions[0].Contour != Contour)
				yield return Contour;

			yield return OuterContour;
		}

		private void RemoveDoor(Door door,bool isSideEffect){
			Doors.Remove (door);
			var args = new DoorRemovedEventArgs (Room.DoorRemovedEvent, this);
			args.IsSideEffect = isSideEffect;
			args.Door = door;
			RaiseEvent (args);
		}

		public void RemoveDoor(Door door){
			RemoveDoor (door, false);
		}

		public void AddDoor(Door door){
			Doors.Add (door);
		}
	}
}

