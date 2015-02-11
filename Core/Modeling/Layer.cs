namespace Cession.Modeling
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;

	using Cession.Geometries;
	using Cession.Utilities;

	public class ShapeSelectedEventArgs:EventArgs
	{
		public Diagram Shape{get;set;}

		public ShapeSelectedEventArgs(Diagram shape)
		{
			this.Shape = shape;
		}
	}

	public class Layer:Diagram
	{
		public static readonly Size DefaultSize = new Size (200000, 200000);

		public string Name{ get; set; }
		public ShapeCollection Diagrams{ get; private set; }

		private List<Diagram> selectedDiagrams = new List<Diagram> ();
		private ReadOnlyCollection<Diagram> readOnlySelectedDiagrams;

		public Matrix Transform{ get; set; }
		public double Scale{
			get{
				return Transform.M11;
			}
		}

		public Size Size{get;set;}

		public ReadOnlyCollection<Diagram> SelectedDiagrams
		{
			get{ return readOnlySelectedDiagrams; }
		}

		public List<RoomGroup> RoomGroups{ get; private set; }

		public event EventHandler<ShapeSelectedEventArgs> ShapeSelected;
		public event EventHandler<EventArgs> SelectionClear;


		public Layer(string name){

			this.Transform = Matrix.Identity;

			//default layer size 200 meter
			Size = DefaultSize;

			this.Name = name;
			Diagrams = new ShapeCollection ();

			readOnlySelectedDiagrams = new ReadOnlyCollection<Diagram> (selectedDiagrams);

			this.AddHandler (Diagram.MoveEvent, new RoutedEventHandler(ShapeMoved));

			RoomGroups = new List<RoomGroup> ();
		}

		public override Diagram HitTest(Point2 point)
		{
			foreach (var diagram in Diagrams) {
				var de = diagram.HitTest (point);
				if (null != de)
					return de;
			}

			return null;
		}

		public override Rect Bounds {
			get {
				return new Rect (0, 0, Size.Width, Size.Height);
			}
		}

		public string CreateRoomName(){
			var names =  Diagrams.
				Where (d => d is Room).
				Select(r => ((Room)r).Name);
			return "Room".CreateDuplicateName (names);
		}

		public void Select(Diagram diagram)
		{
			if (null == diagram)
				return;

			selectedDiagrams.Add (diagram);
			OnShapeSelected (new ShapeSelectedEventArgs (diagram));
		}

		public void ClearSelection()
		{
			selectedDiagrams.Clear ();
			OnSelectionClear ();
		}

		private void OnSelectionClear()
		{
			if (null != SelectionClear)
				SelectionClear (this, EventArgs.Empty);
		}

		private void OnShapeSelected(ShapeSelectedEventArgs e)
		{
			if (null != ShapeSelected)
				ShapeSelected (this, e);
		}

		public Point2 ConvertToLogicalPoint(Point2 point)
		{
			var matrix = Transform;
			matrix.Invert ();

			return matrix.Transform (point);
		}

		public Point2 ConvertToViewPoint(Point2 point)
		{
			return Transform.Transform (point);
		}

		public Size ConvertToLogicalSize(Size size){
			return new Size ((int)(size.Width / Transform.M11), 
				(int)(size.Height / Transform.M11));
		}

		public Size ConvertToViewSize(Size size){
			return new Size ((int)(size.Width * Transform.M11), 
				(int)(size.Height * Transform.M11));
		}

		public int ConvertToLogicalSize(int size){
			return (int)(size / Transform.M11);
		}

		public static Matrix GetDefaultTransform(Size layerSize,int width,int height,int logicalUnitPerDp){
			var matrix = Matrix.Identity;
			matrix.Scale ((double)1 / logicalUnitPerDp, (double)1 / logicalUnitPerDp);
			matrix.Translate ((width   - layerSize.Width / logicalUnitPerDp) / 2,
				(height  - layerSize.Height / logicalUnitPerDp) / 2);
			return matrix;
		}

		public static Matrix GetDefaultTransform(int width,int height,int logicalUnitPerDp){
			return GetDefaultTransform (Layer.DefaultSize, width, height,logicalUnitPerDp);
		}

		internal override void InternalOffset (int x, int y)
		{
			Transform.Translate ((double)x, (double)y);
		}

		public IEnumerable<Diagram> GetDiagrams(Rect bounds){
			return Diagrams.Where (d => d.Bounds.Intersect (bounds).HasValue);
		}

		public IEnumerable<Room> GetRooms(Rect bounds){
			return GetDiagrams (bounds).Where (d => d is Room).Cast<Room> ();
		}

		private void ShapeMoved(object sender,RoutedEventArgs e){
			if (e.OriginalSource is Room) {
				var room = e.OriginalSource as Room;
				var size = ConvertToLogicalSize (24);
				var bounds = room.Bounds;
				bounds.Inflate (size, size);

				var candidateRooms = GetRooms (bounds).
					Where (r => r.Bounds.Intersect (bounds).HasValue && r != room);

				DockRoom (room, candidateRooms);
			}
		}

		internal RoomGroup GetRoomGroup(Room room){
			return RoomGroups.FirstOrDefault (rg => rg.Contains (room));
		}

		private void DockRoom(Room room,IEnumerable<Room> candidateRooms){
			var roomGroup = GetRoomGroup (room);
			if (null != roomGroup){
				roomGroup.Remove (room);
				if (roomGroup.Count < 2)
					RoomGroups.Remove (roomGroup);
			}

			foreach (var r in candidateRooms) {
				if (TryDockRoom (room, r) != null) {
					roomGroup = GetRoomGroup (room);
					if (null != roomGroup)
						roomGroup.Add (r);
					else {
						roomGroup = new RoomGroup ();
						roomGroup.Add (room);
						roomGroup.Add (r);
						this.RoomGroups.Add (roomGroup);
					}
				}
			}
		}

		private Room TryDockRoom(Room room,Room targetRoom){
			if(!(room.Contour is IPolygonal) ||
				!(targetRoom.Contour is IPolygonal))
				return null;

			var polygon1 = room.Contour as IPolygonal;
			var polygon2 = room.Contour as IPolygonal;

			for (int i = 0; i < polygon1.SideCount; i++) {
				var side = polygon1 [i];
				if(IsSideDocked(side,polygon2))
					return targetRoom;
			}
			return null;
		}

		private bool IsSideDocked(Segment segment,IPolygonal polygon){
			for (int i = 0; i < polygon.SideCount; i++) {
				var side = polygon [i];
				if (IsSideDocked (segment, side))
					return true;
			}
			return false;
		}

		private bool IsSideDocked(Segment side1,Segment side2){
			var v1 = side1.P2 - side1.P1;
			var v2 = side2.P2 - side2.P1;

			v1.Normalize ();
			v2.Normalize ();

			var crossProduct = v1.CrossProduct (v2);

			if (!(crossProduct == 0 && v1.Angle != v2.Angle))
				return false;

			int delta = 4000; 
			if (Range.Contains (side1.P1.X, side1.P2.X, side2.P1.X, delta) &&
			   Range.Contains (side1.P1.Y, side1.P2.Y, side2.P1.Y, delta))
				return true;

			if (Range.Contains (side1.P1.X, side1.P2.X, side2.P2.X, delta) &&
			    Range.Contains (side1.P1.Y, side1.P2.Y, side2.P2.Y, delta))
				return true;

			if (Range.Contains (side2.P1.X, side2.P2.X, side1.P1.X, delta) &&
				Range.Contains (side2.P1.Y, side2.P2.Y, side1.P1.Y, delta))
				return true;

			if (Range.Contains (side2.P1.X, side2.P2.X, side1.P2.X, delta) &&
				Range.Contains (side2.P1.Y, side2.P2.Y, side1.P2.Y, delta))
				return true;

			return false;
		}
	}
}

