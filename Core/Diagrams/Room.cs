using System;
using System.Collections.Generic;

using Cession.Geometries;
using Cession.Dimensions;

namespace Cession.Diagrams
{
    public class Room:CompositeShape
    {
        public static readonly double DefaultWallThickness = Length.PixelsPerCentimeter * 20;

        private Floor _floor;
        private ClosedShape _outerContour;
        private Label _label;

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
            };

            RefreshOuterContour (contour);

            _label = new Label ("Room");
            Point location = contour.Center;
            location.X -= _label.Bounds.Width / 2;
            _label.Location = location;
            _label.Parent = this;
            _label.Ability = ShapeAbility.CanHitTest | ShapeAbility.CanOffset;
        }

        private void RefreshOuterContour(ClosedShape contour)
        {
            _outerContour = contour.Inflate (DefaultWallThickness);
            _outerContour.Parent = this;
        }

        public override IEnumerator<Shape> GetEnumerator ()
        {
            yield return _floor;
            yield return _outerContour;
            yield return _label;
        }
    }
}

