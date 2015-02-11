using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Cession.Geometries;
using Cession.Utilities;

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
        public static readonly int LogicalUnitPerPixel = 25;
        public static readonly Size DefaultSize = new Size (200000, 200000);

        public string Name{ get; set; }

        public ShapeCollection Shapes{ get; private set; }

        private List<Shape> _selectedShapes = new List<Shape> ();
        private ReadOnlyCollection<Shape> _readOnlySelectedShapes;

        public Matrix Transform{ get; set; }

        public double Scale
        {
            get{return Transform.M11;}
        }

        public Size Size{ get; set; }

        public ReadOnlyCollection<Shape> SelectedShapes
        {
            get{ return _readOnlySelectedShapes; }
        }

        public event EventHandler<ShapeSelectedEventArgs> ShapeSelected;
        public event EventHandler<EventArgs> SelectionClear;


        public Layer (string name)
        {
            this.Transform = Matrix.Identity;

            //default layer size 200 meter
            Size = DefaultSize;

            this.Name = name;
            Shapes = new ShapeCollection ();

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
            Transform.ScalePrepend (1 / LogicalUnitPerPixel, 1 / LogicalUnitPerPixel);
            matrix.Invert ();

            return matrix.Transform (point);
        }

        public Point ConvertToViewPoint (Point point)
        {
            var matrix = Transform;
            Transform.ScalePrepend (1 / LogicalUnitPerPixel, 1 / LogicalUnitPerPixel);
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
            return length * LogicalUnitPerPixel / Transform.M11;
        }

        public double ConvertToViewLength (double length)
        {
            return length / LogicalUnitPerPixel * Transform.M11;
        }


        //        public static Matrix GetDefaultTransform (Size layerSize, int width, int height, int logicalUnitPerDp)
        //        {
        //            var matrix = Matrix.Identity;
        //            matrix.Scale ((double)1 / logicalUnitPerDp, (double)1 / logicalUnitPerDp);
        //            matrix.Translate ((width - layerSize.Width / logicalUnitPerDp) / 2,
        //                (height - layerSize.Height / logicalUnitPerDp) / 2);
        //            return matrix;
        //        }
        //
        //        public static Matrix GetDefaultTransform (int width, int height, int logicalUnitPerDp)
        //        {
        //            return GetDefaultTransform (Layer.DefaultSize, width, height, logicalUnitPerDp);
        //        }

        //        internal override void InternalOffset (int x, int y)
        //        {
        //            Transform.Translate ((double)x, (double)y);
        //        }

    }
}

