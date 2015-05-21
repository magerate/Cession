using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

using Cession.Geometries;
using Cession.Dimensions;

namespace Cession.Diagrams
{
    public class Elevation:CompositeShape,IFoldable
    {
        public static double DefaultHeight{ get; set; }

        private List<WallSurface> _walls;
        private ClosedShape _contour;
        private double _height;
        private Region _region;

        public string Name{ get; set; }

        public ReadOnlyCollection<WallSurface> Walls{ get; private set; }

        public ObservableCollection<Elevation> ChildElevations{ get; private set; }

        public Region Region
        {
            get{ return _region; }
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
                if (value < 0)
                    throw new ArgumentOutOfRangeException ();
                
                if (value != _height)
                {
                    _height = value;
                    foreach (var wall in _walls)
                    {
                        wall.Height = value;
                    }
                }
            }
        }

        static Elevation ()
        {
            DefaultHeight = Length.PixelsPerMeter * 3;
        }

        public Elevation (ClosedShape contour)
        {
            if (null == contour)
                throw new ArgumentNullException ();

            Name = "C";
            _height = DefaultHeight;

            _contour = contour;
            _contour.Ability = _contour.Ability & ~(ShapeAbility.CanSelect | ShapeAbility.CanOffset | ShapeAbility.CanRotate);
            _contour.Parent = this;
            _region = new Region (_contour);

            _walls = new List<WallSurface> ();
            Walls = new ReadOnlyCollection<WallSurface> (_walls);
            CreateWalls (contour, DefaultHeight);

            ChildElevations = new ObservableCollection<Elevation> ();
        }

        public override IEnumerator<Shape> GetEnumerator ()
        {
            yield return _contour;
            foreach (var e in ChildElevations)
            {
                yield return e;
            }
        }

        public IEnumerable<Shape> GetFoldShapes ()
        {
            return Walls;
        }

        private void CreateWalls (ClosedShape contour, double height)
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

        protected override bool DoContains (Point point)
        {
            return _contour.Contains (point);
        }

        protected override Rect DoGetBounds ()
        {
            return _contour.Bounds;
        }

        public void Layout ()
        {
            double margin = 32;
            double maxWidth = 400;

            Rect bounds = Bounds;

            double maxX = bounds.Right + margin + maxWidth;

            double ox = bounds.Right + margin;
            double oy = bounds.Y - margin;

            double tx = ox;
            double ty = oy;

            foreach (var w in _walls)
            {
                Matrix m = new Matrix ();
                m.Translate (tx, ty);
                w.Transform = m;

                tx += w.Bounds.Width + margin;
                if (tx >= maxX)
                {
                    tx = ox;
                    ty += w.Height + margin;
                }
            }
        }
    }
}

