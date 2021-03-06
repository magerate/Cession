﻿using System;
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

        public ObservableCollection<Elevation> Elevations{ get; private set; }

        public ReadOnlyCollection<Region> Regions
        {
            get{ return _readonlyRegions; }
        }

        public ClosedShape Contour
        {
            get{ return _contour; }
        }

        internal Floor (ClosedShape contour)
        {
            _contour = contour;
            _contour.Ability = ShapeAbility.None;
            _contour.Parent = this;

            _regions = new List<Region> ();
            _regions.Add (new Region (contour));

            _readonlyRegions = new ReadOnlyCollection<Region> (_regions);

            Elevations = new ObservableCollection<Elevation> ();

            Ability = ShapeAbility.None;
        }

        public void AddDivider(ClosedShape contour,Polyline polyline)
        {
        }

        public void RemoveDivider(Polyline polyline)
        {
        }

        public override IEnumerator<Shape> GetEnumerator ()
        {
            foreach (var e in Elevations)
            {
                yield return e;
            }

            foreach (var region in _regions)
            {
                yield return region;
            }

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

