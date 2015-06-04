using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Cession.Geometries;
using Cession.Dimensions;

namespace Cession.Diagrams
{
    public class Elevation:CompositeShape,IFoldableHost
    {
        public static double DefaultHeight{ get; set; }

        private ClosedShape _contour;
        private double _height;
        private Region _region;
        private WallSurfaceMediator _wallMediator;
        public string Name{ get; set; }

        public ReadOnlyCollection<WallSurface> Walls
        {
            get{ return _wallMediator.Walls; }
        }

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
                    foreach (var wall in Walls)
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
            _contour.Ability = ShapeAbility.None;
            _contour.Parent = this;
            _region = new Region (_contour);

            _wallMediator = new WallSurfaceMediator (this,contour, DefaultHeight);


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

       

        protected override bool DoContains (Point point)
        {
            return _contour.Contains (point);
        }

        protected override Rect DoGetBounds ()
        {
            return _contour.Bounds;
        }

        #region "IFoldable"
        public IEnumerable<Shape> GetFoldableShapes ()
        {
            return Walls;
        }

        public void Layout ()
        {
            _wallMediator.Layout (_contour);
        }
        #endregion
    }
}

