using System;
using System.Linq;
using System.Collections.Generic;

using Cession.Geometries;
using Cession.Diagrams;
using D = Cession.Diagrams;

namespace Cession.Tools
{
    public class PolygonMeasurer
    {
        private List<Point> _points = new List<Point> ();
        private Dictionary<Point,Point> _arcPoints = new Dictionary<Point, Point>();

        private Point? _currentPoint;

        public List<Point> Points
        {
            get{ return _points; }
        }

        public Dictionary<Point,Point> ArcPoints
        {
            get{ return _arcPoints; }
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

        public Polyline ToPolyline()
        {
            List<D.Segment> segments = new List<D.Segment> ();
            for (int i = 0; i < Points.Count -1; i++)
            {
                Point p1 = Points [i];
                if(ArcPoints.ContainsKey(p1))
                {
                    Point p2 = ArcPoints [p1];
                    var arc = new ArcSegment (p1, p2);
                    segments.Add (arc);
                }
                else
                {
                    segments.Add (new LineSegment(p1));
                }
            }
            return new Polyline (segments,Points.Last());
        }
    }
}

