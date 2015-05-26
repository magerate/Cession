using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Cession.Geometries;
using Cession.Dimensions;

namespace Cession.Diagrams
{
    public class Room:CompositeShape,IFoldable
    {
        public static readonly double DefaultWallThickness = Length.PixelsPerCentimeter * 20;

        private Floor _floor;
        private ClosedShape _outerContour;
        private Label _label;
        private WallSurfaceMediator _wallMediator;

        public ReadOnlyCollection<WallSurface> Walls
        {
            get{ return _wallMediator.Walls; }
        }

        public Floor Floor
        {
            get{ return _floor; }
        }

        public ClosedShape OuterContour
        {
            get{ return _outerContour; }
        }

        public Label Label
        {
            get{ return _label; }
        }

        public Room (ClosedShape contour)
        {
            _floor = new Floor (contour);
            _floor.Parent = this;
            contour.ContourChanged += (s,e) =>
            {
                RefreshOuterContour(s as ClosedShape);
                RefreshLabelLocation(s as ClosedShape,true);
            };

            RefreshOuterContour (contour);

            _label = new Label ("Room");
            RefreshLabelLocation (contour,false);
            _label.Parent = this;
            _label.Ability = ShapeAbility.CanOffset | ShapeAbility.CanSelect;

            _wallMediator = new WallSurfaceMediator (this, contour, Length.PixelsPerMeter * 3);
        }

        private void RefreshOuterContour(ClosedShape contour)
        {
            _outerContour = contour.Inflate (DefaultWallThickness);
            _outerContour.Parent = this;
        }

        private void RefreshLabelLocation(ClosedShape contour,bool isSideEffect)
        {
            Point location = contour.Center;
            location.X -= _label.Bounds.Width / 2;
            _label.SetLocation (location, isSideEffect);
        }

        public override IEnumerator<Shape> GetEnumerator ()
        {
            yield return _label;
            yield return _floor;
            yield return _outerContour;
        }

        protected override Rect DoGetBounds ()
        {
            return _outerContour.Bounds.Union (_label.Bounds);
        }

        #region "IFoldable"
        public IEnumerable<Shape> GetFoldShapes ()
        {
            return Walls;
        }

        public void Layout ()
        {
            _wallMediator.Layout (_outerContour.Bounds);
        }
        #endregion
    }
}

