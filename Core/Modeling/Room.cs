namespace Cession.Modeling
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;

	using Cession.Geometries;
	using Cession.Dimensions;

	public class Room:CompositeDiagram
	{
		public static readonly RoutedEvent DoorRemovedEvent = new RoutedEvent ("DoorRemoved",
			                                      typeof(EventHandler<DoorRemovedEventArgs>),
			                                      typeof(Room));


		public static double DefaultWallThickness{ get; set; }

		private string name;

		public string Name
		{ 
			get{ return name; }
			set{
				if (name != value) {
					name = value;
					Label.Text = name;
					CenterLabel ();
				}
			}
		}

		public bool IsDocked{
			get{ var layer = Parent as Layer;
				if (null == layer)
					return false;
				return layer.GetRoomGroup (this) != null;
			}
		}
		public Floor Floor{ get; private set; }
		public Ceiling Ceiling{ get; private set; }
		public WallCollection Walls{ get; private set; }
		public Label Label{get;private set;}

		public ClosedShapeDiagram Contour{ get; set; }
		public ClosedShapeDiagram OuterContour{ get; private set; }
		public IReadOnlyList<Door> Doors{
			get{ 
				if (null == readonlyDoorCollection) {
					EnsureDoorsExist ();
					readonlyDoorCollection = new ReadOnlyCollection<Door> (doors);
				}
				return readonlyDoorCollection; 
			}
		}

		private List<Door> doors;
		private ReadOnlyCollection<Door> readonlyDoorCollection;

		static Room(){
			DefaultWallThickness = Length.FromCentimeters(20).Value;
		}

		public Room (ClosedShapeDiagram contour):this(contour,null)
		{
		}

		public Room(ClosedShapeDiagram contour,Diagram parent):base(parent){
			this.Contour = contour;
			contour.Parent = this;
			Floor = new Floor (this);

			name = "Room";
			Label = new Label (Name, Point2.Empty, this);

			RefreshOuterContour ();
			OuterContour.Parent = this;

			AddHandler (Diagram.ShapeChangeEvent, new RoutedEventHandler(ContourShapeChanged));
			AddHandler (Diagram.MoveEvent, new RoutedEventHandler (OnChildMoved));
		}

		private void CenterLabel(){
			var outerBounds = OuterContour.Bounds;
			var bounds = Label.Bounds;

			int x = outerBounds.X + (outerBounds.Width - bounds.Width) / 2;
			int y = outerBounds.Y + (outerBounds.Height - bounds.Height) / 2;

			Label.Location = new Point2 (x, y);
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
			var thickness = (int)DefaultWallThickness;
			if (Contour is PathDiagram)
				OuterContour = (Contour as PathDiagram).OffsetPolygon (thickness);
			else if (Contour is RectangleDiagram) {
				OuterContour = RectangleDiagram.Inflate (Contour as RectangleDiagram, 
					thickness, 
					thickness);
			}

			CenterLabel ();
		}


		public override bool CanSelect {
			get {
				return true;
			}
		}

		public override Rect Bounds {
			get {
				return OuterContour.Bounds.Union (Label.Bounds);
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
			yield return Label;
		}

		private void RemoveDoor(Door door,bool isSideEffect){
			EnsureDoorsExist ();
			doors.Remove (door);
			var args = new DoorRemovedEventArgs (Room.DoorRemovedEvent, this);
			args.IsSideEffect = isSideEffect;
			args.Door = door;
			RaiseEvent (args);
		}

		private void EnsureDoorsExist()
		{
			if (null == doors)
				doors = new List<Door> ();
		}

		public void RemoveDoor(Door door){
			RemoveDoor (door, false);
		}

		public void AddDoor(Door door){
			EnsureDoorsExist ();
			doors.Add (door);
		}
	}
}

