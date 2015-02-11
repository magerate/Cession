using System;
using System.Collections.Generic;

using Cession.Geometries;

namespace Cession.Tools
{
    public class PolygonMeasurer
    {
        private List<Point> _points = new List<Point> ();
        private Point? _currentPoint;

        public List<Point> Points
        {
            get{ return _points; }
        }

        public Point? CurrentPoint
        {
            get{ return _currentPoint; }
            set{ _currentPoint = value; }
        }

        public PolygonMeasurer ()
        {
        }

        public void Clear()
        {
            _points.Clear ();
            _currentPoint = null;
        }
    }
}

