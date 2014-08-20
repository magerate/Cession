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

	public class Layer:Diagram
	{
		public static readonly Size2 DefaultSize = new Size2 (200000, 200000);

		public string Name{ get; set; }
		public DiagramCollection Diagrams{ get; private set; }
		public Project Project{ get; private set; }

		private List<Diagram> selectedDiagrams = new List<Diagram> ();
		private ReadOnlyCollection<Diagram> readOnlySelectedDiagrams;

		public Matrix Transform{ get; set; }
		public double Scale{
			get{
				return Transform.M11;
			}
		}

		public Size2 Size{get;set;}

		public ReadOnlyCollection<Diagram> SelectedDiagrams
		{
			get{ return readOnlySelectedDiagrams; }
		}

		public event EventHandler<ShapeSelectedEventArgs> ShapeSelected;
		public event EventHandler<EventArgs> SelectionClear;


		public Layer(Project project,string name){
			this.Project = project;

			this.Transform = Matrix.Identity;

			//default layer size 200 meter
			Size = DefaultSize;

			this.Name = name;
			Diagrams = new DiagramCollection ();

			readOnlySelectedDiagrams = new ReadOnlyCollection<Diagram> (selectedDiagrams);
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

		public static Matrix GetDefaultTransform(Size2 layerSize,int width,int height,int logicalUnitPerDp){
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

	}
}

