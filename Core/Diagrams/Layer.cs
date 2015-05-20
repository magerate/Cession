using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Cession.Geometries;
using Cession.Dimensions;

namespace Cession.Diagrams
{
    public class ShapeSelectedEventArgs:EventArgs
    {
        public Shape Shape{ get; set; }

        public ShapeSelectedEventArgs (Shape shape)
        {
            this.Shape = shape;
        }
    }

    public class Layer:Shape
    {
        public static readonly Size DefaultSize = new Size (200 * Length.PixelsPerMeter, 200 * Length.PixelsPerMeter);

        public string Name{ get; set; }

        public LayerShapeCollection Shapes{ get; private set; }

        private List<Shape> _selectedShapes = new List<Shape> ();
        private ReadOnlyCollection<Shape> _readOnlySelectedShapes;

        public Matrix Transform{ get; set; }

        public double Scale
        {
            get{return Transform.M11;}
        }

        public Size Size{ get; set; }

        public Rect Frame
        {
            get{ return new Rect (-Size.Width / 2, -Size.Height / 2, Size.Width, Size.Height); }
        }

        public ReadOnlyCollection<Shape> SelectedShapes
        {
            get{ return _readOnlySelectedShapes; }
        }

        public event EventHandler<ShapeSelectedEventArgs> ShapeSelected;
        public event EventHandler<EventArgs> SelectionClear;


        public Layer (string name)
        {
            //default layer size 200 meter
            Size = DefaultSize;

            Name = name;
            Shapes = new LayerShapeCollection (this);

            _readOnlySelectedShapes = new ReadOnlyCollection<Shape> (_selectedShapes);
        }

        protected override Shape DoHitTest (Point point)
        {
            return Shapes.HitTest (point);
        }

        protected override Rect DoGetBounds ()
        {
            return Shapes.GetBounds ();
        }

        internal override void DoOffset (double x, double y)
        {
            throw new NotImplementedException ();
        }

        internal override void DoRotate (Point point, double radian)
        {
            throw new NotImplementedException ();
        }

        public void Select (Shape shape)
        {
            if (null == shape)
                throw new ArgumentNullException ();

            _selectedShapes.Add (shape);
            OnShapeSelected (new ShapeSelectedEventArgs (shape));
        }

        public void ClearSelection ()
        {
            _selectedShapes.Clear ();
            OnSelectionClear ();
        }

        private void OnSelectionClear ()
        {
            if (null != SelectionClear)
                SelectionClear (this, EventArgs.Empty);
        }

        private void OnShapeSelected (ShapeSelectedEventArgs e)
        {
            if (null != ShapeSelected)
                ShapeSelected (this, e);
        }

        public Rect ConvertToLogicalRect (Rect rect)
        {
            Point location = ConvertToLogicalPoint (rect.Location);
            Size size = ConvertToLogicalSize (rect.Size);
            return new Rect (location, size);
        }

        public Rect ConvertToViewRect (Rect rect)
        {
            Point location = ConvertToViewPoint (rect.Location);
            Size size = ConvertToViewSize (rect.Size);
            return new Rect (location, size);
        }

        public Point ConvertToLogicalPoint (Point point)
        {
            var matrix = Transform;
            matrix.Invert ();

            return matrix.Transform (point);
        }

        public Point ConvertToViewPoint (Point point)
        {
            return Transform.Transform (point);
        }

        public Size ConvertToLogicalSize (Size size)
        {
            return new Size (ConvertToLogicalLength (size.Width), ConvertToLogicalLength (size.Height)); 
        }

        public Size ConvertToViewSize (Size size)
        {
            return new Size (ConvertToViewLength (size.Width), ConvertToViewLength (size.Height)); 
        }

        public double ConvertToLogicalLength (double length)
        {
            return length / Transform.M11;
        }

        public double ConvertToViewLength (double length)
        {
            return length / Transform.M11;
        }

        public Vector ConvertToViewVector(Vector vector)
        {
            return new Vector (ConvertToViewLength (vector.X), ConvertToViewLength (vector.Y));
        }

        public Vector ConvertToLogicalVector(Vector vector)
        {
            return new Vector (ConvertToLogicalLength (vector.X), ConvertToLogicalLength (vector.Y));
        }
    }
}

