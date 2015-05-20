using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Cession.Geometries;
using Cession.Dimensions;

namespace Cession.Diagrams
{
    public class Elevation:Shape
    {
        public static double DefaultHeight{ get; set; }

        private List<WallSurface> _walls;
        private ClosedShape _contour;
        private double _height;
        private Shape _dockedShape;
        private ObservableCollection<ClosedShape> _holes;

        public string Name{ get; set; }
        public ReadOnlyCollection<WallSurface> Walls{ get; private set; }

        public ObservableCollection<ClosedShape> Holes
        {
            get{ return _holes; }
        }

        public Shape DockedShape
        { 
            get{ return _dockedShape; }
        }

        public ClosedShape Contour
        {
            get{ return _contour; }
        }

        public double Height
        {
            get{ return _height; }
            set
            {
                if (value != _height)
                {
                    _height = value;
                }
            }
        }

        static Elevation()
        {
            DefaultHeight = Length.PixelsPerMeter * 3;
        }

        public Elevation (ClosedShape contour)
        {
            if (null == contour)
                throw new ArgumentNullException ();

            Name = "C";
            _contour = contour;
            _height = DefaultHeight;
            _holes = new ObservableCollection<ClosedShape> ();
            _walls = new List<WallSurface> ();
            Walls = new ReadOnlyCollection<WallSurface> (_walls);
            CreateWalls (contour, DefaultHeight);
        }

        private void CreateWalls(ClosedShape contour,double height)
        {
            if (contour is Path)
            {
                var path = contour as Path; 
                foreach (var segment in path.Segments)
                {
                    var wall = new WallSurface (segment, height);
                    _walls.Add (wall);
                }
            }
            else if (contour is Circle)
            {
                var circle = contour as Circle;
                var wall = new WallSurface (circle, height);
                _walls.Add (wall);
            }
        }

        internal override void DoOffset (double x, double y)
        {
            _contour.DoOffset (x, y);
        }

        internal override void DoRotate (Point point, double radian)
        {
            _contour.DoRotate (point, radian);
        }

        protected override Rect DoGetBounds ()
        {
            return _contour.GetBounds ();
        }

        protected override bool DoContains (Point point)
        {
            return _contour.Contains (point);
        }

        public void Dock(Shape shape)
        {
            if(_dockedShape != shape)
            {
                Undock ();
                _dockedShape = shape;
                if (_dockedShape is Elevation)
                {
                    ((Elevation)_dockedShape).Holes.Add (Contour);
                }
            }
        }

        public void Undock()
        {
            if (null == _dockedShape)
                return;

            if(_dockedShape is Elevation)
            {
                ((Elevation)_dockedShape).Holes.Remove (Contour);
            }
            _dockedShape = null;
        }
    }
}

