namespace Cession.Modeling
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using Cession.Geometries;

	public class ShapeSelectedEventArgs:EventArgs
	{
		public Diagram Shape{get;set;}

		public ShapeSelectedEventArgs(Diagram shape)
		{
			this.Shape = shape;
		}
	}


	public class Layer
	{
		public string Name{ get; set; }
		public DiagramCollection Diagrams{ get; private set; }

		private List<Diagram> selectedDiagrams = new List<Diagram> ();
		private ReadOnlyCollection<Diagram> readOnlySelectedDiagrams;

		public Matrix Transform{ get; set; }

		public ReadOnlyCollection<Diagram> SelectedDiagrams
		{
			get{ return readOnlySelectedDiagrams; }
		}

		public event EventHandler<ShapeSelectedEventArgs> ShapeSelected;
		public event EventHandler<EventArgs> SelectionClear;


		public Layer (string name)
		{
			this.Transform = Matrix.Identity;

			this.Name = name;
			Diagrams = new DiagramCollection ();

			readOnlySelectedDiagrams = new ReadOnlyCollection<Diagram> (selectedDiagrams);
		}

		public Diagram HitTest(Point2 point)
		{
			foreach (var item in Diagrams) {
				var de = item.HitTest (point);
				if (null != de)
					return de;
			}

			return null;
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
	}
}

