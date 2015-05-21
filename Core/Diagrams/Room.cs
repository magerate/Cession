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
//        private Label _label;

        public Floor Floor
        {
            get{ return _floor; }
        }

        public ClosedShape OuterContour
        {
            get{ return _outerContour; }
        }

        public Room (ClosedShape contour)
        {
            _floor = new Floor (contour);
            _floor.Parent = this;
            contour.ContourChanged += delegate
            {
                _outerContour = contour.Inflate (DefaultWallThickness);
            };

            _outerContour = contour.Inflate (DefaultWallThickness);
            _outerContour.Parent = this;
        }

        public override IEnumerator<Shape> GetEnumerator ()
        {
            yield return _floor;
            yield return _outerContour;
        }
    }
}

