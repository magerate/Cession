using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Cession.Geometries;

namespace Cession.Diagrams
{
    public class Floor:CompositeShape
    {
        private ClosedShape _contour;

        private List<Region> _regions;
        private ReadOnlyCollection<Region> _readonlyRegions;

        public ReadOnlyCollection<Region> Regions
        {
            get{ return _readonlyRegions; }
        }

        internal Floor (ClosedShape contour)
        {
            _contour = contour;
            _contour.Ability = _contour.Ability & ~(ShapeAbility.CanSelect | ShapeAbility.CanOffset | ShapeAbility.CanRotate);
            _contour.Parent = this;

            _regions = new List<Region> ();
            _regions.Add (new Region (contour));

            _readonlyRegions = new ReadOnlyCollection<Region> (_regions);
        }

        public override IEnumerator<Shape> GetEnumerator ()
        {
            yield return _contour;
        }

        protected override bool DoContains (Point point)
        {
            return _contour.Contains (point);
        }

        protected override Rect DoGetBounds ()
        {
            return _contour.Bounds;
        }
    }
}

