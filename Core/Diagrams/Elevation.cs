using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Cession.Geometries;

namespace Cession.Diagrams
{
    public class Elevation:Shape
    {
        private List<WallSurface> _walls;
        private Path _contour;
        private double _height;

        public string Name{ get; set; }
        public Shape Dock{ get; set;}
        public ReadOnlyCollection<WallSurface> Walls{ get; private set; }

        public Path Contour
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

        public Elevation (Path contour)
        {
            Name = "C";
            _contour = contour;
            _walls = new List<WallSurface> ();
            Walls = new ReadOnlyCollection<WallSurface> (_walls);
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
    }
}

